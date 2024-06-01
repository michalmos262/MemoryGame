using System;

namespace MemoryGame
{
    public class Board
    {
        private int[,] m_BoardShownToUser;
        private int[,] m_BoardWithRevealedCards;
        private int m_NumOfRows;
        private int m_NumOfColumns;

        public int NumOfRows
        {
            get
            {
                return m_NumOfRows;
            }
        }

        public int NumOfColumns
        {
            get
            {
                return m_NumOfColumns;
            }
        }

        public ref int[,] BoardShownToUser
        {
            get
            {
                return ref m_BoardShownToUser;
            }
        }

        public Board(int i_NumOfRows, int i_NumOfColumns)
        {
            m_NumOfRows = i_NumOfRows;
            m_NumOfColumns = i_NumOfColumns;
            m_BoardShownToUser = new int[i_NumOfRows, i_NumOfColumns];
            m_BoardWithRevealedCards = new int[i_NumOfRows, i_NumOfColumns];
            fillBoardWithRevealedCards();
        }

        private void fillBoardWithRevealedCards()
        {
            int[] values = generateValues();

            Random rand = new Random();
            for (int i = values.Length - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1);
                int temp = values[i];
                values[i] = values[j];
                values[j] = temp;
            }

            int shuffleIndex = 0;
            for (int i = 0; i < m_NumOfRows; i++)
            {
                for (int j = 0; j < m_NumOfColumns; j++)
                {
                    m_BoardWithRevealedCards[i, j] = values[shuffleIndex];
                    shuffleIndex++;
                }
            }
        }
        private int[] generateValues()
        {
            int numOfCards = m_NumOfRows * m_NumOfColumns;
            int numOfPairs = numOfCards / 2;
            int[] values = new int[numOfCards];

            for (int i = 0; i < numOfPairs; i++)
            {
                values[i * 2] = i + 1;
                values[i * 2 + 1] = i + 1;
            }

            return values;
        }

        public int ShowCellToUser(int row, int column)
        {
            m_BoardShownToUser[row, column] = m_BoardWithRevealedCards[row, column];
            return m_BoardWithRevealedCards[row, column];
        }

        public int GetCell(int row, int column)
        {
            return m_BoardWithRevealedCards[row, column];
        }
    }
}