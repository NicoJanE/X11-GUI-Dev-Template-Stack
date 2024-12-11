using Gtk;
using System;


namespace cl_example;                                                       // References to Classlib: src/backend/cl_example

class Program
{
    static void Main(string[] args)
    {
        Application.Init();
        var window = new Window("GtkSharp Application");
        window.SetDefaultSize(400, 200);

        var label = new Label (BusinessLogicExample.GetGreeting("My friend") );        
        window.Add(label);
        
        window.ShowAll();
        window.DeleteEvent += (o, e) =>                                     // Exit when the window is closed
        {
            Application.Quit();
        };

        Application.Run();
    }
}