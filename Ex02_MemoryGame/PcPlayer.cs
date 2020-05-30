using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02_MemoryGame
{
    public class PcPlayer
    {
        private readonly string r_Name = "Computer";
        private int m_Points = 0;

        public int Points
        {
            get 
            {
                return m_Points;
            }

            set 
            { 
                m_Points = value;
            }
        }

        public string Name
        {
            get 
            {
                return r_Name;
            }
        }
    }
}
