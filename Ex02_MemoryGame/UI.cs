﻿using System;
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
            for(int i = 0; i < m_Game.Board.Width; i++)
            {
                gameToPrint.Append("    " + (char)('A' + i));
            }

            gameToPrint.AppendLine();
            gameToPrint.Append("  ");
            gameToPrint.Append('=', (m_Game.Board.Width * 6) - 4 );
            gameToPrint.AppendLine();
            for(int row = 0; row < m_Game.Board.Height; row++)
            {
                gameToPrint.AppendFormat("{0} ", row + 1);
                for(int column = 0; column < m_Game.Board.Width; column++)
                {
                    if(m_Game.Board[row, column].IsFlipped == false)
                    {
                       cellToPrint = ' ';
                    }
                    else
                    {
                        cellToPrint = m_ObjectArray[m_Game.Board[row, column].Index];
                    }

                    gameToPrint.AppendFormat("|  {0} ", cellToPrint );
                }

                gameToPrint.Append("|");
                gameToPrint.AppendLine();
                gameToPrint.Append("  ");
                gameToPrint.Append('=', (m_Game.Board.Width * 6) - 4);
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
            while(!checkIsValid && o_NumOfPlayers != 1 && o_NumOfPlayers != 2)
            {
                Console.WriteLine(string.Format(
@"Something went wrong... 
Please try again, press 1 to play against computer and 2 to play against another player: "));
                checkIsValid = int.TryParse(Console.ReadLine(), out o_NumOfPlayers);
            }

            if(o_NumOfPlayers == 1)
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
            while((!checkHeight || (o_Height != 4 && o_Height != 6)) || (!checkWidth || (o_Width != 4 && o_Width != 6)))
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

        public void SetSigns(char[] i_Array)
        {
            for(int i = 0; i < i_Array.Length; i++)
            {
                i_Array[i] = (char)('A' + i);
            }
        }

        public void StartGame()
        {
            while(!m_Game.IsEnded() )
            {
                MakeTurn();
            }

            CongratsWinner();
            System.Threading.Thread.Sleep(3000);
        }

        public void MakeTurn()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            PrintBoard();
            int firstColumnChoise, firstRowChoise;
            int secondColumnChoise, secondRowChoise;
            bool toSleep;
            if(m_Game.CurrentPlayer == ePlayerTypes.PC)
            {
                Console.WriteLine("PC is choosing cells");
                m_Game.PCTurn(out firstRowChoise, out firstColumnChoise);
                System.Threading.Thread.Sleep(2000);
                Ex02.ConsoleUtils.Screen.Clear();
                PrintBoard();
                Console.WriteLine("now choosing cell 2 before printing");
                m_Game.PCTurn(out secondRowChoise, out secondColumnChoise);
                Ex02.ConsoleUtils.Screen.Clear();
                PrintBoard();
                m_Game.CheckChoises(firstColumnChoise, firstRowChoise, secondColumnChoise, secondRowChoise, out toSleep);
                if(toSleep)
                {
                    System.Threading.Thread.Sleep(2000);
                }

                m_Game.CurrentPlayer = ePlayerTypes.FirstPlayer;
            }
            else
            {
                UserTurn();
            }
        }

        public void UserTurn()
        {
            int firstColumnChoise, firstRowChoise;
            int secondColumnChoise, secondRowChoise;
            bool toSleep;
            ReadCell(out firstRowChoise, out firstColumnChoise);
            m_Game.ExposeCard(firstRowChoise, firstColumnChoise);
            Ex02.ConsoleUtils.Screen.Clear();
            PrintBoard();
            ReadCell(out secondRowChoise, out secondColumnChoise);
            m_Game.ExposeCard(secondRowChoise, secondColumnChoise);
            Ex02.ConsoleUtils.Screen.Clear();
            PrintBoard();
            m_Game.CheckChoises(firstColumnChoise, firstRowChoise, secondColumnChoise, secondRowChoise, out toSleep);
            if(toSleep)
            {
                System.Threading.Thread.Sleep(2000);
            }

            ChangeCurrentPlayer();
        }

        public void ReadCell(out int o_Row, out int o_Column)
        {
            string cell;
            bool Quit;
            int row, column;
            do
            {
                Console.Write(string.Format("{0}, Please enter the cell you want to expose: ", m_Game.CurrentPlayerName()));
                cell = Console.ReadLine();
                Quit = CheckIfToQuit(cell);
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
            while (!CheckCell(cell));
            ConvertStringToCell(cell, out row, out column);
            o_Row = row;
            o_Column = column;
        }

        public bool CheckIfToQuit(string i_Input)
        {
            return i_Input == "Q";
        }

        public bool CheckCell(string i_Cell)
        {
            int row, column;
            bool IsValidInput = true;
            if(!m_Game.CheckLength(i_Cell.Length))
            {
                IsValidInput = false;
                Console.WriteLine(string.Format(
                    @"{0} is incorrect cell!",
                    i_Cell));
            }

            if(IsValidInput)
            {
                ConvertStringToCell(i_Cell, out row, out column);
                if(!m_Game.CheckBoundries(row, column))
                {
                    IsValidInput = false;
                    Console.WriteLine(string.Format(
                        @"{0} is out of board's boundries!",
                        i_Cell));
                }
                else if(m_Game.IsAlreadyFlipped(row, column))
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
            if (m_Game.NumOfPlayers == 2)
            {
                if (m_Game.CurrentPlayer == ePlayerTypes.FirstPlayer)
                {
                    m_Game.CurrentPlayer = ePlayerTypes.SecondPlayer;
                }
                else
                {
                    m_Game.CurrentPlayer = ePlayerTypes.FirstPlayer;
                }
            }
            else
            {
                m_Game.CurrentPlayer = ePlayerTypes.PC;
            }
        }

        public void CongratsWinner()
        {
            int numOfPoints;
            string winnerName = m_Game.GetWinnerNameAndPoints(out numOfPoints);
            Console.WriteLine(string.Format(
"Congratiulations {0}! you won the game with {1} points!",
winnerName,
numOfPoints));
        }
    }
}
