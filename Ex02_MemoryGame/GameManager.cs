using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02_MemoryGame
{
    class GameManager
    {
        private Board m_Board;
        private Player m_FirstPlayer;
        private Player m_SecondPlayer;
        private PcPlayer m_PcPlayer = null;
        private int m_NumberOfPlayers;
        private ePlayerTypes m_CurrentPlayer;

        public Board Board
        {
            get { return m_Board; }
        }
        public GameManager(int i_BoardWidth, int i_BoardHeight, string i_FirstPlayerName, string i_SecondPlayerName, int i_NumOfPlayers)
        {
            m_Board = new Board(i_BoardWidth, i_BoardHeight);
            m_NumberOfPlayers = i_NumOfPlayers;
            m_FirstPlayer = new Player(i_FirstPlayerName);
            m_CurrentPlayer = ePlayerTypes.FirstPlayer;
            if (i_NumOfPlayers == 1)
            {
                m_PcPlayer = new PcPlayer();
            }
            else
            {
                m_SecondPlayer = new Player(i_SecondPlayerName);
            }
        }
        public int NumOfPlayers
        {
            get { return m_NumberOfPlayers; }
        }
        public ePlayerTypes CurrentPlayer
        {
            get { return m_CurrentPlayer; }
            set { m_CurrentPlayer = value; }
        }
        public bool IsEnded()
        {
            bool IsEnded;
            if(m_NumberOfPlayers == 1)
            {
                IsEnded = m_FirstPlayer.Points + m_PcPlayer.Points == m_Board.Width * m_Board.Height / 2;
            }
            else
            {
                IsEnded = m_FirstPlayer.Points + m_SecondPlayer.Points == m_Board.Width * m_Board.Height / 2;
            }

            return IsEnded;
        }
        public void PCTurn(out int o_RowChoise, out int o_ColumnChoise)
        {
            Random cellChoise = new Random();
            o_ColumnChoise = cellChoise.Next(0, m_Board.Width - 1);
            o_RowChoise = cellChoise.Next(0, m_Board.Height - 1);
            while(m_Board[o_ColumnChoise , o_RowChoise].IsFlipped)
            {
                o_ColumnChoise = cellChoise.Next(0, m_Board.Width - 1);
                o_RowChoise = cellChoise.Next(0, m_Board.Height - 1);
            }

            ExposeCard(o_RowChoise, o_ColumnChoise);
        }
        public void CheckChoises(int i_FirstColumnChoise, int i_FirstRowChoise, int i_SecondColumnChoise, int i_SecondRowChoise, out bool o_ToSleep)
        {
            if(m_Board[i_FirstColumnChoise, i_FirstRowChoise].Index == m_Board[i_SecondColumnChoise, i_SecondRowChoise].Index)
            {
                updatePoints();
                o_ToSleep = false;
            }
            else
            {
                m_Board[i_FirstColumnChoise, i_FirstRowChoise].IsFlipped = false;
                m_Board[i_SecondColumnChoise, i_SecondRowChoise].IsFlipped = false;
                o_ToSleep = true;
            }
        }
        private void updatePoints()
        {
            if (m_CurrentPlayer == ePlayerTypes.PC)
            {
                m_PcPlayer.Points++;
            }
            else if (m_CurrentPlayer == ePlayerTypes.FirstPlayer)
            {
                m_FirstPlayer.Points++;
            }
            else
            {
                m_SecondPlayer.Points++;
            }
        }
        public bool CheckLength(int i_Length)
        {
            return i_Length == 2;
        }
        public bool CheckBoundries(int i_Height, int i_Width)
        {
            return i_Height < m_Board.Height && i_Height >= 0 && i_Width < m_Board.Width && i_Width >= 0;
        }
        public bool IsAlreadyFlipped(int i_Row, int i_Column)
        {
            return m_Board[i_Column, i_Row].IsFlipped;
        }
        public void ExposeCard(int i_Row, int i_Column)
        {
            m_Board[i_Column, i_Row].IsFlipped = true;
        }
        public string CurrentPlayerName()
        {
            string name;
            if(m_CurrentPlayer == ePlayerTypes.FirstPlayer)
            {
                name = m_FirstPlayer.Name;
            }
            else if (m_CurrentPlayer == ePlayerTypes.SecondPlayer)
            {
                name = m_SecondPlayer.Name;
            }
            else
            {
                name = m_PcPlayer.Name;
            }

            return name;
        }
        public string GetWinnerNameAndPoints(out int numOfPoints)
        {
            string winnerName;
            numOfPoints = Math.Max(m_FirstPlayer.Points, Math.Max(m_PcPlayer.Points, m_SecondPlayer.Points));
            
            if(m_NumberOfPlayers == 1)
            {
                if(m_PcPlayer.Points > m_FirstPlayer.Points)
                {
                    winnerName = m_PcPlayer.Name;
                    numOfPoints = m_PcPlayer.Points;
                }
                else
                {
                    winnerName = m_FirstPlayer.Name;
                    numOfPoints = m_FirstPlayer.Points;
                }
            }
            else
            {
                if(m_SecondPlayer.Points > m_FirstPlayer.Points)
                {
                    winnerName = m_SecondPlayer.Name;
                    numOfPoints = m_SecondPlayer.Points;
                }
                else
                {
                    winnerName = m_FirstPlayer.Name;
                    numOfPoints = m_FirstPlayer.Points;
                }
            }

            return winnerName;
        }
    }
}
