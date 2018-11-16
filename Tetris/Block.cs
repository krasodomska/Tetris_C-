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
        public string[,] BlockArray = new string[4, 4];

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
            int randomBlock = new Random().Next(7);

            if (randomBlock == 0)
                array = blockZ;
            if (randomBlock == 1)
                array = blockT;
            if (randomBlock == 2)
                array = blockO;
            if (randomBlock == 3)
                array = blockI;
            if (randomBlock == 4)
                array = blockL;
            if (randomBlock == 5)
                array = blockLL;
            if (randomBlock == 6)
                array = blockS;
            return new Block(array);
        }

        private bool swiftBlock(string[,] changedArray)
        {
            for (int row = 0; row < 4; row++)
            {
                for (int collumn = 0; collumn < 4; collumn++)
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
            for (int row = 0; row < 4; row++)
            {
                for (int collumn = 0; collumn < 4; collumn++)
                {
                    temporaryArray[row, collumn] = this.BlockArray[collumn, 3-row];
                }
            }
            for (int step = 0; step <= 3; step++)
            {
                if (swiftBlock(temporaryArray) == true)
                {
                    for (int row = 0; row < 4; row++)
                    {
                        for (int collumn = 1; collumn < 4; collumn++)
                        {
                            temporaryArray[row, collumn - 1] = temporaryArray[row, collumn];

                            if (collumn == 3)
                                temporaryArray[row, collumn] = "n";

                        }
                    }
                }
            }

                    this.BlockArray = temporaryArray;
        }

        public Block()
        {
            BlockArray = new string[4, 4]
            {
                { "n","n","n","n"},
                { "n","n","n","n"},
                { "n","n","n","n"},
                { "n","n","n","n"}
            };
        }

        public Block(string[,] array)
        {
            this.BlockArray = array;
        }
    }
}
