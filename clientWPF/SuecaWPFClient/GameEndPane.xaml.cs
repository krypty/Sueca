using System.Windows.Controls;

namespace suecaWPFClient
{
    /// <summary>
    /// Interaction logic for GameEndPane.xaml
    /// </summary>
    public partial class GameEndPane : GameStatePaneA
    {
        public GameEndPane()
        {
            InitializeComponent();
        }

        protected override void Quit()
        {
            throw new System.NotImplementedException();
        }
    }
}
