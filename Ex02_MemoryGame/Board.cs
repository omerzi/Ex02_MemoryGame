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
        private Card[,] m_GameBoard;

        public Board(int i_Width, int i_Height)
        {
            m_Width = i_Width;
            m_Height = i_Height;
            m_GameBoard = new Card[i_Width, i_Height];
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
        public Card[,] GameBoard
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
        public Card this[int i, int j]
        {
            get { return m_GameBoard[i, j]; }
            set { m_GameBoard[i, j] = value; }
        }
        public void InitBoard()
        {
            int value = 0;
            int counter = 1;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    m_GameBoard[i, j].Index = value;
                    if (counter == 2)
                    {
                        counter = 0;
                        value++;
                    }
                    counter++;
                }
            }
            Random chooseIndexForCard = new Random();
            int columnRandom, lineRandom, tempIndex;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    columnRandom = chooseIndexForCard.Next(0, Width - 1);
                    lineRandom = chooseIndexForCard.Next(0, Height - 1);
                    tempIndex = m_GameBoard[i, j].Index;
                    m_GameBoard[i, j].Index = m_GameBoard[lineRandom, columnRandom].Index;
                    m_GameBoard[lineRandom, columnRandom].Index = tempIndex;
                }
            }
        }
    }
}
