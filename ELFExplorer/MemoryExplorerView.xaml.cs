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
using Microsoft.Win32;

namespace EmbeddedMemoryExplorer {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MemoryExplorerWindow : Window {
        public MemoryExplorerWindow() {
            InitializeComponent();
            
            DataContext = MemoryExplorerViewModel.Instance;

            ICollectionView cvTasks = CollectionViewSource.GetDefaultView(SymbolsGrid.ItemsSource);

            // CollectionViewSource s = FindResource("symbolsCollection") as SymbolsCollection;
            MemoryExplorerViewModel.Instance.SymbolsView = cvTasks;
            MemoryExplorerViewModel.Instance.Symbols = FindResource("symbolsCollection") as SymbolsCollection;
        }

        private void onFileOpenClicked(object sender, RoutedEventArgs e) {
            OpenElfWindow wnd = new OpenElfWindow();
            if(wnd.ShowDialog() == true) {
                MemoryExplorerViewModel.Instance.Reload(wnd.ElfPath, wnd.ScriptPath);

            }
        }

        private void onDataGridDoubleClick(object sender, MouseButtonEventArgs e)
        {

            
            var dataGrid = (sender as DataGrid);
            MemoryExplorerViewModel.Instance.OnDoubleClick(dataGrid.SelectedItem as SymbolDescriptor);
        }
    }
}
