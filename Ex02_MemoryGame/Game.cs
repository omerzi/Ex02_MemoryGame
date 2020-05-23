using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02_MemoryGame
{
    class Game
    {
        private Player m_Player1;
        private Player m_Player2;
        private PcPlayer m_OptinalPlayer = null;
        //private UI m_InterfaceGame;
        private int m_NumberOfPlayers;

        public Game(int i_NumberOfPlayers, Player i_Player1, Player i_Player2)
        {
            //this.m_InterfaceGame = new UI();
            m_NumberOfPlayers = i_NumberOfPlayers;
            if (i_NumberOfPlayers == 1)
            {
                m_Player1 = i_Player1;
                m_OptinalPlayer = new PcPlayer();
            }
            else
            {
                m_Player1 = i_Player1;
                m_Player2 = i_Player2;
            }
        }
    }
}
