﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Sudoku.ViewModels
{
    [Serializable]
    public class BaseViewModel : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
