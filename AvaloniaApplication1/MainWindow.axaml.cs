using Avalonia.Controls;
using AvaloniaApplication.DAL;
using System;

namespace AvaloniaApplication1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Text1.Text = System.Reflection.Assembly.GetExecutingAssembly().Location;
            Text2.Text = String.Join(Environment.NewLine, StorageSystem.GetAllPlaneFlights());
        }
    }
}