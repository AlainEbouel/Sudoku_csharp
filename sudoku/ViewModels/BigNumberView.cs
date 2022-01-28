using Sudoku.Models;
using Sudoku.Services;
using System.Collections.ObjectModel;
using System.Linq;

namespace Sudoku.ViewModels
{
    public class BigNumberView : BaseViewModel
    {
        public bool IsSelected { get; set; }
        public readonly int Id;
        public IndividualCaseView ParentIndCase { get; set; }
        public BigNumberView(BigNumber number, IndividualCaseView parentIndCase)
        {
            ParentIndCase = parentIndCase;
            Name = this.GetType().Name;
            this.Numbers = number;
            IsSelected = false;
            
        }

        public BigNumber Numbers { get; private set; }
        public string BigNumber
        {
            get
            {
                return Numbers.Number;
            }
            set
            {
                if (IsSelected == true && BackupSystem.Undo_redo_is_Active == false)
                {
                    // SudokuGridViewModel.multSelect = Numbers.Number;
                    SudokuView.ApplyModifications(value);
                }
                else
                {
                    this.Numbers.Number = value;
                    this.OnPropertyChanged("BigNumber");
                    // valueModified = Numbers.Number;
                    //new SudokuView(new ObservableCollection<BaseViewModel>()).ActiveVerification();
                    BackupSystem.SaveOnStacks();
                  
                }
                    if (SudokuView.VerificationSystem != null)
                    SudokuView.VerificationSystem.Verification_2();
                
                
            }
        }
    }
}