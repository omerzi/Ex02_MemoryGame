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
            m_CurrentPlayer = ePlayerTypes.PC;
            if (i_NumOfPlayers == 1)
            {
                m_PcPlayer = new PcPlayer();
            }
            else
            {
                m_SecondPlayer = new Player(i_SecondPlayerName);
            }
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
            while (m_Board[(eBoardColumns)o_ColumnChoise, o_RowChoise].IsFlipped)
            {
                o_ColumnChoise = cellChoise.Next(0, m_Board.Width - 1);
                o_RowChoise = cellChoise.Next(0, m_Board.Height - 1);
            }
            m_Board[(eBoardColumns)o_ColumnChoise, o_RowChoise].IsFlipped = true;
        }
        public void CheckChoises(int i_FirstColumnChoise, int i_FirstRowChoise, int i_SecondColumnChoise, int i_SecondRowChoise, out bool o_ToSleep)
        {
            if(m_Board[(eBoardColumns)i_FirstColumnChoise, i_FirstRowChoise].Index == m_Board[(eBoardColumns)i_SecondColumnChoise, i_SecondRowChoise].Index)
            {
                updatePoints();
                o_ToSleep = false;
            }
            else
            {
                m_Board[(eBoardColumns)i_FirstColumnChoise, i_FirstRowChoise].IsFlipped = false;
                m_Board[(eBoardColumns)i_SecondColumnChoise, i_SecondRowChoise].IsFlipped = false;
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
    }

}
