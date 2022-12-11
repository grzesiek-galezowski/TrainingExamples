using System.Windows.Controls;
using System.Windows.Media;

namespace Tb03Gui.Sequencer
{
    /// <summary>
    /// Interaction logic for SequencerPad.xaml
    /// </summary>
    public partial class SequencerPad : UserControl
    {
        public SequencerPad()
        {
            InitializeComponent();
        }

        public void Mark()
        {
          PadLabel.Background = new SolidColorBrush(Colors.AliceBlue);
        }

        public void Unmark()
        {
          PadLabel.Background = new SolidColorBrush(Colors.LightGray);
        }
    }
}
