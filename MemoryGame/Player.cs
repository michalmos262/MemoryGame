using System;
using System.Collections.Generic;

namespace MemoryGame
{
    public class Player
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

        public GameBoard.Position ChooseRandomCellInBoard(GameBoard board)
        {
            Random random = new Random();
            List<GameBoard.Position> hiddenBoardCells = board.GetCurrentHiddenCells();

            int randomIndex = random.Next(hiddenBoardCells.Count);
            return hiddenBoardCells[randomIndex];
        }
    }
}
