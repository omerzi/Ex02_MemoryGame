using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02_MemoryGame
{
    struct Card <T>
    {
        private T m_Sign;
        private bool m_IsFlipped;

        public Card(T i_Sign)
        {
            m_Sign = i_Sign;
            m_IsFlipped = false;
        }

        public T Sign
        {
            get { return m_Sign; }
            set { m_Sign = value; }
        }

        public bool IsFlipped
        {
            get { return m_IsFlipped; }
            set { m_IsFlipped = value; }
        }
    }
}
