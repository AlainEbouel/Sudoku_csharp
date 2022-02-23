using Sudoku.Models;
using Sudoku.Services;
using System;
using System.Collections.Generic;

namespace Sudoku.ViewModels
{
    [Serializable]
    public class LittleNumberView : BaseViewModel
    {
        private LittleNumberModel LittleNbr1 { get; set; }
        private LittleNumberModel LittleNbr2 { get; set; }
        private LittleNumberModel LittleNbr3 { get; set; }
        private LittleNumberModel LittleNbr4 { get; set; }
        private LittleNumberModel LittleNbr5 { get; set; }
        public bool IsSelected { get; set; }
        public static int input;
        public readonly int Id;


       /* public bool modified1 { get; set; }
        public bool modified2 { get; set; }
        public bool modified3 { get; set; }
        public bool modified4 { get; set; }
        public bool modified5 { get; set; }*/

        public string LittleNumber1
        {
            get => LittleNbr1.LNumber;
            set
            {
                if (IsSelected == true && BackupSystem.Undo_redo_is_Active == false)
                {
                    // SudokuGridViewModel.multSelect = value;
                    //IndividualCaseViewModel ind. = value;
                    input = 1;
                    SudokuView.ApplyModifications(input, value);
                }
                else
                {
                    LittleNbr1.LNumber = value;
                    OnPropertyChanged();
                    BackupSystem.SaveOnStacks();
                }
            }
        }

        public string LittleNumber2
        {
            get => LittleNbr2.LNumber;
            set
            {
                if (IsSelected == true && BackupSystem.Undo_redo_is_Active == false)
                {
                    //SudokuGridViewModel.multSelect  = value;
                    input = 2;
                    SudokuView.ApplyModifications(input, value);
                }
                else
                {
                    LittleNbr2.LNumber = value;
                    OnPropertyChanged();
                    BackupSystem.SaveOnStacks();
                }
            }
        }
        public string LittleNumber3
        {
            get => LittleNbr3.LNumber;
            set
            {

                if (IsSelected == true && BackupSystem.Undo_redo_is_Active == false)
                {
                   // SudokuGridViewModel.multSelect = value;
                    input = 3;
                    SudokuView.ApplyModifications(input, value);
                }
                else
                {
                    LittleNbr3.LNumber = value;
                    OnPropertyChanged();
                    BackupSystem.SaveOnStacks();
                }
            }
        }
        public string LittleNumber4
        {
            get => LittleNbr4.LNumber;
            set
            {
                if (IsSelected == true && BackupSystem.Undo_redo_is_Active == false)
                {
                   // SudokuGridViewModel.multSelect = value;
                    input = 4;
                    SudokuView.ApplyModifications(input, value);
                }
                else
                {
                    LittleNbr4.LNumber = value;
                    OnPropertyChanged();
                    BackupSystem.SaveOnStacks();
                }
            }
        }
        public string LittleNumber5
        {
            get => LittleNbr5.LNumber;
            set
            {
                if (IsSelected == true && BackupSystem.Undo_redo_is_Active == false)
                {
                   // SudokuGridViewModel.multSelect = value;
                    input = 5;
                    SudokuView.ApplyModifications(input, value);
                }
                else
                {
                    LittleNbr5.LNumber = value;
                    OnPropertyChanged();
                    BackupSystem.SaveOnStacks();
                }
            }
        }
        public LittleNumberView()
        {
            Name = this.GetType().Name;

            LittleNbr1 = new LittleNumberModel();
            LittleNbr2 = new LittleNumberModel();
            LittleNbr3 = new LittleNumberModel();
            LittleNbr4 = new LittleNumberModel();
            LittleNbr5 = new LittleNumberModel();

            SetNumbers();
        }
        private void SetNumbers()
        {
            BackupSystem.StackIsLocked = true;
            LittleNumber1 = "";
            LittleNumber2 = "";
            LittleNumber3 = "";
            LittleNumber4 = "";
            LittleNumber5 = "";
            BackupSystem.StackIsLocked = false;
        }

        public List<string> GetNumbers()
        {
            List<string> littleNumbersList = new List<string>();

            littleNumbersList.Add(LittleNumber1);
            littleNumbersList.Add(LittleNumber2);
            littleNumbersList.Add(LittleNumber3);
            littleNumbersList.Add(LittleNumber4);
            littleNumbersList.Add(LittleNumber5);

            return littleNumbersList;
        }
    }
}
