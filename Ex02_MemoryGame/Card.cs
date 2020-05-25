using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02_MemoryGame
{
    public class Card
    {
        private int m_Index;
        private bool m_IsFlipped;

        public Card(int i_Index)
        {
            m_Index = i_Index;
            m_IsFlipped = false;
        }

        public int Index
        {
            get 
            { 
                return m_Index;
            }

            set
            {
                m_Index = value;
            }
        }

        public bool IsFlipped
        {
            get 
            {
                return m_IsFlipped;
            }

            set
            {
                m_IsFlipped = value;
            }
        }
    }
}
