using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AsyncAwaitWpfApp
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

        private static int GetRandomTimespan()
        {
            return (Random.Shared.Next() % 2000) + 1000;
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            Info.Text = "";

            var list = new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(Task.Run(() => Thread.Sleep(GetRandomTimespan())));
            }
            Task.WaitAll([.. list]); // Blockierende Operation
            Info.Text = "Tasks fertig";
        }

        private async void StartAsync_Click(object sender, RoutedEventArgs e)
        {
            Info.Text = "";

            var list = new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(Task.Delay(GetRandomTimespan()));
            }
            await Task.WhenAll([.. list]); // Blockiert nicht
            Info.Text = "Tasks fertig";
        }

        private async void RequestAsync_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("https://jsonplaceholder.typicode.com/todos/1");
                var result = await response.Content.ReadAsStringAsync();
                Info.Text = result;
            }
        }
    }
}