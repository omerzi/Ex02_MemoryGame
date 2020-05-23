using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02_MemoryGame
{
    class Game
    {
        private Board m_Board;
        private Player m_FirstPlayer;
        private Player m_SecondPlayer;
        private PcPlayer m_PcPlayer = null;
        private int m_NumberOfPlayers;

        public Game(int i_BoardWidth, int i_BoardHeight, string i_FirstPlayerName, string i_SecondPlayerName, int i_NumOfPlayers)
        {
            m_Board = new Board(i_BoardWidth, i_BoardHeight);
            m_NumberOfPlayers = i_NumOfPlayers;
            m_FirstPlayer = new Player(i_FirstPlayerName);
            if(i_NumOfPlayers == 1)
            {
                m_PcPlayer = new PcPlayer();
            }
            else
            {
                m_SecondPlayer = new Player(i_SecondPlayerName);
            }
        }
    }
}
