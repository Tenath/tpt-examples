#include "GLIntroApp.hpp"
#include "Transform.hpp"

std::vector<Vertex> vertices =
{
	Vertex(Vector3f(-0.5f, -0.3f, 0.0f), Vector4f(1.0f,0.0f,0.0f,1.0f)),
	Vertex(Vector3f(0.5f, -0.3f, 0.0f), Vector4f(0.0f,1.0f,0.0f,1.0f)),
	Vertex(Vector3f(0.0f, 0.6f, 0.0f), Vector4f(0.0f,0.0f,1.0f,1.0f)),
};

/*std::vector<unsigned> indices =
{
	0,1,2
};*/

void GLIntroApp::Draw()
{
	Application::Draw();
	vertex_array->Bind();
	program->Bind();
	glUniformMatrix3fv(rotation_uniform, 1, false, rotation_matrix.data);

	vertex_array->Draw(0);

	vertex_array->Unbind();
	program->Unbind();
}

void GLIntroApp::AppInit()
{
	SetTargetFPS(144);
	program = new Program("shaderlist.txt");

	if (!program->IsBuilt())
	{
		throw std::logic_error("Error in shader program: " + program->GetError());
	}

	PrimitiveSequence<unsigned> triangle(PrimitiveType::Triangles, { 0,1,2 });
	std::vector<PrimitiveSequence<unsigned>> sequences = { triangle };

	RecomputeAspectRatio();
	vertex_array = new VertexArray<Vertex, unsigned>(vertices, sequences);

	rotation_uniform = glGetUniformLocation(program->GetHandle(), "rm");
	aspect_uniform = glGetUniformLocation(program->GetHandle(), "aspect_ratio");

	rotation_matrix = GenerateRotationMatrix(0.0f, 0.0f, 0.0f);
	program->Bind();
	glUniform1fv(aspect_uniform, 1, &aspect_ratio);
	glUniformMatrix3fv(rotation_uniform, 1, false, rotation_matrix.data);
	program->Unbind();
}

void GLIntroApp::RecomputeAspectRatio()
{
	aspect_ratio = WinWidth / (float)WinHeight;
}

void GLIntroApp::HandleEvents()
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

	if (SDL_GetKeyboardState(0)[SDL_SCANCODE_W]) { rotation.X() += 0.1f; }
	if (SDL_GetKeyboardState(0)[SDL_SCANCODE_S]) { rotation.X() -= 0.1f; }
	if (SDL_GetKeyboardState(0)[SDL_SCANCODE_A]) { rotation.Y() += 0.1f; }
	if (SDL_GetKeyboardState(0)[SDL_SCANCODE_D]) { rotation.Y() -= 0.1f; }
	if (SDL_GetKeyboardState(0)[SDL_SCANCODE_Q]) { rotation.Z() += 0.1f; }
	if (SDL_GetKeyboardState(0)[SDL_SCANCODE_E]) { rotation.Z() -= 0.1f; }
}

void GLIntroApp::Process()
{
	rotation_matrix = GenerateRotationMatrix(rotation.X(), rotation.Y(), rotation.Z());
}
void GLIntroApp::ResizeWindow()
{

}