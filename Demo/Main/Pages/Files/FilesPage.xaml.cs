using System.Windows;
using Microsoft.Win32;

namespace CodeWanda.UI.Main.Pages.Files
{
    public partial class FilesPage
    {
        public FilesPage()
        {
            InitializeComponent();
        }

        private void OnAddClicked(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "C# Code Files (*.cs)|*.cs|All files (*.*)|*.*"
            };

            openFileDialog.ShowDialog();
        }

        private void OnRemoveClicked(object sender, RoutedEventArgs e)
        {
        }
    }
}