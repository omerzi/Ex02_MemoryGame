using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02_MemoryGame
{
    public class UI
    {
        private GameManager m_Game;
        private char[] m_ObjectArray;

        public UI()
        {
            SetupGame();
        }

        public void PrintBoard()
        {
            char cellToPrint;
            StringBuilder gameToPrint = new StringBuilder();
            for (int i = 0; i < m_Game.Board.Width; i++)
            {
                gameToPrint.Append("    " + (char)('A' + i));
            }

            gameToPrint.AppendLine();
            gameToPrint.Append("  ");
            gameToPrint.Append('=', (m_Game.Board.Width * 6) - 4);
            gameToPrint.AppendLine();
            for (int row = 0; row < m_Game.Board.Height; row++)
            {
                gameToPrint.AppendFormat("{0} ", row + 1);
                for (int column = 0; column < m_Game.Board.Width; column++)
                {
                    if (m_Game.Board[row, column].IsFlipped == false)
                    {
                        cellToPrint = ' ';
                    }
                    else
                    {
                        cellToPrint = m_ObjectArray[m_Game.Board[row, column].Index];
                    }

                    gameToPrint.AppendFormat("|  {0} ", cellToPrint);
                }

                gameToPrint.Append("|");
                gameToPrint.AppendLine();
                gameToPrint.Append("  ");
                gameToPrint.Append('=', (m_Game.Board.Width * 6) - 4);
                gameToPrint.AppendLine();
            }

            Console.WriteLine(gameToPrint);
        }

        public void ReadPlayersNames(out string o_FirstPlayerName, out string o_SecondPlayerName, out GameManager.eGameType o_GameType)
        {
            Console.WriteLine("Hello, please enter your name: ");
            checkName(out string name);
            o_FirstPlayerName = name;
            Console.WriteLine(string.Format(
"Hi there {0}! please press 1 to play against the Computer, press 2 to play against another player: ",
o_FirstPlayerName));
            checkGameType(out int numOfPlayers);
            o_GameType = (GameManager.eGameType)numOfPlayers;
            if (o_GameType == GameManager.eGameType.AgainstPC)
            {
                o_SecondPlayerName = "PC";
            }
            else
            {
                Console.WriteLine("Please enter the second player name: ");
                checkName(out name);
                o_SecondPlayerName = name;
            }
        }

        private void checkName(out string o_Name)
        {
            o_Name = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(o_Name))
            {
                Console.Write(string.Format(
@"Your name cannot be empty or consist only white-space characters,
please enter a valid name: "));
                o_Name = Console.ReadLine();
            }
        }

        private void checkGameType(out int o_NumOfPlayers)
        {
            bool checkIsValid = int.TryParse(Console.ReadLine(), out o_NumOfPlayers);
            while (!checkIsValid || (o_NumOfPlayers != (int)GameManager.eGameType.AgainstPC && o_NumOfPlayers != (int)GameManager.eGameType.AgainstPlayer))
            {
                Console.WriteLine(string.Format(
@"You entered wrong number of players. 
Please try again, press 1 to play against computer and 2 to play against another player: "));
                checkIsValid = int.TryParse(Console.ReadLine(), out o_NumOfPlayers);
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
            while (!checkHeight || o_Height < 4 || o_Height > 6 || !checkWidth || o_Width < 4 || o_Width > 6 || (o_Height * o_Width) % 2 != 0)
            {
                Console.WriteLine(string.Format(
@"You entered wrong board sizes. 
Please try again, enter board height and then board width: "));
                checkHeight = int.TryParse(Console.ReadLine(), out o_Height);
                checkWidth = int.TryParse(Console.ReadLine(), out o_Width);
            }
        }

        public void SetupGame()
        {
            ReadPlayersNames(out string firstPlayerName, out string secondPlayerName, out GameManager.eGameType gameType);
            ReadBoardSize(out int boardWidth, out int boardHeight);
            m_Game = new GameManager(boardWidth, boardHeight, firstPlayerName, secondPlayerName, gameType);
            m_ObjectArray = new char[(boardHeight * boardWidth) / 2];
            SetSigns(m_ObjectArray);
        }

        public void SetSigns(char[] i_Array)
        {
            for (int i = 0; i < i_Array.Length; i++)
            {
                i_Array[i] = (char)('A' + i);
            }
        }

        public void StartGame()
        {
            while (!m_Game.IsEnded())
            {
                MakeTurn();
            }

            CongratsWinner();
            if (toStartOver())
            {
                Ex02.ConsoleUtils.Screen.Clear();
                SetupGame();
                StartGame();
            }
        }

        public void MakeTurn()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            PrintBoard();
            if (m_Game.CurrentPlayer == GameManager.ePlayerType.PC)
            {
                Console.WriteLine("PC is choosing cells");
                m_Game.PCTurn(out int firstRowChoise, out int firstColumnChoise);
                System.Threading.Thread.Sleep(2000);
                Ex02.ConsoleUtils.Screen.Clear();
                PrintBoard();
                m_Game.PCTurn(out int secondRowChoise, out int secondColumnChoise);
                Ex02.ConsoleUtils.Screen.Clear();
                PrintBoard();
                m_Game.CheckChoises(firstColumnChoise, firstRowChoise, secondColumnChoise, secondRowChoise, out bool toSleep);
                if (toSleep)
                {
                    System.Threading.Thread.Sleep(2000);
                }

                m_Game.CurrentPlayer = GameManager.ePlayerType.FirstPlayer;
            }
            else
            {
                UserTurn();
            }
        }

        public void UserTurn()
        {
            ReadCell(out int firstRowChoise, out int firstColumnChoise);
            m_Game.ExposeCard(firstRowChoise, firstColumnChoise);
            Ex02.ConsoleUtils.Screen.Clear();
            PrintBoard();
            ReadCell(out int secondRowChoise, out int secondColumnChoise);
            m_Game.ExposeCard(secondRowChoise, secondColumnChoise);
            Ex02.ConsoleUtils.Screen.Clear();
            PrintBoard();
            m_Game.CheckChoises(firstColumnChoise, firstRowChoise, secondColumnChoise, secondRowChoise, out bool toSleep);
            if (toSleep)
            {
                System.Threading.Thread.Sleep(2000);
            }

            ChangeCurrentPlayer();
        }

        public void ReadCell(out int o_Row, out int o_Column)
        {
            string cell;
            bool Quit;
            do
            {
                Console.Write(string.Format("{0}, Please enter the cell you want to expose: ", m_Game.CurrentPlayerName()));
                cell = Console.ReadLine();
                Quit = checkIfToQuit(cell);
                if (Quit)
                {
                    Console.Write(string.Format(
@"{0} You decided to finish the game,
thank you 
and Bye Bye till next time!",
m_Game.CurrentPlayerName()));
                    System.Threading.Thread.Sleep(2000);
                    Environment.Exit('Q');
                }
            }
            while (!checkCell(cell));
            ConvertStringToCell(cell, out int row, out int column);
            o_Row = row;
            o_Column = column;
        }

        private bool checkIfToQuit(string i_Input)
        {
            return i_Input == "Q";
        }

        private bool checkCell(string i_Cell)
        {
            bool IsValidInput = true;
            if (!m_Game.CheckLength(i_Cell.Length))
            {
                IsValidInput = false;
                Console.WriteLine(string.Format(
                    @"{0} is incorrect cell!",
                    i_Cell));
            }

            if (IsValidInput)
            {
                ConvertStringToCell(i_Cell, out int row, out int column);
                if (!m_Game.CheckBoundries(row, column))
                {
                    IsValidInput = false;
                    Console.WriteLine(string.Format(
                        @"{0} is out of board's boundries!",
                        i_Cell));
                }
                else if (m_Game.IsAlreadyFlipped(row, column))
                {
                    IsValidInput = false;
                    Console.WriteLine(string.Format(
                        @"{0} is already flipped!",
                        i_Cell));
                }
            }

            return IsValidInput;
        }

        public void ConvertStringToCell(string i_String, out int o_Row, out int o_Column)
        {
            o_Row = i_String[1] - '1';
            o_Column = i_String[0] - 'A';
        }

        public void ChangeCurrentPlayer()
        {
            if (m_Game.GameType == GameManager.eGameType.AgainstPlayer)
            {
                if (m_Game.CurrentPlayer == GameManager.ePlayerType.FirstPlayer)
                {
                    m_Game.CurrentPlayer = GameManager.ePlayerType.SecondPlayer;
                }
                else
                {
                    m_Game.CurrentPlayer = GameManager.ePlayerType.FirstPlayer;
                }
            }
            else
            {
                m_Game.CurrentPlayer = GameManager.ePlayerType.PC;
            }
        }

        public void CongratsWinner()
        {
            string winnerName = m_Game.GetWinnerNameAndPoints(out int numOfPoints, out bool checkTie);
            if (!checkTie)
            {
                Console.WriteLine(string.Format(
                    "Congratiulations {0}! you won the game with {1} points!",
                    winnerName,
                    numOfPoints));
                System.Threading.Thread.Sleep(3000);
            }
            else
            {
                Console.WriteLine("Its a Tie!");
                System.Threading.Thread.Sleep(3000);
            }
        }

        private bool toStartOver()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine("Would you like to start over? press Y (capital) to start over, press anything else to quit the game");
            string startOver = Console.ReadLine();
            return startOver == "Y";
        }
    }
}
