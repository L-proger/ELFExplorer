using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace EmbeddedMemoryExplorer {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        protected override void OnStartup(StartupEventArgs e) {
            try {
                var window = new MemoryExplorerWindow { DataContext = MemoryExplorerViewModel.Instance };
                window.Show();
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
