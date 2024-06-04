using System.Collections.Generic;
using System;

namespace MemoryGame
{
    public class GameManager
    {
        private GameBoard m_Board;
        private Player[] m_Players;
        private uint m_CurrentPlayerIndex;
        private bool m_IsGameOver;
        private const uint k_NumOfPlayers = 2;

        public GameManager()
        {
            m_Board = null;
            m_CurrentPlayerIndex = 0;
            m_IsGameOver = false;
            m_Players = new Player[k_NumOfPlayers];
        }

        public void MakeActivePlayerFirstTurn(GameBoard.Position i_FirstCardPosition)
        {
            Player activePlayer;

            activePlayer = GetActivePlayer();
            if (activePlayer != null)
            {
                revealCardInBoard(i_FirstCardPosition);
            }
        }

        public void MakeActivePlayerSecondTurn(GameBoard.Position i_FirstCardPosition, GameBoard.Position i_SecondCardPosition)
        {
            Player activePlayer;
            activePlayer = GetActivePlayer();
            if (activePlayer != null)
            {
                if (isPairAtPositions(i_FirstCardPosition, i_SecondCardPosition))
                {
                    revealPairAtPositions(i_FirstCardPosition, i_SecondCardPosition);
                    activePlayer.Score++;
                }
                else
                {
                    passTurn();
                }
            }
        }

        private void passTurn()
        {
            m_CurrentPlayerIndex = (m_CurrentPlayerIndex + 1) % k_NumOfPlayers;
        }

        public Player GetActivePlayer()
        {
            Player activePlayer;

            if (wasGameInitialized())
            {
                activePlayer = m_Players[m_CurrentPlayerIndex];
            }
            else
            {
                activePlayer = null;
            }

            return activePlayer;
        }

        private bool isGameOver()
        {
            m_IsGameOver = m_Board.AreAllCardsRevealed();

            return m_IsGameOver;
        }

        public Player GetWinner()
        {
            Player winner;

            if (isGameOver())
            {
                winner = getPlayerWithHighestScore();
            }
            else
            {
                winner = null;
            }

            return winner;
        }

        private Player getPlayerWithHighestScore()
        {
            int maxScore = m_Players[0].Score;
            Player playerWithHighestScore = m_Players[0];

            foreach (Player player in m_Players)
            {
                if (player.Score > maxScore)
                {
                    maxScore = player.Score;
                    playerWithHighestScore = player;
                }
            }

            return playerWithHighestScore;
        }

        public ePositionStatus GetPositionChoiceStatus(GameBoard.Position i_ChosenPosition)
        {
            ePositionStatus positionStatus;

            if (!m_Board.IsValidPosition(i_ChosenPosition))
            {
                positionStatus = ePositionStatus.OutsideBoard;
            }
            else if (m_Board.IsCardRevealed(i_ChosenPosition))
            {
                positionStatus = ePositionStatus.RevealedPosition;
            }
            else
            {
                positionStatus = ePositionStatus.Valid;
            }

            return positionStatus;
        }

        public static GameBoard.Position ChooseRandomHiddenCellInBoard(GameBoard board)
        {
            Random random = new Random();
            List<GameBoard.Position> hiddenBoardCells = board.GetCurrentHiddenCells();

            int randomIndex = random.Next(hiddenBoardCells.Count);
            return hiddenBoardCells[randomIndex];
        }


        private Card revealCardInBoard(GameBoard.Position i_Position)
        {
            Card revealedCard = new Card(Card.k_InvalidCard);

            if (GetPositionChoiceStatus(i_Position) is ePositionStatus.Valid)
            {
                revealedCard = m_Board.RevealCard(i_Position);
            }

            return revealedCard;
        }

        public void hidePairInBoard(GameBoard.Position i_FirstCardPosition, GameBoard.Position i_SecondCardPosition)
        {
            hideCardInBoard(i_FirstCardPosition);
            hideCardInBoard(i_SecondCardPosition);
        }

        private void hideCardInBoard(GameBoard.Position i_Position)
        {
            if (GetPositionChoiceStatus(i_Position) is ePositionStatus.Valid)
            {
                m_Board.HideCard(i_Position);
            }
        }

        public void SetPlayers(string[] i_Names, bool[] i_isHumanArray)
        {
            if (i_Names.Length == i_isHumanArray.Length)
            {
                for (int i = 0; i < i_Names.Length; i++)
                {
                    m_Players[i] = new Player(i_Names[i], i_isHumanArray[i]);
                }
            }
        }

        public void SetBoardDimensions(int i_numOfRows, int i_numOfColumns)
        {
            if (areBoardDimensionsValid(i_numOfRows, i_numOfColumns))
            {
                m_Board = new GameBoard(i_numOfRows, i_numOfColumns);
            }
        }

        private bool areBoardDimensionsValid(int i_numOfRows, int i_numOfColumns)
        {
            return GameBoard.AreDimensionsValid(i_numOfRows, i_numOfColumns);
        }

        private bool isPairAtPositions(GameBoard.Position i_FirstCardPosition, GameBoard.Position i_SecondCardPosition)
        {
            return m_Board.IsPairAtPositions(i_FirstCardPosition, i_SecondCardPosition);
        }

        private void revealPairAtPositions(GameBoard.Position i_FirstCardPosition, GameBoard.Position i_SecondCardPosition)
        {
            m_Board.RevealIfPair(i_FirstCardPosition, i_SecondCardPosition);
        }

        private bool wasGameInitialized()
        {
            return m_Board == null;
        }
    }
}