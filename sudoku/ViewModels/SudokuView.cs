﻿using Sudoku.Models;
using Sudoku.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Sudoku.ViewModels
{
    public class SudokuView : BaseViewModel
    {
        public static ObservableCollection<LittleSudokuView> littleGridViewModels { get; private set; }
        private static VerificationClass VerificationApp;
        public static VerificationReviewed VerificationSystem { get; set; }
        //public static string MultSelect { get; set; }        

        public static List<BigNumberView> selectedCase = new List<BigNumberView>();
        public static List<LittleNumberView> selectedCase2 = new List<LittleNumberView>();
        public static int Sudoku_id { get; set; } = 1;


        public ObservableCollection<LittleSudokuView> LittleGridViewModels
        {
            get
            {
                return littleGridViewModels;
            }
            set
            {
                littleGridViewModels = value;
                OnPropertyChanged();
            }
        }
        public SudokuView()
        {

            LittleGridViewModels = new ObservableCollection<LittleSudokuView>();
            Name = this.GetType().Name;
            InitViews();
            VerificationApp = new VerificationClass(LittleGridViewModels);


        }

        public SudokuView(ObservableCollection<BaseViewModel> littleGridViewModel)
        {

        }

        private void InitViews()
        {


            LittleGridViewModels = new ObservableCollection<LittleSudokuView>();
            for (int i = 0; i != 9; i++)
            {
                LittleGridViewModels.Insert(i, new LittleSudokuView());
            }
            BackupSystem.SaveOnStacks();
            VerificationSystem = new VerificationReviewed(LittleGridViewModels);




        }
        public void ApplyColors(string color)
        {
            foreach (var cases in LittleGridViewModels)
            {
                for (int i = 0; i < 9; i++)
                {
                    var checking = cases.getIndividualCase(i);
                    if (checking.IsSelected)
                    {
                        checking.changeColor(color);
                    }
                }
            }
        }
        public void ActiveVerification()
        {
            VerificationApp.Verification();
            int[,] temp = VerificationApp.getRedCases();
            int bigGrid = 0;
            int littleGrid = 0;
            foreach (int block in temp)
            {
                var current = LittleGridViewModels[bigGrid].getIndividualCase(littleGrid).CurrentColor;
                if (block == 1)
                {///TODO voire si la multisélection peut cohabiter avec la vérification
                   // LittleGridViewModels[bigGrid].getIndividualCase(littleGrid).changeColor("red");
                }
                else
                {
                    // LittleGridViewModels[bigGrid].getIndividualCase(littleGrid).changeColor(current);
                }
                littleGrid++;
                if (littleGrid % 9 == 0)
                {
                    bigGrid++;
                }
                if (littleGrid == 9)
                {
                    littleGrid = 0;
                }
                if (bigGrid == 9)
                {
                    bigGrid = 0;
                }
            }

        }

        public static void ApplyModifications(string bigNumber)
        {
            for (int i = 0; i < selectedCase.Count(); i++)
                selectedCase[i].IsSelected = false;

            BackupSystem.StackIsLocked = true;

            for (int i = 0; i < selectedCase.Count(); i++)
            {
                selectedCase[i].BigNumber = bigNumber;
            }

            BackupSystem.StackIsLocked = false;
            BackupSystem.SaveOnStacks();

            for (int i = 0; i < selectedCase.Count(); i++)
                selectedCase[i].IsSelected = true;
        }
        public static void ApplyModifications(int input, string LNumber)
        {
            for (int i = 0; i < selectedCase2.Count(); i++)
                selectedCase2[i].IsSelected = false;

            BackupSystem.StackIsLocked = true;

            switch (input)
            {
                case 1:
                    for (int i = 0; i < selectedCase.Count(); i++)
                    {
                        selectedCase2[i].LittleNumber1 = LNumber;
                    }
                    break;
                case 2:
                    for (int i = 0; i < selectedCase.Count(); i++)
                    {
                        selectedCase2[i].LittleNumber2 = LNumber;
                    }
                    break;
                case 3:
                    for (int i = 0; i < selectedCase.Count(); i++)
                    {
                        selectedCase2[i].LittleNumber3 = LNumber;
                    }
                    break;
                case 4:
                    for (int i = 0; i < selectedCase.Count(); i++)
                    {
                        selectedCase2[i].LittleNumber4 = LNumber;
                    }
                    break;
                case 5:
                    for (int i = 0; i < selectedCase.Count(); i++)
                    {
                        selectedCase2[i].LittleNumber5 = LNumber;
                    }
                    break;
            }

            BackupSystem.StackIsLocked = false;
            BackupSystem.SaveOnStacks();

            for (int i = 0; i < selectedCase2.Count(); i++)
                selectedCase2[i].IsSelected = true;

        }

        public static void ResetMultiSelection()
        {
            foreach (var l in littleGridViewModels)
                for (int i = 0; i < l.IndividualCaseList.Count; i++)
                {
                    if (l.IndividualCaseList[i].IsSelected == true)
                    {
                        l.IndividualCaseList[i].bnvm.IsSelected = false;
                        l.IndividualCaseList[i].lnvm.IsSelected = false;
                        l.IndividualCaseList[i].IsSelected = false;
                        l.IndividualCaseList[i].changeBorderColor("white");
                    }
                }
            selectedCase.RemoveAll(a => a != null);
            selectedCase2.RemoveAll(a => a != null);
        }

        public static void SerializeIndCase()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("serializedData.bin", FileMode.Create, FileAccess.Write, FileShare.None);         
     

            foreach (LittleSudokuView l in littleGridViewModels)
            {
                foreach (IndividualCaseView ind in l.IndividualCaseViewModels)
                {
                    string bN = ind.bnvm.BigNumber;
                    string l1 = ind.lnvm.LittleNumber1;
                    string l2 = ind.lnvm.LittleNumber2;
                    string l3 = ind.lnvm.LittleNumber3;
                    string l4 = ind.lnvm.LittleNumber4;
                    string l5 = ind.lnvm.LittleNumber5;
                    string currentColor = ind.CurrentColor;
                 
                    formatter.Serialize(stream, bN);
                    formatter.Serialize(stream, l1);
                    formatter.Serialize(stream, l2);
                    formatter.Serialize(stream, l3);
                    formatter.Serialize(stream, l4);
                    formatter.Serialize(stream, l5);
                    formatter.Serialize(stream, currentColor);
                }
            }
            stream.Close();
        }

        public static void DeserializedIndCase()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("serializedData.bin", FileMode.Open, FileAccess.Read);

            foreach (LittleSudokuView l in littleGridViewModels)
            {
                foreach (IndividualCaseView ind in l.IndividualCaseViewModels)
                {
                   ind.bnvm.BigNumber = (string)formatter.Deserialize(stream);
                   ind.lnvm.LittleNumber1 = (string)formatter.Deserialize(stream);  
                   ind.lnvm.LittleNumber2 = (string)formatter.Deserialize(stream);  
                   ind.lnvm.LittleNumber3 = (string)formatter.Deserialize(stream);  
                   ind.lnvm.LittleNumber4 = (string)formatter.Deserialize(stream);  
                   ind.lnvm.LittleNumber5 = (string)formatter.Deserialize(stream);  
                   ind.CurrentColor = (string)formatter.Deserialize(stream);                    
                }
            }                   
        }
    }

}