using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02_MemoryGame
{
    public class Player
    {
        private string m_Name;
        private int m_Points;

        public Player(string i_Name)
        {
            m_Name = i_Name;
            m_Points = 0;
        }
        
        public string Name
        {
            get 
            {
                return m_Name; 
            }

            set 
            {
                m_Name = value; 
            }
        }

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
    }
}
