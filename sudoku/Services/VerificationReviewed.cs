using Sudoku.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Sudoku.Services
{
    public class VerificationReviewed
    {
        private ObservableCollection<LittleSudokuView> LittleSudokuList { get; set; }
        public List<List<IndividualCaseView>> ListOfRowList;
        public List<List<IndividualCaseView>> ListOfColumnList;
        public List<IndividualCaseView> FalseIndCaseList { get; set; }
       

       /* private List<IndividualCaseView> Row1;
        private List<IndividualCaseView> Row2;
        private List<IndividualCaseView> Row3;
        private List<IndividualCaseView> Row4;
        private List<IndividualCaseView> Row5;
        private List<IndividualCaseView> Row6;
        private List<IndividualCaseView> Row7;
        private List<IndividualCaseView> Row8;
        private List<IndividualCaseView> Row9;

        private List<IndividualCaseView> Column1;
        private List<IndividualCaseView> Column2;
        private List<IndividualCaseView> Column3;
        private List<IndividualCaseView> Column4;
        private List<IndividualCaseView> Column5;
        private List<IndividualCaseView> Column6;
        private List<IndividualCaseView> Column7;
        private List<IndividualCaseView> Column8;*/
        private List<IndividualCaseView> Column9;

        public VerificationReviewed(ObservableCollection<LittleSudokuView> littleSudokuList)
        {
            LittleSudokuList = littleSudokuList;
            ListOfRowList = new List<List<IndividualCaseView>>();
            ListOfColumnList = new List<List<IndividualCaseView>>();
            FalseIndCaseList = new List<IndividualCaseView>();

            for(int i = 0; i < 9; i++)
            {
                ListOfRowList.Add(new List<IndividualCaseView>());
                ListOfColumnList.Add(new List<IndividualCaseView>());
            }
            LittleSudokuDistribution();
            
           /* Row1 = new List<IndividualCaseView>();
            Row2 = new List<IndividualCaseView>();
            Row3 = new List<IndividualCaseView>();
            Row4 = new List<IndividualCaseView>();
            Row5 = new List<IndividualCaseView>();
            Row6 = new List<IndividualCaseView>();
            Row7 = new List<IndividualCaseView>();
            Row8 = new List<IndividualCaseView>();
            Row9 = new List<IndividualCaseView>();

            Column1 = new List<IndividualCaseView>();
            Column2 = new List<IndividualCaseView>();
            Column3 = new List<IndividualCaseView>();
            Column4 = new List<IndividualCaseView>();
            Column5 = new List<IndividualCaseView>();
            Column6 = new List<IndividualCaseView>();
            Column7 = new List<IndividualCaseView>();
            Column8 = new List<IndividualCaseView>();
            Column9 = new List<IndividualCaseView>();*/
        }


        /*Repartir les individuals cases dans les listes*/
        private void LittleSudokuDistribution()
        {
           // LittleSudokuList[0].IndividualCaseList[0].BorderColor = "red";
            int column = 0;
            int row = 0;

            for(int i = 0; i < 3 ; i++)
            {
               Distribution(LittleSudokuList[i], column, row);
                column+=3;
            }

            column = 0;
            row += 3;
            for (int i = 3; i <6 ; i++)
            {                           
                Distribution(LittleSudokuList[i], column, row );
                column += 3;
            }

            column = 0;
            row += 3;
            for (int i = 6; i < 9 ; i++)
            {                              
                Distribution(LittleSudokuList[i], column, row);
                column += 3;
            }
        }

        private void Distribution(LittleSudokuView littleSudokuView, int column, int row)
        {
            int initialColumn = column;
            for (int i = 0; i < 3; i++)
            {
                IndividualCaseView indCase = littleSudokuView.IndividualCaseList[i];

                indCase.BelongingList.Add(littleSudokuView.IndividualCaseList);
                ListOfColumnList[column].Add(indCase);
                ListOfRowList[row].Add(indCase);

                indCase.BelongingList.Add(ListOfColumnList[column++]);
                indCase.BelongingList.Add(ListOfRowList[row]);

            }

            row += 1;
            column = initialColumn;
            for (int i = 3; i < 6; i++)
            {               
                IndividualCaseView indCase = littleSudokuView.IndividualCaseList[i];

                indCase.BelongingList.Add(littleSudokuView.IndividualCaseList);
                ListOfColumnList[column].Add(indCase);
                ListOfRowList[row].Add(indCase);

                indCase.BelongingList.Add(ListOfColumnList[column++]);
                indCase.BelongingList.Add(ListOfRowList[row]);

            }

            row += 1;
            column = initialColumn;
            for (int i = 6; i < 9; i++)
            {
                
                IndividualCaseView indCase = littleSudokuView.IndividualCaseList[i];

                indCase.BelongingList.Add(littleSudokuView.IndividualCaseList);
                ListOfColumnList[column].Add(indCase);
                ListOfRowList[row].Add(indCase);

                indCase.BelongingList.Add(ListOfColumnList[column++]);
                indCase.BelongingList.Add(ListOfRowList[row]);

            }
            

        }
        public void Verification_2()
        {
            foreach(var lsudoku in LittleSudokuList)
            {
                foreach (var indCase in lsudoku.IndividualCaseList)
                {

                    Verification_2(indCase);
                        
                }
            }
                

        }

        private void Verification_2(IndividualCaseView indCase)
        {
            bool isRed = false;
            foreach(var list in indCase.BelongingList)
            {
               foreach(var indC in list)
                    if(indCase != indC && indC.bnvm.BigNumber.Equals(indCase.bnvm.BigNumber) && indC.bnvm.BigNumber != "")
                    {
                        indC.BorderColor = "red";
                        indCase.BorderColor = "red";
                        FalseIndCaseList.Add(indC);
                        FalseIndCaseList.Add(indCase);
                        isRed = true;
                        break;
                    }
            }

            if (!isRed && indCase.BorderColor != "transparent")
                indCase.BorderColor = "transparent";


        }

        private object ApplyRedBorder(IndividualCaseView a)
        {
            throw new NotImplementedException();
        }
    }
}
