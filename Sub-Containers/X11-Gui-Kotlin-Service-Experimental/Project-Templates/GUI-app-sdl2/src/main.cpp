#include <thread>
#include <slint.h>
#include "hello_world.h"        // Generated from hello_world.slint, ❗WARNING this will display red in VSC when app is not build❗


#ifdef _WIN32
    #include <windows.h>
#endif

#include <SDL.h>

// forwards
void run_sdl_window();

int main()
{
    auto window = HelloWorldWindow::create();
    
    // Set up the event handler triggered from the Slint UI.
    // When the event is triggered (e.g., button click), it will run the 'SDL2 window creation' in a separate thread (via a Lamda)
    window->on_launch_sdl([]() {
        // Run SDL window creation in a separate thread (asynchronous task)
        // std::thread runs the task, and .detach() ensures it operates independently of the main thread.
        std::thread(run_sdl_window).detach();
    });

    // window->show();
    // Run the Slint event loop, keeping the UI responsive and open.    
    window->run();
    return 0;
}


// SDL2 Window called by event on_launch_sdl, running in it's own thread
void run_sdl_window() 
{
    if (SDL_Init(SDL_INIT_VIDEO) != 0) {
        SDL_Log("SDL_Init Error: %s", SDL_GetError());
        return;
    }

    SDL_Window* win = SDL_CreateWindow("SDL Window", 100, 100, 640, 480, SDL_WINDOW_SHOWN);
    if (!win) {
        SDL_Log("SDL_CreateWindow Error: %s", SDL_GetError());
        SDL_Quit();
        return;
    }

    SDL_Renderer* ren = SDL_CreateRenderer(win, -1, SDL_RENDERER_ACCELERATED | SDL_RENDERER_PRESENTVSYNC);
    if (!ren) {
        SDL_DestroyWindow(win);
        SDL_Log("SDL_CreateRenderer Error: %s", SDL_GetError());
        SDL_Quit();
        return;
    }

    bool running = true;
    SDL_Event e;

    while (running) {
        while (SDL_PollEvent(&e)) {
            if (e.type == SDL_QUIT) {
                running = false;
            }
        }

        SDL_SetRenderDrawColor(ren, 0, 200, 200, 255);
        SDL_RenderClear(ren);
        SDL_RenderPresent(ren);
        SDL_Delay(16);
    }

    SDL_DestroyRenderer(ren);
    SDL_DestroyWindow(win);
    SDL_Quit();
}

