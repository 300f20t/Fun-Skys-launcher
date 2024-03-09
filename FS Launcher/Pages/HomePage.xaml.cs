using CmlLib.Core.Auth;
using CmlLib.Core.Downloader;
using CmlLib.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using CmlLib.Core.Version;

namespace FS_Launcher
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        private string nickname = "NoName";
        private int memory = Convert.ToInt32(File.ReadAllText(Environment.CurrentDirectory + "//Settings//Memory.txt"));
        public HomePage()
        {
            InitializeComponent();
            nickname = File.ReadAllText(Environment.CurrentDirectory + "//UserData//Login.txt");
            Nick.Text = nickname;
        }

        private void But1_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SettingsPage());
        }

        private async void Play_Click(object sender, RoutedEventArgs e)
        {
            var path = new MinecraftPath();
            var launcher = new CMLauncher(path);
            launcher.FileChanged += (e) =>
            {
                Console.WriteLine("FileKind: " + e.FileKind.ToString());
                Console.WriteLine("FileName: " + e.FileName);
                Console.WriteLine("ProgressedFileCount: " + e.ProgressedFileCount);
                Console.WriteLine("TotalFileCount: " + e.TotalFileCount);
            };
            launcher.ProgressChanged += (s, e) =>
            {
                Console.WriteLine("{0}%", e.ProgressPercentage);
            };
            
            var versions = await launcher.GetAllVersionsAsync();
            foreach (var v in versions)
            {
                Console.WriteLine(v.Name);
            }
            var process = await launcher.CreateProcessAsync("1.20.1", new MLaunchOption
            {
                MaximumRamMb = 2048,
                Session = MSession.GetOfflineSession("hello123"),
            });
            process.Start();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(Environment.CurrentDirectory + "//UserData//Login.txt", Nick.Text);
        }
    }
}
