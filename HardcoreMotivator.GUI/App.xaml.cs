using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using HardcoreMotivator.BL;
using HardcoreMotivator.GUI.Views;

namespace HardcoreMotivator.GUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                if (DataBaseManager.IsUserExists())
                {
                    MainWindow window = new MainWindow();
                    window.Show();
                }
                else
                {
                    InitialWindow window = new InitialWindow();
                    window.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
