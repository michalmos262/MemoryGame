using System;

namespace MemoryGame
{
    internal class GameManager
    {
        private eGameModes m_GameMode;
        private Board m_Board;
        private Player[] m_Players;
        private uint m_CurrentPlayerIndex;
        private bool m_IsGameOver;
        private const uint k_NumOfPlayers = 2;

        public GameManager(eGameModes i_GameMode, int i_NumOfRowsInBoard, int i_NumOfColumnsInBoard)
        {
            m_GameMode = i_GameMode;
            m_Board = new Board(i_NumOfRowsInBoard, i_NumOfColumnsInBoard);
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

        public eTurnChoiceStatus GetChoiceStatus(int i_Row, int i_Column)
        {
            eTurnChoiceStatus choiceStatus;

            if(!m_Board.isInBoard(i_Row, i_Column))
            {
                choiceStatus = eTurnChoiceStatus.OutsideBoard;
            }
            else if(m_Board.isCardRevealed(i_Row, i_Column))
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
