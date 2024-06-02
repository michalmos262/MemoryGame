using MemoryGame;
using System;

namespace Ex_02
{
    public class Program
    {
        public static void Main()
        {
            GameManager game = new GameManager(eGameModes.HumanVsHuman, 5, 4);
            UI userInterface = new UI(game);

            userInterface.ShowGameBoard();
        }
        
    }
}
