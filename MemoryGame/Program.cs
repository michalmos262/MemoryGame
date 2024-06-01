using MemoryGame;
using System;

namespace Ex_02
{
    public class Program
    {
        public static void Main()
        {
            GameBoard board = new GameBoard(6, 6);

            for (int i = 0; i < board.NumOfRows; i++)
            {
                for (int j = 0; j < board.NumOfColumns; j++)
                {
                    Console.Write(board.Board[i, j].Number + "\t");
                }
                Console.WriteLine();
            }
        }
        
    }
}
