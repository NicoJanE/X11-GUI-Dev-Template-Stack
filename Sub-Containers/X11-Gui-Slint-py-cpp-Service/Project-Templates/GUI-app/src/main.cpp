#include <slint.h>
#include "hello_world.h"        // Generated from hello_world.slint, ❗WARNING this will display red in VSC when app is not build❗

int main()
{
    auto window = HelloWorldWindow::create();
    window->run();
    return 0;
}
