using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace FS_Launcher
{
    /// <summary>
    /// Логика взаимодействия для Page2.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
            Memory.Text = File.ReadAllText(Environment.CurrentDirectory + "//Settings//Memory.txt");
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new HomePage());
        }

        private void RibbonTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (!Char.IsNumber((char)KeyInterop.VirtualKeyFromKey(e.Key)))
            {
                e.Handled = true;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(Environment.CurrentDirectory + "//Settings//Memory.txt", Memory.Text);
        }
    }
}
