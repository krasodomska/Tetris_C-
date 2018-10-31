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
        public int y = -1; //coordinates of block
        public string[,] BlockArray = new string[4, 4] 
        {
            {"n","n","n","n"},
            {"n","n","n","n"},
            {"n","n","n","n"},
            {"n","n","n","n"}
        };
        
        public Block()
        {
            BlockArray = new string[4, 4]
            {
                { "w","w","n","n"},
                { "w","w","n","n"},
                { "n","n","n","n"},
                { "n","n","n","n"}
            };
        }
    }
}
