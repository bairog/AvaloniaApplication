using Avalonia.Controls;
using System;

namespace AvaloniaApplication1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Text1.Text = System.Reflection.Assembly.GetExecutingAssembly().Location;
        }
    }
}