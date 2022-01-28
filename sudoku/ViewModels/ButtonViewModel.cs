using Sudoku.Commands;
using Sudoku.Services;
using System;
using System.Collections.ObjectModel;

namespace Sudoku.ViewModels
{
    class ButtonViewModel : BaseViewModel
    {
        public DelegateCommand<string> SaveCommand { get; set; }
        public DelegateCommand<string> LoadCommand { get; set; }
        public DelegateCommand<string> UndoCommand { get; set; }
        public DelegateCommand<string> RedoCommand { get; set; }
        public DelegateCommand<string> MultipleCommand { get; set; }
        public int activationCount = 0;
        //public static bool isActive;
        private string _ButtonText;

        public string ButtonText
        {
            get { return _ButtonText ?? (_ButtonText = "Reset MultipleSelection"); }
            set
            {
                _ButtonText = value;
                OnPropertyChanged();
            }
        }
        public ButtonViewModel()
        {
            Name = this.GetType().Name;
            SaveCommand = new DelegateCommand<string>(Save);
            LoadCommand = new DelegateCommand<string>(Load);
            UndoCommand = new DelegateCommand<string>(Undo);
            RedoCommand = new DelegateCommand<string>(Redo);
            MultipleCommand = new DelegateCommand<string>(MultipleSelection);
        }
        private void Load(string obj)
        {
            BackupSystem.StackIsLocked = true;
            BackupSystem.LoadingActivation("backupFile.txt");
            BackupSystem.StackIsLocked = false;
        }
        private void Save(string obj)
        {
            BackupSystem.BackupActivation();
        }

        private void Undo(string obj)
        {
            if (BackupSystem.UndoStack.Count > 1)
            {                
                BackupSystem.Undo();               
            }
        }

        private void Redo(string obj)
        {
            if (BackupSystem.RedoStack.Count > 0)
            {               
                BackupSystem.Redo();               
            }
        }

        private void MultipleSelection(string obj)
        {
            SudokuView.ResetMultiSelection();

            /* if(activationCount % 2 == 0)
             {
                 ButtonText = "MultipleSelection(activated)";
                 isActive = true;
             }
             else
             {
                 ButtonText = "MultipleSelection";
                 isActive = false;              
                // new SudokuGridViewModel(new ObservableCollection<BaseViewModel>()).FreeModifs();  
             }
             activationCount++;*/

        }
    }
}