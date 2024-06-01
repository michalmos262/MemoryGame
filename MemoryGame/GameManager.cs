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

        private void passTurn()
        {
            m_CurrentPlayerIndex = (m_CurrentPlayerIndex + 1) % (uint)m_Players.Length;
        }

        private bool isGameOver()
        {
            return m_Board.AreAllCardsRevealed();
        }

        public eTurnChoiceStatus GetChoiceStatus(GameBoard.Position i_ChosenPosition)
        {
            eTurnChoiceStatus choiceStatus;

            if(!m_Board.IsInBoard(i_ChosenPosition))
            {
                choiceStatus = eTurnChoiceStatus.OutsideBoard;
            }
            else if(m_Board.IsCardRevealed(i_ChosenPosition))
            {
                choiceStatus = eTurnChoiceStatus.TakenCell;
            }
            else
            {
                choiceStatus = eTurnChoiceStatus.Valid;
            }

            return choiceStatus;
        }
    }
}
