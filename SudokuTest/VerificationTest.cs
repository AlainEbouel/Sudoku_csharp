using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku.Services;
using Sudoku.ViewModels;
using System.Collections.ObjectModel;

namespace SudokuTest
{
    [TestClass]
    public class VerificationTest
    {

        VerificationClass analyzer;

        public int[,] goodGrid = { { 5, 3, 0, 0, 0, 0, 0, 0, 0 },
                                  { 6, 0, 0, 0, 0, 0, 0, 0, 0 },
                                  { 0, 9, 8, 0, 0, 0, 0, 0, 0 },
                                  { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                  { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                  { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                  { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                  { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                  { 0, 0, 0, 0, 0, 0, 0, 0, 0 } };

        public int[,] badGrid = { { 5, 3, 0, 0, 7, 0, 0, 0, 0 }, 
                                { 6, 0, 0, 1, 9, 5, 0, 0, 0 }, 
                                { 0, 9, 8, 0, 0, 0, 0, 6, 0 }, 
                                { 8, 0, 0, 0, 6, 0, 0, 0, 3 }, 
                                { 4, 0, 0, 8, 1, 3, 0, 0, 1 }, 
                                { 7, 0, 0, 0, 2, 1, 0, 0, 6 }, 
                                { 0, 6, 0, 0, 0, 0, 2, 8, 0 }, 
                                { 0, 0, 0, 4, 1, 9, 0, 0, 5 }, 
                                { 0, 0, 0, 0, 8, 0, 0, 7, 9 } };

        public int[,] redCase = {{0,0,0,0,0,0,0,0,0 },
                                {0,0,0,0,0,0,0,0,0 },
                                {0,0,0,0,0,0,0,0,0 },
                                {0,0,0,0,0,0,0,0,0 },
                                {0,0,0,0,1,0,0,0,1 },
                                {0,0,0,0,0,1,0,0,0 },
                                {0,0,0,0,0,0,0,0,0 },
                                {0,0,0,0,1,0,0,0,0 },
                                {0,0,0,0,0,0,0,0,0 },};



        [TestInitialize]

        public void SetUp() => analyzer = new VerificationClass(new ObservableCollection<LittleSudokuView>());

        public void TestVerification(bool expected, int[,] grid)
        {
            //Act
            analyzer.SetGrid(grid);

            //Assert
            Assert.AreEqual(expected, analyzer.Verification());

        }

        public void TestRedCases(int [,] expected, int[,] grid)
        {
            //Act
            analyzer.SetGrid(grid);

            analyzer.Verification();
            var redCaseT = analyzer.getRedCases();

            //Assert
            Assert.AreEqual(expected, redCaseT);
        }




        [TestMethod]
        public void GoodGrid() => TestVerification(false, goodGrid);

        [TestMethod]
        public void BadGrid() => TestVerification(false, badGrid);

       /* [TestMethod]
        public void RedCaseGrid() => TestRedCases(redCase, badGrid);*/

    }
}
