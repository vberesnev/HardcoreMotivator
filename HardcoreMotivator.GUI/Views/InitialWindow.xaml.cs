using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HardcoreMotivator.GUI.ViewModels;

namespace HardcoreMotivator.GUI.Views
{
    /// <summary>
    /// Interaction logic for InitialWindow.xaml
    /// </summary>
    public partial class InitialWindow : Window
    {
        public InitialWindow()
        {
            InitializeComponent();
            var dataContext = (BaseViewModel) DataContext;
            dataContext.MessageEvent += ShowMessageBox;
        }

        private void ShowMessageBox(object sender, ViewModelMessageEventArgs e)
        {
            switch (e.Type)
            {
                case EventType.Message:
                    MessageBox.Show(e.Message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case EventType.Success:
                    MessageBox.Show(e.Message, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case EventType.Error:
                    MessageBox.Show(e.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                default:
                    MessageBox.Show(e.Message, "", MessageBoxButton.OK, MessageBoxImage.None);
                    break;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void WindowTop_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
