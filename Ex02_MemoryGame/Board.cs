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
            InitBoard();
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
        public Card<char>[,] GameBoard
        {
            get
            {
                return m_GameBoard;
            }

            set
            {
                m_GameBoard = value;
            }
        }
        public void InitBoard()
        {
            StringBuilder cardsOptions = new StringBuilder();
            int numberOfOptinalCards = (m_Height * m_Height) / 2;
            for (int i = 0; i < numberOfOptinalCards; i++)
            {
                cardsOptions.Append((char)('A' + i), 2);
            }
            Random chooseIndexForCard = new Random();
            int indexChoise;
            for (int i = 0; i < Height ; i++)
            {
                for (int j = 0; j < Width ; j++)
                {
                    indexChoise = chooseIndexForCard.Next(0, cardsOptions.Length - 1);
                    GameBoard[i, j].Sign = cardsOptions[indexChoise];
                    cardsOptions.Remove(indexChoise, 1);
                }
            }
        }
    }
}
