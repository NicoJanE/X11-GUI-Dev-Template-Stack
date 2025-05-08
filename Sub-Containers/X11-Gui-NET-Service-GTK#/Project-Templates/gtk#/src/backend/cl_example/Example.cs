
// This sample class library contains a simple business logic method. 
// When a library name starts with 'cl_' (e.g., cl_[name]), it becomes compatible 
// with tasks in Visual Studio Code (e.g., Clean tasks). During the build process, 
// the library name (based on the directory name) is transformed by replacing 'cl_' with 'lib_' 
// to generate the target output name (e.g., lib_[name]).
//
// To create a similar library, use the VSC task: '2.1 AFX CREATE: Class Source Library'.

namespace app.src.backend.cl_example
{
    public static class BusinessLogicExample
    {
        public static string GetGreeting(string name)
        {
            return $"Hello you there,  {name}!";
        }
    }
}