using System;

namespace MemoryGame
{
    internal class GameManager
    {
        private eGameModes m_GameMode;
        private GameBoard m_Board;
        private Player[] m_Players;
        private uint m_CurrentPlayerIndex;
        private bool m_IsGameOver;
        private const uint k_NumOfPlayers = 2;

        public GameManager(eGameModes i_GameMode, int i_NumOfRowsInBoard, int i_NumOfColumnsInBoard)
        {
            m_GameMode = i_GameMode;
            m_Board = new GameBoard(i_NumOfRowsInBoard, i_NumOfColumnsInBoard);
            m_CurrentPlayerIndex = 0;
            m_IsGameOver = false;
            m_Players = new Player[k_NumOfPlayers];
            for (int i = 0; i < k_NumOfPlayers; i++)
            {
                m_Players[i] = new Player();
            }
        }

        public GameBoard GameBoard
        {
            get
            {
                return m_Board;
            }
        }

        private void passTurn()
        {
            m_CurrentPlayerIndex = (m_CurrentPlayerIndex + 1) % k_NumOfPlayers;
        }

        private bool isGameOver()
        {
            return m_Board.AreAllCardsRevealed();
        }

        public ePositionStatus GetPositionChoiceStatus(GameBoard.Position i_ChosenPosition)
        {
            ePositionStatus positionStatus;

            if(!m_Board.IsValidPosition(i_ChosenPosition))
            {
                positionStatus = ePositionStatus.OutsideBoard;
            }
            else if(m_Board.IsCardRevealed(i_ChosenPosition))
            {
                positionStatus = ePositionStatus.RevealedPosition;
            }
            else
            {
                positionStatus = ePositionStatus.Valid;
            }

            return positionStatus;
        }

        public Player GetWinner()
        {
            int maxScore, currentPlayerScore;
            Player winner;

            if (isGameOver())
            {
                winner = m_Players[0];
                maxScore = m_Players[0].GetNumOfRevealedCards();

                foreach (Player player in m_Players)
                {
                    currentPlayerScore = player.GetNumOfRevealedCards();
                    if (currentPlayerScore > maxScore)
                    {
                        winner = player;
                        maxScore = currentPlayerScore;
                    }
                }
            }
            else
            {
                winner = null;
            }

            return winner;
        }
    }
}
