using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AsyncProgrammingOnClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("UI Thread");

            await DoWorkAsync();

            Debug.WriteLine("Continuation Task");

            button.Background = new SolidColorBrush(Colors.Blue);
        }

        private async Task DoWorkAsync()
        {
            Debug.WriteLine("Work Started");

            await Task.Delay(3000);

            Debug.WriteLine("Work Ended");
        }
    }
}
