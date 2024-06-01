using System;
using System.Collections.Generic;
using static MemoryGame.Board;

namespace MemoryGame
{
    public class Board
    {
        private int[,] m_Board;
        private int m_NumOfRows;
        private int m_NumOfColumns;

        public struct Position
        {
            private int m_RowIndex;
            private int m_ColumnIndex;

            public Position(int i_RowIndex, int i_ColumnIndex)
            {
                m_RowIndex = i_RowIndex;
                m_ColumnIndex = i_ColumnIndex;
            }

            public int RowIndex
            {
                get
                {
                    return m_RowIndex;
                }
            }

            public int ColumnIndex
            {
                get
                {
                    return m_ColumnIndex;
                }
            }
        }

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
                return ref m_Board;
            }
        }

        public Board(int i_NumOfRows, int i_NumOfColumns)
        {
            m_NumOfRows = i_NumOfRows;
            m_NumOfColumns = i_NumOfColumns;
            fillBoardWithHiddenCards();
        }

        private void fillBoardWithHiddenCards()
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
                    m_Board[i, j] = values[shuffleIndex];
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

        public int ShowCellToUser(Position position)
        {
            m_BoardShownToUser[position.RowIndex, position.ColumnIndex] = m_BoardWithRevealedCards[position.RowIndex, position.ColumnIndex];
            return m_BoardWithRevealedCards[position.RowIndex, position.ColumnIndex];
        }

        public int GetCell(Position position)
        {
            return m_BoardWithRevealedCards[position.RowIndex, position.ColumnIndex];
        }

        public void GetHiddenCells()
        {
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
        }
    }
}