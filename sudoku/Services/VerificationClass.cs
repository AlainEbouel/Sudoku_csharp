using Sudoku.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Sudoku.Services
{
    public class VerificationClass
    {
        //public static int[,] values2evaluate = new int[9, 9];
        private int[,] values2evaluate { get; set; }
        private int[,] redcases { get; set; }
        public static ObservableCollection<LittleSudokuGridViewModel> LittleGridList;


        // Pour les teste unitaire, au lieu de recréer un nouvelle classe
        public void SetGrid(int[,] newGrid)
        {
            values2evaluate = newGrid;
        }

        public VerificationClass(ObservableCollection<LittleSudokuGridViewModel> gameActu)
        {
            LittleGridList = gameActu;

            values2evaluate = new int[9, 9]{
                                     { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                     { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                     { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                     { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                     { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                     { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                     { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                     { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                     { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    }; 
        }

        public bool Verification()
        {
            int count = 0;
            redcases = new int[9, 9]{
                                     { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                     { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                     { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                     { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                     { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                     { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                     { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                     { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                     { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                    };
            for (int i = 0; i < 9; i++)
            {

                if (ValuesRow(i, Collection2Int()))
                    count++;

                if (ValuesCol(i, Collection2Int()))
                    count++;

                if (ValuesSquares(i, Collection2Int()))
                    count++;
            }
            if (count != 0)
                return false;
            else
                return true;
        }
        private int[,] Collection2Int()
        {
            foreach (LittleSudokuGridViewModel littlegrid in LittleGridList)
            {
                foreach (IndividualCaseViewModel individual in littlegrid.IndividualCaseViewModels)
                {
                    int i = LittleGridList.IndexOf(littlegrid);
                    int j = littlegrid.IndividualCaseViewModels.IndexOf(individual);

                    values2evaluate[i, j] = individual.InputCase();
                    //Debug.WriteLine(values2evaluate[i, j].ToString());
                    /* var input = individual.InputCase();
                      values2evaluate[i, j] = input;*/


                }
            }
            return values2evaluate;
        }
        private bool ValuesRow(int row, int[,] grid)
        {
            int count = 0;
            int[] numbers = GetRow(grid, row);
            int x;
            int i = 0;
            IEnumerable<int> num = numbers;

            foreach (int number in num)
            {
                x = num.Count(n => n == number);
                if ((x > 1) && (number != 0))
                {
                    redcases[row, i] = 1;
                    count++;
                }
                i++;
            }
            if (count != 0)
                return false;
            else
                return true;
        }

        private bool ValuesCol(int col, int[,] grid)
        {
            int count = 0;
            int[] numbers = GetColumn(grid, col);
            int x;
            int i = 0;
            IEnumerable<int> num = numbers;

            foreach (int number in num)
            {
                x = num.Count(n => n == number);
                if ((x > 1) && (number != 0))
                {
                    redcases[i,col] = 1;
                    count++;
                }
                i++;
            }
            if (count != 0)
                return false;
            else
                return true;
        }

        /*private bool ValidSubsquare(int[,] grid)
        {
            int count = 0;
            for (int row = 0; row < 9; row = row + 3)
            {
                for (int col = 0; col < 9; col = col + 3)
                {
                    ISet<int> set = new HashSet<int>();
                    for (int r = row; r < row + 3; r++)
                    {
                        for (int c = col; c < col + 3; c++)
                        {
                            // Checking for repeated values.
                            if (grid[r, c] != 0)
                            {
                                if (set.Add(grid[r, c]) == false)
                                {
                                    redcases[r, c] = 1;
                                    count++;
                                }
                            }
                        }
                    }
                }
            }
            if (count != 0)
                return false;
            else
                return true;
        }*/
        private bool ValuesSquares(int square, int[,] grid)
        {
            int count = 0;
            int[] numbers = GetSquare(grid, square);
            int x;
            int i = 0;
            IEnumerable<int> num = numbers;

            foreach (int number in num)
            {
                x = num.Count(n => n == number);
                if ((x > 1) && (number != 0))
                {
                    redcases[(int)(Math.Floor((decimal)square / 3) * 3) + (int)(Math.Floor((decimal)i / 3)), ((square % 3) * 3) + (i % 3)] = 1;
                    count++;
                }
                i++;
            }
            if (count != 0)
                return false;
            else
                return true;
        }
        private int[] GetColumn(int[,] matrix, int columnNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(0))
                    .Select(x => matrix[x, columnNumber])
                    .ToArray();
        }

        private int[] GetRow(int[,] matrix, int rowNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(1))
                    .Select(x => matrix[rowNumber, x])
                    .ToArray();
        }
        private int[] GetSquare(int[,] matrix, int squareNumber)
        {
            int[] squares = new int[9];
            int k = (int)Math.Floor((decimal)squareNumber / 3) * 3;
            int l = (squareNumber % 3) * 3;
            int pos = 0;

            for (var i = k; i < k + 3; i++)
            {
                for (int j = l; j < l + 3; j++)
                {
                    if (pos < 9)
                    {
                        squares[pos] = matrix[i, j];
                        pos++;
                    }
                }
            }
            return squares;
        }
        public int[,] getRedCases()
        {
            return redcases;
        }
    }
}