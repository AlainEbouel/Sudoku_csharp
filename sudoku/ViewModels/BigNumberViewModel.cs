using Sudoku.Models;
using Sudoku.Services;
using System.Collections.ObjectModel;

namespace Sudoku.ViewModels
{
    public class BigNumberViewModel : BaseViewModel
    {
        public bool IsSelected { get; set; }
        public readonly int Id;
        public BigNumberViewModel(BigNumber number)
        {
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
                    SudokuGridViewModel.ApplyModifications(value);
                }
                else
                {
                    this.Numbers.Number = value;
                    this.OnPropertyChanged("BigNumber");
                    // valueModified = Numbers.Number;
                    new SudokuGridViewModel(new ObservableCollection<BaseViewModel>()).ActiveVerification();
                    BackupSystem.SaveOnStacks();
                }
                
            }
        }
    }
}