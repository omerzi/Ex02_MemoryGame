using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Ex02_MemoryGame
{
    public class Program
    {
        public static void Main()
        {
            UI memoryGameUI = new UI();
            memoryGameUI.StartGame();
        }
    }
}
