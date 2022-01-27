using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku.Services;
using Sudoku.ViewModels;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;
using System.Linq;

namespace SudokuTest
{

    [TestClass]
    public class UndoRedoTest
    {

        private SudokuGridViewModel grille;

        string start, beforeUndo, afterUndo, beforeUndo2,afterUndo2,beforeUndo3,afterUndo3,redoLittle,redoColor,redoBig;

        [TestInitialize]

        public void SetUp() 
        { 


            grille = new SudokuGridViewModel();

            BackupSystem.SaveOnStacks();
            BackupSystem.BackupActivation("start.txt");
            
            //littleNumber(annotations)
            
            grille.LittleGridViewModels[0].littlecaseList[0].lnvm.LittleNumber1 = "1";
            //grille.LittleGridViewModels[0].littlecaseList[0].bnvm.BigNumber = "1";
            BackupSystem.BackupActivation("beforeUndo.txt"); //devrait etre different
            BackupSystem.Undo();
            BackupSystem.BackupActivation("afterUndo.txt");//devrait etre identique
            BackupSystem.Redo();
            BackupSystem.BackupActivation("redoLittle.txt");
            BackupSystem.Undo();

            //couleur

            grille.LittleGridViewModels[0].littlecaseList[0].CurrentColor = "Aquamarine";
            BackupSystem.BackupActivation("beforeUndo2.txt");
            BackupSystem.Undo();
            BackupSystem.BackupActivation("afterUndo2.txt");
            BackupSystem.Redo();
            BackupSystem.BackupActivation("redoColor.txt");
            BackupSystem.Undo();

            //bigNumber (nombre principal)

            grille.LittleGridViewModels[0].littlecaseList[0].bnvm.BigNumber = "1";
            BackupSystem.BackupActivation("beforeUndo3.txt");
            BackupSystem.Undo();
            BackupSystem.BackupActivation("afterUndo3.txt");
            BackupSystem.Redo();
            BackupSystem.BackupActivation("redoBig.txt");
            BackupSystem.Undo();




            start = File.ReadAllText("start.txt");
            beforeUndo = File.ReadAllText("beforeUndo.txt");
            afterUndo = File.ReadAllText("afterUndo.txt");
            beforeUndo2 = File.ReadAllText("beforeUndo2.txt");
            afterUndo2 = File.ReadAllText("afterUndo2.txt");
            beforeUndo3 = File.ReadAllText("beforeUndo3.txt");
            afterUndo3 = File.ReadAllText("afterUndo3.txt");
            redoBig = File.ReadAllText("redoBig.txt");
            redoLittle = File.ReadAllText("redoLittle.txt");
            redoColor = File.ReadAllText("redoColor.txt");


        }


        public void TestUndo(string test1, string test2, bool expected)
        {
            //Act 
            bool b;
            if (test1 == test2)
            {
                b = true;
            }
            else
                b = false;
            //Assert
            Assert.AreEqual(b, expected);

        }


         
        [TestMethod]
        public void Undo() => TestUndo(start, afterUndo,true);
        
        [TestMethod]
        public void Undo2() => TestUndo(start,beforeUndo, false);
        [TestMethod]
        public void Undo3() => TestUndo(start, beforeUndo2, false);

        [TestMethod]
        public void Undo4() => TestUndo(start, afterUndo2, true);

        [TestMethod]
        public void Undo5() => TestUndo(start, beforeUndo3, false);

        [TestMethod]
        public void Undo6() => TestUndo(start, afterUndo3, true);

        [TestMethod]
        public void Redo() => TestUndo(beforeUndo, redoLittle, true);
        [TestMethod]
        public void Redo2() => TestUndo(beforeUndo2, redoColor, true);

        [TestMethod]
        public void Redo3() => TestUndo(beforeUndo3, redoBig, true);

        /*
        /*
        [TestMethod]
        public void Redo() => TestRedo(afterRedo);
        */

    }
}


