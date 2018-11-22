using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Tetris
{
     class Block
    {
        public int x = 0;
        public int y = -1; //coordinates of falling block
        public string[,] BlockArray = new string[height, width];
        public static int width = 4, height = 4;

        public static string[,] blockZ = new string[4, 4]
            {
                { "w","w","n","n"},
                { "n","w","w","n"},
                { "n","n","n","n"},
                { "n","n","n","n"}
            };
        public static string[,] blockS = new string[4, 4]
    {
                { "n","w","w","n"},
                { "w","w","n","n"},
                { "n","n","n","n"},
                { "n","n","n","n"}
    };
        public static string[,] blockT = new string[4, 4]
            {
                { "n","w","n","n"},
                { "w","w","w","n"},
                { "n","n","n","n"},
                { "n","n","n","n"}
            };
        public static string[,] blockO = new string[4, 4]
            {
                { "w","w","n","n"},
                { "w","w","n","n"},
                { "n","n","n","n"},
                { "n","n","n","n"}
            };
        public static string[,] blockI = new string[4, 4]
            {
                { "w","n","n","n"},
                { "w","n","n","n"},
                { "w","n","n","n"},
                { "w","n","n","n"}
};
        public static string[,] blockL = new string[4, 4]
            {
                { "w","w","w","n"},
                { "n","n","w","n"},
                { "n","n","n","n"},
                { "n","n","n","n"}
            };
        public static string[,] blockLL = new string[4, 4]
    {
                { "w","w","w","n"},
                { "w","n","n","n"},
                { "n","n","n","n"},
                { "n","n","n","n"}
    };

        public static Block randomBlock()
        {
            string[,] array = blockZ;
            int randomBlock = new Random().Next(7); //This choose random number from 0 to 6, there are 7 types of blocks

            switch (randomBlock)
            {
                case 0:
                    array = blockZ;
                    break;
                case 1:
                    array = blockT;
                    break;
                case 2:
                    array = blockO;
                    break;
                case 3:
                    array = blockI;
                    break;
                case 4:
                    array = blockL;
                    break;
                case 5:
                    array = blockLL;
                    break;
                case 6:
                    array = blockS;
                    break;
            }
            return new Block(array);
        }

        private bool isBlockShiftable(string[,] changedArray)
        {
            for (int row = 0; row < height; row++)
            {
                for (int collumn = 0; collumn < width; collumn++)
                {
                    if (changedArray[row, collumn] == "w" && collumn == 0)
                        return false;

                }
            }
            return true;
        }

        public void rotateBlock()
        {
            string [,] temporaryArray = new string [4,4];
            for (int row = 0; row < height; row++)
            {
                for (int collumn = 0; collumn < width; collumn++)
                {
                    temporaryArray[row, collumn] = this.BlockArray[collumn, 3-row];
                }
            }
            for (int step = 0; step < width; step++)
            {
                if (isBlockShiftable(temporaryArray) == true)
                {
                    for (int row = 0; row < height; row++)
                    {
                        for (int collumn = 1; collumn < width; collumn++)
                        {
                            temporaryArray[row, collumn - 1] = temporaryArray[row, collumn];

                            if (collumn == width - 1)
                                temporaryArray[row, collumn] = "n";

                        }
                    }
                }
            }

                    this.BlockArray = temporaryArray;
        }

        public Block()
        {
            BlockArray = new string[4, 4];
        }

        public Block(string[,] array)
        {
            this.BlockArray = array;
        }
    }
}
