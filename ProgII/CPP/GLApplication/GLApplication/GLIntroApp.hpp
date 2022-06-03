#include "Application.hpp"
#include "GLObject.hpp"
#include "av_matrix_t.hpp"

using namespace av;

class GLIntroApp : public Application
{
	VertexArray<Vertex, unsigned>* vertex_array = nullptr;
	Program* program = nullptr;

	Vector3f rotation;

	Matrix3f rotation_matrix;

	GLuint rotation_uniform;
	GLuint aspect_uniform;

	float aspect_ratio = 1.0f;

protected:
	void RecomputeAspectRatio();

	void Draw();
	void AppInit();

	void HandleEvents();
	void Process();
	void ResizeWindow();
public:

	virtual ~GLIntroApp()
	{
		if (vertex_array != nullptr) delete vertex_array;
		if (program != nullptr) delete program;
	}
};