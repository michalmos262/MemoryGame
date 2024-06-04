using System;
using System.Collections.Generic;

namespace MemoryGame
{
    public class GameBoard
    {
        private Card[,] m_Board;
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

        public Card[,] Board
        {
            get
            {
                return m_Board;
            }
        }

        public int GetNumOfCardPairs()
        {
            return m_NumOfRows * m_NumOfColumns / 2;
        }

        public GameBoard(int i_NumOfRows, int i_NumOfColumns)
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
            while (temporaryCardsAmount > 1)
            {
                temporaryCardsAmount--;
                int randomIndex = rand.Next(temporaryCardsAmount + 1);
                Card temporaryCard = cards[randomIndex];
                cards[randomIndex] = cards[temporaryCardsAmount];
                cards[temporaryCardsAmount] = temporaryCard;
            }

            int cardIndexInCards = 0;
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
            int numOfCards = m_NumOfRows * m_NumOfColumns;
            int numOfPairs = numOfCards / 2;
            Card[] cards = new Card[numOfCards];

            for (int i = 0; i < numOfPairs; i++)
            {
                cards[i * 2] = new Card(i + 1);
                cards[i * 2 + 1] = new Card(i + 1);
            }

            return cards;
        }

        public Card RevealCard(Position position)
        {
            m_Board[position.RowIndex, position.ColumnIndex].IsRevealed = true;
            return m_Board[position.RowIndex, position.ColumnIndex];
        }

        public void RevealPair(Position i_FirstCardPosition, Position i_SecondCardPosition)
        {
            if (IsPair(i_FirstCardPosition, i_SecondCardPosition))
            {
                RevealCard(i_FirstCardPosition);
                RevealCard(i_SecondCardPosition);
            }
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

        public bool IsValidPosition(Position i_Position)
        {
            return i_Position.RowIndex < m_NumOfRows && i_Position.ColumnIndex < m_NumOfColumns;
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

        public bool IsPair(Position i_FirstCardPosition, Position i_SecondCardPosition)
        {
            Card firstCard, secondCard;

            firstCard = m_Board[i_FirstCardPosition.RowIndex, i_FirstCardPosition.ColumnIndex];
            secondCard = m_Board[i_SecondCardPosition.RowIndex, i_SecondCardPosition.ColumnIndex];

            return firstCard.Number == secondCard.Number;
        }

        public static bool AreDimensionsValid(int i_NumOfRows, int i_NumOfColumns)
        {
            return (i_NumOfRows * i_NumOfColumns) % 2 == 0;
        }
    }
}