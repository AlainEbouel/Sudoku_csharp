
using System;

namespace Sudoku.Models
{
    [Serializable]
    public class LittleNumberModel
    {
        public string LNumber { get; set; }
        public LittleNumberModel()
        { 
            LNumber = "";
        }
    }
}
