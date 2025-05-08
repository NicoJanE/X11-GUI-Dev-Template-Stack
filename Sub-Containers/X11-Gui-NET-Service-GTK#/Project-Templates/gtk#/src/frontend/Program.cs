using System;
using System.Collections.ObjectModel;
using Gtk;

// Application libraries used
using app.src.backend.cl_example_rest;
using app.src.backend.cl_example;


namespace app
{
    // General methods
    interface IProgramGTK
    {
        void SampleInterfaceMethod();
    }


    class ProgramGTK: IProgramGTK
    {
        private InitMainWindow _mainWindow;
        //private CallRestSample _callRestSample;

        static void Main(string[] args)
        {
            var program = new ProgramGTK();            
            Application.Init();     // Ensure GTK object is initialized first            
            program.Initialize();   // Explicitly initialize components after Application.Init()
            
            Application.Run(); // Start the GTK application loop
        }

        public ProgramGTK()
        {
            Console.WriteLine("Constructor called");
//            _callRestSample = new CallRestSample(); // Business logic is independent of GTK
        }

        private void Initialize()
        {
            _mainWindow = new InitMainWindow(this); // Initialize window only after Application.Init()
            _mainWindow.ShowAll(); // Show the main window
        }

        // INTERFACE METHODS
        // -----------------------------------------------------------------------------------------------
        // Example of an interface method
        public void SampleInterfaceMethod()
        {
            Console.WriteLine("ProgramGTK.SampleInterfaceMethod called");
        }

    }

    class InitMainWindow :Window
    {
        private IProgramGTK _programGTK;            // Interface methods of ProgramGTK
        public TextView TextView { get; set; }
        
        public InitMainWindow(ProgramGTK programGTK) : base("GtkSharp, a GTK# Application1")        
        {
            _programGTK = programGTK;               // Intialize Interface member
            
            var vbox = new VBox(false, 10);         // False means no homogeneous resizing, 10 is the spacing

            SetDefaultSize(400, 200);
            var label = new Label(BusinessLogicExample.GetGreeting("My friend"));
            var button = new Button("Update Books by using Rest");
            TextView = new TextView();
            
            TextView.Editable = false;      
            TextView.WrapMode = WrapMode.Word; 
            var scrolledWindow = new ScrolledWindow();
            scrolledWindow.Add(TextView);

            button.Clicked += OnButtonClicked;
            vbox.Add(button);
            vbox.Add(label);
            vbox.Add(scrolledWindow);            
            Add(vbox);          

            DeleteEvent += (o, e) =>
            {
                Application.Quit();
            };

            // Example of calling a Interface ProgramGTK method
            _programGTK.SampleInterfaceMethod();
        }

        // EVENT METHODS
        // -----------------------------------------------------------------------------------------------
        private void OnButtonClicked(object sender, EventArgs e)
        { 
            var _callRestSample = new CallRestSample();
            TextView.Buffer.Text = "Getting result with ID 2 from the book collection via REST:\n" + _callRestSample.GetAuthorById();
            Console.WriteLine("Rest executed!");
        }

        //public void Show()
        //{
        //    ShowAll();
        //}



    }




    class CallRestSample
    {
        private RestViewModel _restViewModel;
        private ObservableCollection<Book> books;

        public CallRestSample()
        {
            _restViewModel = new RestViewModel();
            books = _restViewModel.Books;

            _restViewModel.AddBook("The Lord of the Rings", "J.R.R. Tolkien");
            _restViewModel.DeleteBook(1);
            var selectedBook = _restViewModel.GetBookById(2);
            Console.WriteLine("CallRestSample constructor called");
        }

        public string GetAuthorById(int id=2)
        {
            Book _book =  _restViewModel.GetBookById(id);
            return _book.Author; // why does it not return the author? string
        }

    }
}