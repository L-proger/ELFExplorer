using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using EmbeddedMemoryExplorer.Annotations;
using Microsoft.Win32;

namespace EmbeddedMemoryExplorer {
    /// <summary>
    /// Interaction logic for OpenElfWindow.xaml
    /// </summary>
    public partial class OpenElfWindow : Window, INotifyPropertyChanged
    {
        private string _elfPath;
        public string _scriptPath;

        public OpenElfWindow() {
            InitializeComponent();
            btnOk.IsEnabled = System.IO.File.Exists(txtElf.Text);
        }

        public string ElfPath
        {
            get { return _elfPath; }
            set { _elfPath = value; OnPropertyChanged();}
        }

        public string ScriptPath
        {
            get { return _scriptPath; }
            set { _scriptPath = value;
                OnPropertyChanged();
            }
        }

        private void onSelectElfClick(object sender, RoutedEventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            if(ofd.ShowDialog() == true)
            {
                txtElf.Text = ofd.FileName;
            }
        }

        private void onSelectScriptClick(object sender, RoutedEventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            if(ofd.ShowDialog() == true) {
                txtScript.Text = ofd.FileName;
            }
        }

        private void onBtnCancelClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void onElfPathChanged(object sender, TextChangedEventArgs e)
        {
            btnOk.IsEnabled = System.IO.File.Exists(txtElf.Text);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void onBtnOkClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
