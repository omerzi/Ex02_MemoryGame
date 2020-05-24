using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02_MemoryGame
{
    class UI
    {
        private GameManager m_Game;
        private char[] m_ObjectArray;

        public UI()
        {
            SetupGame();
        }
        public void PrintBoard(Board i_CurrentGame)
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
            for (int row = 0; row < i_CurrentGame.Width ; row++)
            {
                gameToPrint.AppendFormat("{0} ", (row + 1));
                for (int column = 0; column < i_CurrentGame.Height ; column++)
                {
                    if(i_CurrentGame[(eBoardColumns)column, row].IsFlipped == false)
                    {
                       cellToPrint = ' ';
                    }
                    else
                    {
                        cellToPrint = m_ObjectArray[m_Game.Board[(eBoardColumns)column, row].Index];
                    }
                    //gameToPrint.AppendFormat("|  {0} ", cellToPrint );
                }
                gameToPrint.Append("|");
                gameToPrint.AppendLine();
                gameToPrint.Append("  ");
               gameToPrint.Append('=', i_CurrentGame.Height *6 - 4);
                gameToPrint.AppendLine();
            }
            Console.WriteLine(gameToPrint);
        }
        public void ReadPlayersNames(out string o_FirstPlayerName, out string o_SecondPlayerName, out int o_NumOfPlayers)
        {
            bool checkIsValid;
            Console.WriteLine("Hello, please enter your name: ");
            o_FirstPlayerName = Console.ReadLine();
            Console.WriteLine(string.Format(
@"Hi there {0}! please press 1 to play against the Computer, press 2 to play against another player: ",
o_FirstPlayerName));
            checkIsValid = int.TryParse(Console.ReadLine(), out o_NumOfPlayers);
            while (!checkIsValid && o_NumOfPlayers != 1 && o_NumOfPlayers != 2)
            {
                Console.WriteLine(string.Format(
@"Something went wrong... 
Please try again, press 1 to play against computer and 2 to play against another player: "));
                checkIsValid = int.TryParse(Console.ReadLine(), out o_NumOfPlayers);
            }

            if (o_NumOfPlayers == 1)
            {
                o_SecondPlayerName = "PC";
            }
            else
            {
                Console.WriteLine("Please enter the second player name: ");
                o_SecondPlayerName = Console.ReadLine();
            }
        }
        public void ReadBoardSize(out int o_Width, out int o_Height)
        {
            bool checkHeight, checkWidth;
            Console.WriteLine(string.Format(
@"Please enter the board sizes, height and then width :
(Minimum size : 4x4, Maximum Size : 6x6 and only even values!)"));
            checkHeight = int.TryParse(Console.ReadLine(), out o_Height);
            checkWidth = int.TryParse(Console.ReadLine(), out o_Width);
            while ((!checkHeight || (o_Height == 4 && o_Height != 6)) || (!checkWidth || (o_Width != 4 && o_Width != 6)))
            {
                Console.WriteLine(string.Format(
@"Something went wrong... 
Please try again, enter board height and then board width: "));
                checkHeight = int.TryParse(Console.ReadLine(), out o_Height);
                checkWidth = int.TryParse(Console.ReadLine(), out o_Width);
            }
        }
        public void SetupGame()
        {
            ReadPlayersNames(out string firstPlayerName, out string secondPlayerName, out int numOfPlayers);
            ReadBoardSize(out int boardWidth, out int boardHeight);
            m_Game = new GameManager(boardWidth, boardHeight, firstPlayerName, secondPlayerName, numOfPlayers);
            m_ObjectArray = new char[(boardHeight * boardWidth) / 2];
            SetSigns(m_ObjectArray);
        }
        public void SetSigns(char [] i_Array)
        {
            for(int i = 0; i < i_Array.Length; i++)
            {
                i_Array[i] = (char)('A' + i);
            }
        }
    }
}
