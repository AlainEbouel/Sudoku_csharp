using Sudoku.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;

namespace Sudoku.Services
{
    public class BackupSystem
    {
        public static Stack<string> UndoStack { get; set; } = new Stack<string>();
        public static Stack<string> RedoStack { get; set; } = new Stack<string>();
        public static bool StackIsLocked { get; set; }
        public static bool Undo_redo_is_Active { get; set; }


        public static void Undo()
        {
            StackIsLocked = true;
            Undo_redo_is_Active = true;
            RedoStack.Push(UndoStack.Pop());

            Load(UndoStack.Peek());
            StackIsLocked = false;
            Undo_redo_is_Active = false;
        }

        public static void Redo()
        {
            StackIsLocked = true;
            Undo_redo_is_Active = true;
            UndoStack.Push(RedoStack.Pop());

            Load(UndoStack.Peek());
            StackIsLocked = false;
            Undo_redo_is_Active = false;
        }

        public static void SaveOnStacks()
        {
            if (StackIsLocked == false)
            {
                UndoStack.Push(Save(SudokuView.littleGridViewModels));

                if (RedoStack.Count > 0)
                {
                    RedoStack.Clear();
                }
            }
        }

        public static void BackupActivation()
        {
            string DataToSave;

            DataToSave = Save(SudokuView.littleGridViewModels);
            File.WriteAllText(@"backupFile.txt", DataToSave);
        }


        //methode pour les tests unitaires

        public static void BackupActivation(string fileName)
        {
            string DataToSave;

            DataToSave = Save(SudokuView.littleGridViewModels);
            File.WriteAllText(@fileName, DataToSave);
        }


        public static void LoadingActivation(string fileName)
        {
            string savedData;

            try
            {
                savedData = File.ReadAllText(@fileName);
            }
            catch (FileNotFoundException)
            {
                return;
            }

            Load(savedData);

            UndoStack.Push(savedData);

            RedoStack.Clear();
        }

        private static string Save(ObservableCollection<LittleSudokuView> listOfLittleNumber)
        {
            XmlDocument gameData = new XmlDocument();

            XmlNode sudoku = gameData.CreateElement("Sudoku");
            gameData.AppendChild(sudoku);

            XmlNode littleSudoku = gameData.CreateElement("LittleSudoku");
            sudoku.AppendChild(littleSudoku);

            foreach (LittleSudokuView l in listOfLittleNumber)
            {
                XmlNode childLittleSudoku = gameData.CreateElement("ChildLittleSudoku");
                littleSudoku.AppendChild(childLittleSudoku);

                foreach (IndividualCaseView ind in l.IndividualCaseViewModels)
                {
                    XmlNode individualCase = gameData.CreateElement("IndividualCase");

                    /*Sauvegarde des bigNumbers*/
                    XmlAttribute BgNbr = gameData.CreateAttribute("id3");
                    XmlAttribute color = gameData.CreateAttribute("id2");

                    BgNbr.Value = ind.value.ToString();
                    color.Value = ind.CurrentColor;

                    individualCase.Attributes.Append(BgNbr);
                    individualCase.Attributes.Append(color);
                    childLittleSudoku.AppendChild(individualCase);

                    /*Sauvegarde des commentaires et notes dans les coins*/
                    foreach (string LNumber in ind.lnvm.GetNumbers())
                    {
                        XmlNode LNumbers = gameData.CreateElement("LNumbers");
                        XmlAttribute lnumber = gameData.CreateAttribute("lnumber");

                        lnumber.Value = LNumber;
                        LNumbers.Attributes.Append(lnumber);
                        individualCase.AppendChild(LNumbers);
                    }
                }
            }
            return gameData.InnerXml;
        }

        private static void Load(string savedData)
        {
            List<string> dataList = new List<string>();
            int i = 0;

            XmlDocument gameData = new XmlDocument();

            gameData.LoadXml(savedData);

            foreach (XmlNode little in gameData.SelectNodes("/Sudoku/LittleSudoku/ChildLittleSudoku"))
            {
                foreach (XmlNode individual in little.SelectNodes("IndividualCase"))
                {
                    dataList.Add(individual.Attributes["id3"].Value);
                    dataList.Add(individual.Attributes["id2"].Value);

                    foreach (XmlNode lnumber in individual.SelectNodes("LNumbers"))
                    {
                        if (lnumber.Attributes["lnumber"].Value == "")
                            dataList.Add("0");
                        else
                            dataList.Add(lnumber.Attributes["lnumber"].Value);
                    }
                }
            }
            
            foreach (LittleSudokuView little in SudokuView.littleGridViewModels)
            {
                foreach (IndividualCaseView ind in little.IndividualCaseViewModels)
                {
                    if (dataList[i] == "0")
                    {
                        ind.bnvm.BigNumber = "";
                        i++;
                    }
                    else
                    {
                        ind.bnvm.BigNumber = dataList[i++];
                    }
                    ind.CurrentColor = dataList[i++];

                    string[] arrayLnumber = new string[ind.lnvm.GetNumbers().Count];
                    

                    for (int j = 0; j < ind.lnvm.GetNumbers().Count; j++)
                    {
                        if (dataList[i] == "0")
                        {
                            arrayLnumber[j] = "";
                            i++;
                        }
                        else
                        {
                            arrayLnumber[j] = dataList[i++];
                        }
                    }
                    
                    ind.lnvm.LittleNumber1 = arrayLnumber[0];
                    ind.lnvm.LittleNumber2 = arrayLnumber[1];
                    ind.lnvm.LittleNumber3 = arrayLnumber[2];
                    ind.lnvm.LittleNumber4 = arrayLnumber[3];
                    ind.lnvm.LittleNumber5 = arrayLnumber[4];
                                       
                }
            }
            
        }
    }
}
