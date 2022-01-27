using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sudoku.Views
{
    /// <summary>
    /// Interaction logic for LittleNumberView.xaml
    /// </summary>
    public partial class LittleNumberView : UserControl
    {
        public LittleNumberView()
        {
            InitializeComponent();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^1-9]+");
            e.Handled = regex.IsMatch(e.Text);
            if (e.Text == TextBox1.Text || e.Text == TextBox2.Text || e.Text == TextBox4.Text || e.Text == TextBox5.Text)
            {
                e.Handled = true;
            }
            else if (TextBox3.Text.Contains(e.Text))
            {
                e.Handled = true;
            }
        }
    }
}
