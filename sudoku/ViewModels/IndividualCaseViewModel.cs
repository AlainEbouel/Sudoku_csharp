using Sudoku.Commands;
using Sudoku.Models;
using Sudoku.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Sudoku.ViewModels
{
    public class IndividualCaseViewModel : BaseViewModel
    {
        private BaseViewModel currentViewModel;
        private List<BaseViewModel> viewModels;
        private int count;
        public BigNumberViewModel bnvm { get; set; }
        public LittleNumberViewModel lnvm { get; set; }
        public int value { get; set; }
        private string currentColor;
        private string borderColor;
        public bool IsSelected { get; set; } 
    
        public DelegateCommand<string> ChangePageCommand { get; set; }
        public DelegateCommand<string> ColorPickerCommand { get; set; }
        public DelegateCommand<string> SelectedCommand { get; set; }
        //public bool selected { get; set; }

        public string CurrentColor
        {
            get { return currentColor; }
            set
            {
                currentColor = value;
                OnPropertyChanged("CurrentColor");
                BackupSystem.SaveOnStacks();
            }
        }

        public string BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                OnPropertyChanged("BorderColor");
            }
        }
        public BaseViewModel CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                currentViewModel = value;
                OnPropertyChanged();
            }
        }
        public List<BaseViewModel> ViewModels
        {
            get
            {
                if (viewModels == null)
                    viewModels = new List<BaseViewModel>();
                return viewModels;
            }
        }
        public int Count
        {
            get { return count; }
            set
            {
                count = value;
                OnPropertyChanged();
            }
        }
        public IndividualCaseViewModel()
        {
            Name = this.GetType().Name;
            CurrentColor = "transparent";
            BorderColor = "transparent";
            ChangePageCommand = new DelegateCommand<string>(ChangePage);
            ColorPickerCommand = new DelegateCommand<string>(ColorPicker);
            SelectedCommand = new DelegateCommand<string>(Selected);
            count = 0;
            initViewModels();
            CurrentViewModel = ViewModels[0];
        }

        private void Selected(string obj)
        {
           // if (ButtonViewModel.isActive)
           // {
            if (IsSelected == false)
            {
                bnvm.IsSelected = true;
                lnvm.IsSelected = true;
                IsSelected = true;
                //selected = true;
                changeBorderColor("black");
                SudokuGridViewModel.selectedCase.Add(bnvm);
                SudokuGridViewModel.selectedCase2.Add(lnvm);
               
                //InputSelected();
            }
            else
            {
                BorderColor = "transparent";
                bnvm.IsSelected = false;
                lnvm.IsSelected = false;
                IsSelected = false;

                IEnumerable<BigNumberViewModel> bigN = new List<BigNumberViewModel>();
                IEnumerable<LittleNumberViewModel> littleN = new List<LittleNumberViewModel>();                
                
                bigN = SudokuGridViewModel.selectedCase.Where(a => a != bnvm);                
                littleN = SudokuGridViewModel.selectedCase2.Where(a => a != lnvm);

                SudokuGridViewModel.selectedCase = new List<BigNumberViewModel>();
                SudokuGridViewModel.selectedCase2 = new List<LittleNumberViewModel>();

                foreach (var bN in bigN)
                    SudokuGridViewModel.selectedCase.Add(bN);

                foreach (var lN in littleN)
                    SudokuGridViewModel.selectedCase2.Add(lN);
            }
          
        }


        private void ColorPicker(string obj)
        {
            if (count == 8)
            {
                count = 0;
            }
            switch (count)
            {
                case 0:
                    CurrentColor = ColorPickerService.Aquamarine.ToString();
                    if (IsSelected)
                    {
                        new SudokuGridViewModel(new ObservableCollection<BaseViewModel>()).ApplyColors(CurrentColor);
                    }
                    break;
                case 1:
                    CurrentColor = ColorPickerService.BlueViolet.ToString();
                    if (IsSelected)
                    {
                        new SudokuGridViewModel(new ObservableCollection<BaseViewModel>()).ApplyColors(CurrentColor);
                    }
                    break;
                case 2:
                    CurrentColor = ColorPickerService.Brown.ToString();
                    if (IsSelected)
                    {
                        new SudokuGridViewModel(new ObservableCollection<BaseViewModel>()).ApplyColors(CurrentColor);
                    }
                    break;
                case 3:
                    CurrentColor = ColorPickerService.Lime.ToString();
                    if (IsSelected)
                    {
                        new SudokuGridViewModel(new ObservableCollection<BaseViewModel>()).ApplyColors(CurrentColor);
                    }
                    break;
                case 4:
                    CurrentColor = ColorPickerService.Orange.ToString();
                    if (IsSelected)
                    {
                        new SudokuGridViewModel(new ObservableCollection<BaseViewModel>()).ApplyColors(CurrentColor);
                    }
                    break;
                case 5:
                    CurrentColor = ColorPickerService.SeaGreen.ToString();
                    if (IsSelected)
                    {
                        new SudokuGridViewModel(new ObservableCollection<BaseViewModel>()).ApplyColors(CurrentColor);
                    }
                    break;
                case 6:
                    CurrentColor = ColorPickerService.RoyalBlue.ToString();
                    if (IsSelected)
                    {
                        new SudokuGridViewModel(new ObservableCollection<BaseViewModel>()).ApplyColors(CurrentColor);
                    }
                    break;
                case 7:
                    CurrentColor = ColorPickerService.Gold.ToString();
                    if (IsSelected)
                    {
                        new SudokuGridViewModel(new ObservableCollection<BaseViewModel>()).ApplyColors(CurrentColor);
                    }
                    break;
                case 8:
                    CurrentColor = ColorPickerService.DarkTurquoise.ToString();
                    if (IsSelected)
                    {
                        new SudokuGridViewModel(new ObservableCollection<BaseViewModel>()).ApplyColors(CurrentColor);
                    }
                    break;
            }
            count++;
        }

        private void initViewModels()
        {
            bnvm = new BigNumberViewModel(new BigNumber());
            ViewModels.Add(bnvm);
            lnvm = new LittleNumberViewModel();
            ViewModels.Add(lnvm);
        }
        private void ChangePage(string pageName)
        {
            if (CurrentViewModel == ViewModels[1])
            {
                CurrentViewModel = ViewModels[0];
            }
            else
            {
                CurrentViewModel = ViewModels[1];
            }
        }

        internal void changeColor(string color)
        {
            CurrentColor = color;
        }

        internal void changeBorderColor(string color)
        {
            BorderColor = color;
        }

        public int InputCase()
        {
            string defaultValue = "0";

            if (bnvm.BigNumber == "")
            {
                value = Convert.ToInt32(defaultValue);
            }
            else
            {
                value = Convert.ToInt32(bnvm.BigNumber);
            }
            return value;
        }

    }
}