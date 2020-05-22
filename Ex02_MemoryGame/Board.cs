using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02_MemoryGame
{
    class Board
    {
        private int m_Width;
        private int m_Height;
        private Card<char>[,] m_GameBoard;

        public Board(int i_Width, int i_Height)
        {
            m_Width = i_Width;
            Height = i_Height;
            m_GameBoard = new Card<char>[i_Width, i_Height];
        }

        public int Height
        {
            get
            {
                return m_Height;
            }

            set
            {
                m_Height = value;
            }
        }

        public int Width
        {
            get
            {
                return m_Width;
            }

            set
            {
                m_Width = value;
            }
        }
    }
}
