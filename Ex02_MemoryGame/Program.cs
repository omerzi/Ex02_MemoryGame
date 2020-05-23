using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Ex02_MemoryGame
{
    class Program
    {
        static void Main()
        {
            //UI memoryGameUI = new UI();
            //memoryGameUI.SetupGame();

            Board c = new Board(6, 6);
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                   c.GameBoard[i, j].IsFlipped = true;
                }
            }
            //UI.PrintBoard(c);

            UI.PrintBoard(c);
        }
    }
}
