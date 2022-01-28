namespace Sudoku.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private BaseViewModel currentViewModel;
        private BaseViewModel buttonViewModel;
        private SudokuView sgvm;
        private ButtonViewModel bvm;

        public BaseViewModel CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                currentViewModel = value;
                OnPropertyChanged();
            }
        }
        public BaseViewModel ButtonViewModel
        {
            get { return buttonViewModel; }
            set
            {
                buttonViewModel = value;
                OnPropertyChanged();
            }
        }
        public MainViewModel()
        {
            initViewModels();
        }
        private void initViewModels()
        {
            sgvm = new SudokuView();
            bvm = new ButtonViewModel();
            CurrentViewModel = sgvm;
            ButtonViewModel = bvm;
        }

    }
}