#pragma once
#include <stdexcept>
#include <SDL.h>
#include <GL\glew.h>

enum class AppComponent : size_t
{
    SDL_Subsystems = 0,
    Window = 1,
    GL_Context = 2,
    GL_Procedures = 3,
    LAST
};

class Application
{
protected:
    static const size_t STATUS_SIZE = (size_t)AppComponent::LAST;

    SDL_Window* window = nullptr;
    SDL_GLContext glctx = nullptr;
    SDL_Event evt;

    bool running = true;

    bool status[STATUS_SIZE] = {};

    unsigned target_fps = 60;
    unsigned frame_delay = 16;
    unsigned time_frame_start = 0;
    unsigned time_frame_end = 0;

    unsigned WinWidth = 800;
    unsigned WinHeight = 600;

protected:
    constexpr inline bool& Status(AppComponent cmp) { return status[(size_t)cmp]; }

    void Cleanup()
    {
        if (Status(AppComponent::GL_Context)) SDL_GL_DeleteContext(glctx);
        if (Status(AppComponent::Window)) SDL_DestroyWindow(window);
        if (Status(AppComponent::SDL_Subsystems)) SDL_Quit();
    }
public:
	Application()
	{
        try
        {
            SetTargetFPS(60);

            if (SDL_Init(SDL_INIT_VIDEO | SDL_INIT_TIMER))
                throw std::logic_error("Failed to initialize SDL subsystems");
            else Status(AppComponent::SDL_Subsystems) = true;

            SDL_GL_SetAttribute(SDL_GL_CONTEXT_MAJOR_VERSION, 3); // OpenGL 3.3
            SDL_GL_SetAttribute(SDL_GL_CONTEXT_MINOR_VERSION, 3);

            SDL_GL_SetAttribute(SDL_GL_DOUBLEBUFFER, 1);
            SDL_GL_SetAttribute(SDL_GL_DEPTH_SIZE, 24);

            window = SDL_CreateWindow("SDL Application", SDL_WINDOWPOS_CENTERED, SDL_WINDOWPOS_CENTERED,
                800, 600, SDL_WINDOW_OPENGL | SDL_WINDOW_SHOWN | SDL_WINDOW_RESIZABLE);

            if (window == nullptr) throw std::logic_error("Failed to create SDL window");
            else Status(AppComponent::Window) = true;

            glctx = SDL_GL_CreateContext(window);
            if (glctx == nullptr) throw std::logic_error("Failed to create OpenGL context");
            else Status(AppComponent::GL_Context) = true;

            GLenum gl_status = glewInit();
            if (gl_status) throw std::logic_error("Failed to bind OpenGL procedures");
            else Status(AppComponent::GL_Procedures) = true;
        }
        catch (std::exception& e)
        {
            Cleanup();
            throw e;
        }
        
	}

    virtual ~Application()
    {
        Cleanup();
    }

    void Run()
    {
        AppInit();

        unsigned time_next_frame = 0;
        unsigned sleep_duration = 0;

        while (running)
        {
            // «асекаем врем€ начала кадра
            time_frame_start = SDL_GetTicks();
            // ѕодсчитываем, в какое врем€ по плану должна начинатьс€ отрисовка следующего кадра
            time_next_frame = time_frame_start + frame_delay;
            // ќбработка событий
            HandleEvents();
            // ¬нутренние вычислени€
            Process();
            // ѕредставление
            Draw();

            SDL_GL_SwapWindow(window);

            // «асекаем врем€ конца кадра
            time_frame_end = SDL_GetTicks();
            // ≈сли врем€ конца кадра меньше планового дл€ начала следующего (успели обработать кадр быстрее,
            // чем за целевое дл€ данного FPS врем€), тогда ищему разницу между ними, и ждЄм найденное
            // количество миллисекунд. ¬ ином случае(если не успеваем) - "ждЄм" 0 миллисекунд.
            sleep_duration = (time_frame_end < time_next_frame) ? (time_next_frame - time_frame_end) : 0;

            SDL_Delay(sleep_duration);
        }
    }

    void SetTargetFPS(unsigned fps)
    {
        if (fps == 0) fps = 1000;
        target_fps = fps;

        frame_delay = (unsigned)round(1000.0f / fps);
    }

protected:
    virtual void AppInit() {}

    virtual void HandleEvents()
    {
        while (SDL_PollEvent(&evt))
        {
            switch (evt.type)
            {
            case SDL_QUIT:
                running = false;
                break;
            default:
                break;
            }
        }
    }

    virtual void Process()
    {

    }

    virtual void Draw()
    {
        glClearColor(0.0f, 0.0f, 0.3f, 0.0f);
        glClearDepth(1.0f);
        glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
    }
};