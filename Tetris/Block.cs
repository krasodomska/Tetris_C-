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
                { "n","w","n","n"},
                { "n","w","n","n"},
                { "n","w","n","n"},
                { "n","w","n","n"}
};
        public static string[,] blockL = new string[4, 4]
            {
                { "w","w","w","n"},
                { "n","n","w","n"},
                { "n","n","n","n"},
                { "n","n","n","n"}
            };

        public static Block randomBlock()
        {
            string[,] array = blockZ;
            int randomBlock = new Random().Next(4);

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
            return new Block(array);
        }

        public void printCoordinates()
        {
            Console.WriteLine("X = " + x + ", Y = " + y);
        }

        public Block()
        {
            BlockArray = new string[4, 4]
            {
                { "w","n","n","n"},
                { "w","n","n","n"},
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
