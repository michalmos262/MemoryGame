using MemoryGame;
using System;

namespace Ex_02
{
    public class Program
    {
        public static void Main()
        {
            Card c;
            GameManager game = new GameManager();
            game.SetBoardDimensions(4, 4);
            GameBoard.Position position = new GameBoard.Position(1,2);
            c = game.RevealCardInBoard(position);
            Console.WriteLine(c.Number);
        }
        
    }
}
