using System.Linq;
using System.Windows;
using System.Windows.Input;
using CodeWanda.UI.Common;
using CodeWanda.UI.Main;

namespace CodeWanda.UI.Welcome
{
    /// <summary>
    /// Interaction logic for WelcomeWindow.xaml
    /// </summary>
    public partial class WelcomeWindow
    {
        private MainWindow _mainWindow;

        public WelcomeWindow()
        {
            InitializeComponent();
        }

        private void OnNewProject(object sender, RoutedEventArgs e)
        {
            _mainWindow = new MainWindow();

            Close();

            _mainWindow.ShowDialog();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var pt = e.GetPosition((IInputElement) sender);

            if (!Match(RecentListView, WelcomeCreateNewButton, WelcomeOpenExistingButton))
            {
                DragMove();
            }

            bool Match(params FrameworkElement[] elements)
            {
                return elements.Select(element => element.GetAbsolutePlacement(Window)).Any(rect => rect.Contains(pt));
            }
        }
    }
}