﻿using Sudoku.Commands;
using Sudoku.Models;
using Sudoku.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Sudoku.ViewModels
{
    //[Serializable]
    public class IndividualCaseView : BaseViewModel
    {
        private BaseViewModel currentViewModel;
        private List<BaseViewModel> viewModels;
        private int count;
        public BigNumberView bnvm { get; set; }
        public LittleNumberView lnvm { get; set; }
        public int value { get; set; }
        private string currentColor;
        private string borderColor;
        public bool IsSelected { get; set; } 
        public int id { get; set; }
     
        public List<List<IndividualCaseView>> BelongingList { get; set; }
    
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
        public IndividualCaseView()
        {
            Name = this.GetType().Name;
            CurrentColor = "transparent";
            BorderColor = "transparent";
            ChangePageCommand = new DelegateCommand<string>(ChangePage);
            ColorPickerCommand = new DelegateCommand<string>(ColorPicker);
            SelectedCommand = new DelegateCommand<string>(Selected);
            BelongingList = new List<List<IndividualCaseView>>();
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
                SudokuView.selectedCase.Add(bnvm);
                SudokuView.selectedCase2.Add(lnvm);
               
                //InputSelected();
            }
            else
            {
                BorderColor = "transparent";
                bnvm.IsSelected = false;
                lnvm.IsSelected = false;
                IsSelected = false;

                IEnumerable<BigNumberView> bigN = new List<BigNumberView>();
                IEnumerable<LittleNumberView> littleN = new List<LittleNumberView>();                
                
                bigN = SudokuView.selectedCase.Where(a => a != bnvm);                
                littleN = SudokuView.selectedCase2.Where(a => a != lnvm);

                SudokuView.selectedCase = new List<BigNumberView>();
                SudokuView.selectedCase2 = new List<LittleNumberView>();

                foreach (var bN in bigN)
                    SudokuView.selectedCase.Add(bN);

                foreach (var lN in littleN)
                    SudokuView.selectedCase2.Add(lN);
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
                        new SudokuView(new ObservableCollection<BaseViewModel>()).ApplyColors(CurrentColor);
                    }
                    break;
                case 1:
                    CurrentColor = ColorPickerService.BlueViolet.ToString();
                    if (IsSelected)
                    {
                        new SudokuView(new ObservableCollection<BaseViewModel>()).ApplyColors(CurrentColor);
                    }
                    break;
                case 2:
                    CurrentColor = ColorPickerService.Brown.ToString();
                    if (IsSelected)
                    {
                        new SudokuView(new ObservableCollection<BaseViewModel>()).ApplyColors(CurrentColor);
                    }
                    break;
                case 3:
                    CurrentColor = ColorPickerService.Lime.ToString();
                    if (IsSelected)
                    {
                        new SudokuView(new ObservableCollection<BaseViewModel>()).ApplyColors(CurrentColor);
                    }
                    break;
                case 4:
                    CurrentColor = ColorPickerService.Orange.ToString();
                    if (IsSelected)
                    {
                        new SudokuView(new ObservableCollection<BaseViewModel>()).ApplyColors(CurrentColor);
                    }
                    break;
                case 5:
                    CurrentColor = ColorPickerService.SeaGreen.ToString();
                    if (IsSelected)
                    {
                        new SudokuView(new ObservableCollection<BaseViewModel>()).ApplyColors(CurrentColor);
                    }
                    break;
                case 6:
                    CurrentColor = ColorPickerService.RoyalBlue.ToString();
                    if (IsSelected)
                    {
                        new SudokuView(new ObservableCollection<BaseViewModel>()).ApplyColors(CurrentColor);
                    }
                    break;
                case 7:
                    CurrentColor = ColorPickerService.Gold.ToString();
                    if (IsSelected)
                    {
                        new SudokuView(new ObservableCollection<BaseViewModel>()).ApplyColors(CurrentColor);
                    }
                    break;
                case 8:
                    CurrentColor = ColorPickerService.DarkTurquoise.ToString();
                    if (IsSelected)
                    {
                        new SudokuView(new ObservableCollection<BaseViewModel>()).ApplyColors(CurrentColor);
                    }
                    break;
            }
            count++;
        }

        private void initViewModels()
        {
            bnvm = new BigNumberView(new BigNumber(), this);
           // bnvm.BigNumber = SudokuView.Sudoku_id++.ToString();
            ViewModels.Add(bnvm);
            lnvm = new LittleNumberView();
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