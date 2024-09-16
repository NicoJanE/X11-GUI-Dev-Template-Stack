using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.ReactiveUI;
using ReactiveUI;
using System.Reactive;

namespace DefaultApp.Views.UserControls
{
    /// <summary>
    /// Interaction logic for MenuWithToolBar.xaml
    /// </summary>
    public partial class MenuWithToolBar : UserControl
    {
        public ReactiveCommand<Unit, Unit> NewCommand { get; }
        public ReactiveCommand<Unit, Unit> CopyCommand { get; }
        public ReactiveCommand<Unit, Unit> HelpCommand { get; }


        public MenuWithToolBar()
        {
            InitializeComponent();
            // Initialize the commands
            NewCommand = ReactiveCommand.Create(() => { System.Diagnostics.Debug.WriteLine("New Command Executed"); });

            CopyCommand = ReactiveCommand.Create(() => { System.Diagnostics.Debug.WriteLine("Copy Command Executed"); });

            HelpCommand = ReactiveCommand.Create(() => { System.Diagnostics.Debug.WriteLine("Help Command Executed"); });

            // Set DataContext to this (or ViewModel if using one)
            DataContext = this;
        }
    }
}
