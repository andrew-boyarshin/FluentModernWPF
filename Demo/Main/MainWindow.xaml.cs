using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using MahApps.Metro.Controls;
using ModernWpf.Controls.Primitives;
using Serilog;

namespace CodeWanda.UI.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            
            AttachStaticTab("MainMenuFiles", "FilesPage");
            AttachStaticTab("MainMenuControlFlow", "ControlFlowPage");

            void AttachStaticTab(string menuItemName, string menuItemValue)
            {
                if (TryFindResource(menuItemName) is HamburgerMenuIconItem item)
                {
                    if ((item.Tag = TryFindResource(menuItemValue)) == null)
                    {
                        Log.Error("Tab {Name} is missing", menuItemValue);
                    }
                }
                else
                {
                    Log.Error("Menu item {Name} is missing", menuItemName);
                }
            }
        }

        private void NavView_ItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs e)
        {
            if (e.IsItemOptions)
            {
                Navigate(NavView.SelectedOptionsItem);
            }
            else
            {
                Navigate(NavView.SelectedItem);
            }
        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            bool TagContentPredicate(HamburgerMenuItem x) => x.Tag == e.Content;

            NavView.SelectedItem = NavView.Items.OfType<HamburgerMenuItem>().FirstOrDefault(TagContentPredicate);

            var selectedItem = NavView.SelectedItem ?? NavView.SelectedOptionsItem;
            if (selectedItem is HamburgerMenuItem item)
            {
                NavView.Header = item.Label;
            }
        }

        private void Navigate(object item)
        {
            if (!(item is HamburgerMenuItem menuItem)) return;

            if (ContentFrame.Content != menuItem.Tag)
            {
                ContentFrame.Navigate(menuItem.Tag);
            }
        }

        private void PaneHyperlink_OnClick(object sender, RoutedEventArgs e)
        {
            var context = (MainModel) DataContext;
            context.ProjectNameEditValue = context.ProjectName;
            FlyoutBase.ShowAttachedFlyout((FrameworkElement) sender);
        }

        private void FlyoutInputOnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                (sender as TextBox).GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void MainWindowName_Loaded(object sender, RoutedEventArgs e)
        {
            NavView.SelectedItem = NavView.Items.OfType<HamburgerMenuItem>().First();
            Navigate(NavView.SelectedItem);
        }
    }
}