using System.Windows;

namespace CodeWanda.UI.Main.Pages.ControlFlow
{
    /// <summary>
    /// Interaction logic for ControlFlowPage.xaml
    /// </summary>
    public partial class ControlFlowPage
    {
        private static int index;

        public ControlFlowPage()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainModel model)
            {
                model.ContextTreeDetails.Add(new AnalysisContext {Demo = $"Demo Value {++index}"});
            }
        }
    }
}