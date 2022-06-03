// GLApplication.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <SDL.h>
#include <GL/glew.h>
#include "GLIntroApp.hpp"

// Через nuget поставить sdl2.nuget (версия 2.0.18) и glew-2.2.0

// glu32.lib
// opengl32.lib

int main(int argc, char* argv[])
{
    try
    {
        GLIntroApp app;
        app.Run();
    }
    catch (std::exception& e)
    {
        std::cout << "Fatal error: " << e.what();
    }

    return 0;
}

/*int old_main()
{
    try
    {
        if (SDL_Init(SDL_INIT_VIDEO | SDL_INIT_TIMER))
            throw std::logic_error("Failed to initialize SDL subsystems");

        SDL_GL_SetAttribute(SDL_GL_CONTEXT_MAJOR_VERSION, 3); // OpenGL 3.3
        SDL_GL_SetAttribute(SDL_GL_CONTEXT_MINOR_VERSION, 3);

        SDL_GL_SetAttribute(SDL_GL_DOUBLEBUFFER, 1);
        SDL_GL_SetAttribute(SDL_GL_DEPTH_SIZE, 24);

        SDL_Window* window = SDL_CreateWindow("SDL Application", SDL_WINDOWPOS_CENTERED, SDL_WINDOWPOS_CENTERED,
            800, 600, SDL_WINDOW_OPENGL | SDL_WINDOW_SHOWN | SDL_WINDOW_RESIZABLE);

        if (window == nullptr) throw std::logic_error("Failed to create SDL window");

        SDL_GLContext glctx = SDL_GL_CreateContext(window);
        if (glctx == nullptr) throw std::logic_error("Failed to create OpenGL context");

        GLenum gl_status = glewInit();

        bool running = true;

        SDL_Event evt;

        while (running)
        {
            // Обработка событий
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
            // Внутренние вычисления

            // Представление
            glClearColor(0.0f, 0.0f, 0.3f, 0.0f);
            glClearDepth(1.0f);

            glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

            SDL_GL_SwapWindow(window);

            SDL_Delay(16);
        }

        SDL_GL_DeleteContext(glctx);
        SDL_DestroyWindow(window);
    }
    catch (std::exception e)
    {
        std::cout << "Fatal error: " << e.what();
    }

    SDL_Quit();

    return 0;
}*/

// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file
