using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Level_Library
{
    public class MyLevels
    {
        public int[,] Values { get; set; }
 
        public MyLevels(int[,] values)
        {
            Values = values;
        }
        public int GetValue(int row, int column)
        {
            return Values[row, column];
        }
 
        public int Rows
        {
            get
            {
                return Values.GetLength(0);
            }
        }
 
        public int Columns
        {
            get
            {
                return Values.GetLength(1);
            }
        }
    }
}
