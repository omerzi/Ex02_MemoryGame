using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02_MemoryGame
{
    public class GameManager
    {
        private Board m_Board;
        private Player m_FirstPlayer;
        private Player m_SecondPlayer;
        private PcPlayer m_PcPlayer = null;
        private eGameType m_GameType;
        private ePlayerType m_CurrentPlayer;

        public Board Board
        {
            get 
            {
                return m_Board;
            }
        }

        public enum eGameType
        {
            AgainstPC = 1,
            AgainstPlayer = 2
        }

        public enum ePlayerType
        {
            PC,
            FirstPlayer,
            SecondPlayer
        }
        
        public GameManager(int i_BoardWidth, int i_BoardHeight, string i_FirstPlayerName, string i_SecondPlayerName, eGameType i_GameType)
        {
            m_Board = new Board(i_BoardHeight, i_BoardWidth);
            m_GameType = i_GameType;
            m_FirstPlayer = new Player(i_FirstPlayerName);
            m_CurrentPlayer = ePlayerType.FirstPlayer;
            if(i_GameType == eGameType.AgainstPC)
            {
                m_PcPlayer = new PcPlayer();
            }
            else
            {
                m_SecondPlayer = new Player(i_SecondPlayerName);
            }
        }

        public eGameType GameType
        {
            get
            {
                return m_GameType;
            }
        }

        public ePlayerType CurrentPlayer
        {
            get 
            {
                return m_CurrentPlayer; 
            }

            set 
            {
                m_CurrentPlayer = value;
            }
        }

        public bool IsEnded()
        {
            bool IsEnded;
            if(m_GameType == eGameType.AgainstPC)
            {
                IsEnded = m_FirstPlayer.Points + m_PcPlayer.Points == (m_Board.Width * m_Board.Height) / 2;
            }
            else
            {
                IsEnded = m_FirstPlayer.Points + m_SecondPlayer.Points == (m_Board.Width * m_Board.Height) / 2;
            }

            return IsEnded;
        }

        public void PCTurn(out int o_RowChoise, out int o_ColumnChoise)
        {
            Random cellChoise = new Random();
            o_ColumnChoise = cellChoise.Next(0, m_Board.Width);
            o_RowChoise = cellChoise.Next(0, m_Board.Height);
            while(m_Board[o_RowChoise, o_ColumnChoise].IsFlipped)
            {
                o_ColumnChoise = cellChoise.Next(0, m_Board.Width);
                o_RowChoise = cellChoise.Next(0, m_Board.Height);
            }

            ExposeCard(o_RowChoise, o_ColumnChoise);
        }

        public void CheckChoises(int i_FirstColumnChoise, int i_FirstRowChoise, int i_SecondColumnChoise, int i_SecondRowChoise, out bool o_ToSleep)
        {
            if(m_Board[i_FirstRowChoise, i_FirstColumnChoise].Index == m_Board[i_SecondRowChoise, i_SecondColumnChoise].Index)
            {
                updatePoints();
                o_ToSleep = false;
            }
            else
            {
                m_Board[i_FirstRowChoise, i_FirstColumnChoise].IsFlipped = false;
                m_Board[i_SecondRowChoise, i_SecondColumnChoise].IsFlipped = false;
                o_ToSleep = true;
            }
        }

        private void updatePoints()
        {
            if(m_CurrentPlayer == ePlayerType.PC)
            {
                m_PcPlayer.Points++;
            }
            else if(m_CurrentPlayer == ePlayerType.FirstPlayer)
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
            return m_Board[i_Row, i_Column].IsFlipped;
        }

        public void ExposeCard(int i_Row, int i_Column)
        {
            m_Board[i_Row, i_Column].IsFlipped = true;
        }

        public string CurrentPlayerName()
        {
            string name;
            if(m_CurrentPlayer == ePlayerType.FirstPlayer)
            {
                name = m_FirstPlayer.Name;
            }
            else if(m_CurrentPlayer == ePlayerType.SecondPlayer)
            {
                name = m_SecondPlayer.Name;
            }
            else
            {
                name = m_PcPlayer.Name;
            }

            return name;
        }

        public string GetWinnerNameAndPoints(out int o_NumOfPoints, out bool o_CheckTie)
        {   
            if (m_PcPlayer != null)
            {
                o_CheckTie = m_FirstPlayer.Points == m_PcPlayer.Points;
                o_NumOfPoints = Math.Max(m_FirstPlayer.Points, m_PcPlayer.Points);
            }
            else
            {
                o_CheckTie = m_FirstPlayer.Points == m_SecondPlayer.Points;
                o_NumOfPoints = Math.Max(m_FirstPlayer.Points, m_SecondPlayer.Points);
            }

            return getWinnerName(o_NumOfPoints);
        }

        private string getWinnerName(int i_NumOfPoints)
        {
            string playerName;
            if(m_FirstPlayer.Points == i_NumOfPoints)
            {
                playerName = m_FirstPlayer.Name;
            }
            else if(m_SecondPlayer.Points == i_NumOfPoints)
            {
                playerName = m_SecondPlayer.Name;
            }
            else
            {
                playerName = m_PcPlayer.Name;
            }

            return playerName;
        }
    }
}
