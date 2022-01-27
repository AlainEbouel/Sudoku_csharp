using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Sudoku.ViewModels
{
    public class LittleSudokuGridViewModel : BaseViewModel
    {
        private ObservableCollection<BaseViewModel> individualCaseViewModels;
        public List<IndividualCaseViewModel> littlecaseList { get; set; }
        public ObservableCollection<BaseViewModel> IndividualCaseViewModels
        {
            get
            {
                return individualCaseViewModels;
            }
            set
            {
                individualCaseViewModels = value;
                OnPropertyChanged();
            }
        }
        public LittleSudokuGridViewModel()
        {
            individualCaseViewModels = new ObservableCollection<BaseViewModel>();
            Name = this.GetType().Name;
            littlecaseList = new List<IndividualCaseViewModel>();
            InitViews();
        }

        private void InitViews()
        {
            IndividualCaseViewModels = new ObservableCollection<BaseViewModel>();
            for (int i = 0; i != 9; i++)
            {
                IndividualCaseViewModel individualCaseViewModel = new IndividualCaseViewModel();
                IndividualCaseViewModels.Insert(i, individualCaseViewModel);
                littlecaseList.Add(individualCaseViewModel);
            }
        }
        public IndividualCaseViewModel getIndividualCase(int index)
        {
            return (IndividualCaseViewModel)IndividualCaseViewModels[index];
        }

    }
}
