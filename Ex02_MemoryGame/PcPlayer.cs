using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02_MemoryGame
{
    class PcPlayer
    {
        private readonly string m_Name = "Computer";
        private int m_Points;

        public int Points
        {
            get { return m_Points; }
            set { m_Points = value; }
        }
        public string Name
        {
            get { return m_Name; }
        }
    }
}
