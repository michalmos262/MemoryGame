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

        public Board.Position ChooseRandomCellInBoard(ref Board board)
        {
            Random random = new Random();
            List<Board.Position> hiddenBoardCells = new List<Board.Position>();

            for (int i = 0; i < board.NumOfRows; i++)
            {
                for (int j = 0; j < board.NumOfColumns; j++)
                {
                    if (board.BoardShownToUser[i, j] == 0)
                    {

                        hiddenBoardCells.Add(new Board.Position(i, j));
                    }
                }
            }
            
            int randomIndex = random.Next(hiddenBoardCells.Count);
            
            return hiddenBoardCells[randomIndex];
        }
    }
}
