﻿using System.Collections.Generic;
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
        private bool m_isInputNeeded;

        public GameManager()
        {
            m_Board = null;
            m_CurrentPlayerIndex = 0;
            m_IsGameOver = false;
            m_Players = new Player[k_NumOfPlayers];
            m_isInputNeeded = true;
        }

        public GameBoard Board
        {
            get
            {
                return m_Board;
            }
        }

        public uint NumOfPlayers
        {
            get
            {
                return k_NumOfPlayers;
            }
        }

        public bool IsGameOver
        {
            get
            {
                return m_IsGameOver;
            }
        }

        public bool IsInputNeeded
        {
            get
            {
                return m_isInputNeeded;
            }
        }

        public Player[] Players
        {
            get
            {
                return m_Players;
            }
        }

        public void MakeActivePlayerFirstTurn(ref GameBoard.Position io_FirstCardPosition)
        {
            Player activePlayer = GetActivePlayer();

            if (activePlayer != null)
            {
                if (!activePlayer.IsHuman)
                {
                    io_FirstCardPosition = ChooseRandomHiddenCellInBoard();
                }

                RevealCardInBoard(io_FirstCardPosition);
            }
        }

        public void MakeActivePlayerSecondTurn(ref GameBoard.Position io_FirstCardPosition, ref GameBoard.Position io_SecondCardPosition)
        {
            Player activePlayer = GetActivePlayer();
            bool isValidPair;

            if (activePlayer != null)
            {
                if (!activePlayer.IsHuman)
                {
                    io_SecondCardPosition = ChooseRandomHiddenCellInBoard();
                }

                RevealCardInBoard(io_SecondCardPosition);
                isValidPair = m_Board.IsValidPairAtPositions(io_FirstCardPosition, io_SecondCardPosition);

                if (isValidPair)
                {
                    activePlayer.Score++;
                    m_IsGameOver = m_Board.AreAllCardsRevealed();
                }
                else
                {
                    HidePairInBoard(io_FirstCardPosition, io_SecondCardPosition);
                    passTurn();
                }
            }
        }

        private void setInputIsNeeded()
        {
            if (!m_Players[m_CurrentPlayerIndex].IsHuman)
            {
                m_isInputNeeded = false;
            }
            else
            {
                m_isInputNeeded = true;
            }
        }

        private void passTurn()
        {
            m_CurrentPlayerIndex = (m_CurrentPlayerIndex + 1) % k_NumOfPlayers;
            setInputIsNeeded();
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

            if (!m_Board.IsPositionInRange(i_ChosenPosition))
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

        public GameBoard.Position ChooseRandomHiddenCellInBoard()
        {
            Random random = new Random();
            List<GameBoard.Position> hiddenBoardCells = m_Board.GetCurrentHiddenCells();
            int randomIndex = random.Next(hiddenBoardCells.Count);

            return hiddenBoardCells[randomIndex];
        }


        public Card RevealCardInBoard(GameBoard.Position i_Position)
        {
            Card revealedCard = new Card(Card.k_InvalidCardIndicator);

            if (GetPositionChoiceStatus(i_Position) is ePositionStatus.Valid)
            {
                revealedCard = m_Board.RevealCard(i_Position);

            }

            return revealedCard;
        }

        public void HidePairInBoard(GameBoard.Position i_FirstCardPosition, GameBoard.Position i_SecondCardPosition)
        {
            hideCardInBoard(i_FirstCardPosition);
            hideCardInBoard(i_SecondCardPosition);
        }

        private void hideCardInBoard(GameBoard.Position i_Position)
        {
            if (GetPositionChoiceStatus(i_Position) is ePositionStatus.RevealedPosition)
            {
                m_Board.HideCard(i_Position);
            }
        }

        public void SetPlayers(string[] i_Names, bool[] i_IsHumanArray)
        {
            if (i_Names.Length == i_IsHumanArray.Length)
            {
                for (int i = 0; i < i_Names.Length; i++)
                {
                    m_Players[i] = new Player(i_Names[i], i_IsHumanArray[i]);
                }
            }
        }

        public void ResetGame()
        {
            m_CurrentPlayerIndex = 0;
            m_IsGameOver = false;
            m_isInputNeeded = true;

            foreach (Player player in m_Players)
            {
                player.Score = 0;
            }
        }

        public void SetBoardDimensions(uint i_NumOfRows, uint i_NumOfColumns)
        {
            if (GameBoard.AreDimensionsValid(i_NumOfRows, i_NumOfColumns))
            {
                m_Board = new GameBoard(i_NumOfRows, i_NumOfColumns);
            }
        }

        private bool wasGameInitialized()
        {
            return m_Board != null;
        }
    }
}