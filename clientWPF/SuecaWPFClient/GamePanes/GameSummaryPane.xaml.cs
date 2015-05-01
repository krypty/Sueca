using System.Windows.Controls;

namespace suecaWPFClient
{
    /// <summary>
    /// Interaction logic for GameSummaryPane.xaml
    /// </summary>
    public partial class GameSummaryPane : GameStatePaneA
    {
        public GameSummaryPane()
        {
            InitializeComponent();
        }

        protected override void Quit()
        {
            throw new System.NotImplementedException();
        }
    }
}
