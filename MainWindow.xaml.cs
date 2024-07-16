using DFS.DesktopV2.View;
using System;
using System.Collections.Generic;
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

namespace DFS.DesktopV2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        Window login;
        public MainWindow(Window parent)
        {
            InitializeComponent();

            login = parent;
        }

        private void Window_Closing(object sender,System.ComponentModel.CancelEventArgs e)
        {
            login.Show();
        }
        private void Window_MouseDown(object sender,MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void btnMinimize_Click(object sender,RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void btnMaximize_Click(object sender,RoutedEventArgs e)
        {

            if(WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;
            }
        }
        private void btnClose_Click(object sender,RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnLogout_Click(object sender,RoutedEventArgs e)
        {
            this.Close();
        }
        
        private async void sidebar_SelectionChanged(object sender,SelectionChangedEventArgs e)
        {
            var selected = sidebar.SelectedItem as NavButton;

            //MessageBox.Show("Hello Boostrap");
            LoadingOverlay.Visibility = Visibility.Visible;
            await Task.Delay(1000); // Simulate loading data
            LoadingOverlay.Visibility = Visibility.Collapsed;
            navframe.Navigate(selected.Navlink);
        }
      
    }
}
