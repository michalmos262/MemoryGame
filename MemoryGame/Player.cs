using System;
using System.Collections.Generic;

namespace MemoryGame
{
    public struct Player
    {
        private int m_Score;
        private bool m_isHuman;

        public int Score
        {
            get
            {
                return m_Score;
            }
            set
            {
                m_Score = value;
            }
        }

        public bool IsHuman
        {
            get
            {
                return m_isHuman;
            }
            set
            {
                m_isHuman = value;
            }
        }

        public (int, int) ChooseRandomCellInBoard(ref Board board)
        {
            Random random = new Random();
            List<(int, int)> hiddenBoardCells = new List<(int, int)>();
            (int, int) chosenCell = (-1, -1);

            for (int i = 0; i < board.NumOfRows; i++)
            {
                for (int j = 0; j < board.NumOfColumns; j++)
                {
                    if (board.BoardShownToUser[i, j] == 0)
                    {
                        hiddenBoardCells.Add((i, j));
                    }
                }
            }
            
            int randomIndex = random.Next(hiddenBoardCells.Count);
            if (hiddenBoardCells.Count != 0)
            {
                chosenCell = hiddenBoardCells[randomIndex];
            }
            
            return chosenCell;
        }
    }
}
