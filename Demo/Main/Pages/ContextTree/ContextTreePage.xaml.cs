namespace CodeWanda.UI.Main.Pages.ContextTree
{
    /// <summary>
    /// Interaction logic for ContextTreePage.xaml
    /// </summary>
    public partial class ContextTreePage
    {
        private readonly AnalysisContext _context;
        private readonly AnalysisCollection _analysisCollection;
        
        public ContextTreePage(AnalysisContext context)
        {
            _context = context;

            InitializeComponent();
            
            _analysisCollection = (AnalysisCollection) Resources["GraphContextCollection"];
            
            _analysisCollection.Add(_context);
        }
    }
}