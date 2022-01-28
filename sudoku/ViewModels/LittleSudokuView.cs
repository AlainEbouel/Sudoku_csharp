using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Sudoku.ViewModels
{
    public class LittleSudokuView : BaseViewModel
    {
        private ObservableCollection<BaseViewModel> individualCaseViewModels;
        public List<IndividualCaseView> IndividualCaseList { get; set; }
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
        public LittleSudokuView()
        {
            individualCaseViewModels = new ObservableCollection<BaseViewModel>();
            Name = this.GetType().Name;
            IndividualCaseList = new List<IndividualCaseView>();
            InitViews();
        }

        private void InitViews()
        {
            IndividualCaseViewModels = new ObservableCollection<BaseViewModel>();
            for (int i = 0; i != 9; i++)
            {
                IndividualCaseView individualCaseViewModel = new IndividualCaseView();
                IndividualCaseViewModels.Insert(i, individualCaseViewModel);
                IndividualCaseList.Add(individualCaseViewModel);
            }
        }
        public IndividualCaseView getIndividualCase(int index)
        {
            return (IndividualCaseView)IndividualCaseViewModels[index];
        }

    }
}
