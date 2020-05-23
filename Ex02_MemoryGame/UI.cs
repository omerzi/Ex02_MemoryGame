using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02_MemoryGame
{
    class UI
    {
        public static void PrintBoard(Board i_CurrentGame)
        {
            char cellToPrint;
            StringBuilder gameToPrint = new StringBuilder();
            for (int i = 0; i < i_CurrentGame.Height; i++)
            {
                gameToPrint.Append("    " + (char)('A' + i));
             }
            gameToPrint.AppendLine();
            gameToPrint.Append("  ");
            gameToPrint.Append('=', i_CurrentGame.Height *6 - 4 );
            gameToPrint.AppendLine();
            for (int i = 0; i < i_CurrentGame.Width ; i++)
            {
                gameToPrint.AppendFormat("{0} ", (i + 1));
                for (int j = 0; j < i_CurrentGame.Height ; j++)
                {
                    if(i_CurrentGame.GameBoard[i, j].IsFlipped == false)
                    {
                       cellToPrint = ' ';
                    }
                    else
                    {
                        cellToPrint = i_CurrentGame.GameBoard[i, j].Sign;
                    }
                    gameToPrint.AppendFormat("|  {0} ", cellToPrint );
                }
                gameToPrint.Append("|");
                gameToPrint.AppendLine();
                gameToPrint.Append("  ");
               gameToPrint.Append('=', i_CurrentGame.Height *6 - 4);
                gameToPrint.AppendLine();
            }
            Console.Write(gameToPrint);
        }
        //public void ExitGameOrNextMove(string i_PlayerInput)
        //{
        //    if(i_PlayerInput=="Q" || i_PlayerInput=="q" )
        //    {
        //        Console.WriteLine("Bye Bye, Thank you for playing!");
                
        //    }


        //}
        
    }
}
