using System;
using System.Collections.Generic;

namespace MemoryGame
{
    public class GameBoard
    {
        private Card[,] m_Board;
        private uint m_NumOfRows;
        private uint m_NumOfColumns;

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
                set
                {
                    m_RowIndex = value;
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
        public uint NumOfRows
        {
            get
            {
                return m_NumOfRows;
            }
        }

        public uint NumOfColumns
        {
            get
            {
                return m_NumOfColumns;
            }
        }

        public GameBoard(uint i_NumOfRows, uint i_NumOfColumns)
        {
            m_NumOfRows = i_NumOfRows;
            m_NumOfColumns = i_NumOfColumns;
            m_Board = new Card[i_NumOfRows, i_NumOfColumns];
            fillBoardWithHiddenCards();
        }

        private void fillBoardWithHiddenCards()
        {
            Card[] cards = generateRandomCards();
            Random rand = new Random();
            int temporaryCardsAmount = cards.Length;
            int cardIndexInCards = 0;

            while (temporaryCardsAmount > 1)
            {
                temporaryCardsAmount--;
                int randomIndex = rand.Next(temporaryCardsAmount + 1);
                Card temporaryCard = cards[randomIndex];
                cards[randomIndex] = cards[temporaryCardsAmount];
                cards[temporaryCardsAmount] = temporaryCard;
            }
            for (int i = 0; i < m_NumOfRows; i++)
            {
                for (int j = 0; j < m_NumOfColumns; j++)
                {
                    m_Board[i, j] = cards[cardIndexInCards];
                    cardIndexInCards++;
                }
            }
        }
        private Card[] generateRandomCards()
        {
            uint numOfCards = m_NumOfRows * m_NumOfColumns;
            uint numOfPairs = numOfCards / 2;
            Card[] cards = new Card[numOfCards];

            for (int i = 0; i < numOfPairs; i++)
            {
                cards[i * 2] = new Card(i + 1);
                cards[i * 2 + 1] = new Card(i + 1);
            }

            return cards;
        }

        public Card RevealCard(Position i_Position)
        {
            m_Board[i_Position.RowIndex, i_Position.ColumnIndex].IsRevealed = true;

            return m_Board[i_Position.RowIndex, i_Position.ColumnIndex];
        }

        public void HideCard(Position i_Position)
        {
            m_Board[i_Position.RowIndex, i_Position.ColumnIndex].IsRevealed = false;
        }

        public Card GetCardInPosition(Position position)
        {
            return m_Board[position.RowIndex, position.ColumnIndex];
        }

        public List<Position> GetCurrentHiddenCells()
        {
            List<Position> currentHiddenBoardCells = new List<Position>();

            for (int i = 0; i < m_NumOfRows; i++)
            {
                for (int j = 0; j < m_NumOfColumns; j++)
                {
                    if (m_Board[i, j].IsRevealed == false)
                    {
                        currentHiddenBoardCells.Add(new GameBoard.Position(i, j));
                    }
                }
            }

            return currentHiddenBoardCells;
        }

        public bool IsPositionInRange(Position i_Position)
        {
            bool isPositiveRowIndex = i_Position.RowIndex >= 0;
            bool isRowIndexInBoard = i_Position.RowIndex < m_NumOfRows;
            bool isPositiveColumnIndex = i_Position.ColumnIndex >= 0;
            bool isColumnIndexInBoard = i_Position.ColumnIndex < m_NumOfColumns;

            return isPositiveRowIndex && isRowIndexInBoard && isPositiveColumnIndex && isColumnIndexInBoard;
        }

        public bool IsCardRevealed(Position i_Position)
        {
            Card cardInPosition = GetCardInPosition(i_Position);

            return cardInPosition.IsRevealed;
        }

        public bool AreAllCardsRevealed()
        {
            List<Position> currentHiddenBoardCells = GetCurrentHiddenCells();

            return currentHiddenBoardCells.Count == 0;
        }

        public bool IsValidPairAtPositions(Position i_FirstCardPosition, Position i_SecondCardPosition)
        {
            Card firstCard, secondCard;

            firstCard = m_Board[i_FirstCardPosition.RowIndex, i_FirstCardPosition.ColumnIndex];
            secondCard = m_Board[i_SecondCardPosition.RowIndex, i_SecondCardPosition.ColumnIndex];

            return firstCard.Number == secondCard.Number;
        }

        public static bool AreDimensionsValid(uint i_NumOfRows, uint i_NumOfColumns)
        {
            return (i_NumOfRows * i_NumOfColumns) % 2 == 0;
        }
    }
}