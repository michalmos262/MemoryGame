using MemoryGame;
using System;

namespace Ex_02
{
    public class Program
    {
        public static void Main()
        {
            GameManager game = new GameManager(eGameModes.HumanVsHuman, 5, 4);
            GameBoard.Position position2 = new GameBoard.Position(1, 2);

            game.GameBoard.RevealCard(position2);

            for (int i = 0; i < game.GameBoard.NumOfRows; i++)
            {
                for (int j = 0; j < game.GameBoard.NumOfColumns; j++)
                {
                    GameBoard.Position position = new GameBoard.Position(i, j);
                    
                    if (game.GameBoard.IsCardRevealed(position))
                    {
                        Console.Write(game.GameBoard.Board[i, j].Number + "\t");
                    }
                    else
                    {
                        Console.Write(0 + "\t");
                    }
                }
                Console.WriteLine();
            }
        }
        
    }
}
