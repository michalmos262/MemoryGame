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
        private eGameModes m_gameMode;

        public GameManager()
        {
            m_Board = null;
            m_CurrentPlayerIndex = 0;
            m_IsGameOver = false;
            m_Players = new Player[k_NumOfPlayers];
            m_gameMode = eGameModes.ErrorMode;
        }

        internal eGameModes GameMode
        {
            get
            {
                return m_gameMode;
            }
            set
            {
                m_gameMode = value;
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


        //TODO: RevealCardInBoard
        public void RevealCardInBoard(GameBoard.Position position)
        {

        }

        public void SetPlayers(string i_firstPlayerName, string i_secondPlayerName)
        {
            m_Players[0] = new Player(i_firstPlayerName, true);
            if (m_gameMode == eGameModes.HumanVsComputer)
            {
                m_Players[1] = new Player("", false);
            }
            else
            {
                m_Players[1] = new Player(i_secondPlayerName, true);
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
            return (i_numOfRows * i_numOfColumns) % 2 == 0;
        }

    }
}