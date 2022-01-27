using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sudoku.Views
{
    /// <summary>
    /// Interaction logic for BigNumberView.xaml
    /// </summary>
    public partial class BigNumberView : UserControl
    {
        public BigNumberView()
        {
            InitializeComponent();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^1-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
 
    }
}
