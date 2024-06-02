using System;
using System.Collections.Generic;

namespace MemoryGame
{
    public class MemoryGame
    {
        private GameBoard m_Board;
        private Player[] m_Players;
        private uint m_CurrentPlayerIndex;
        private bool m_IsGameOver;
        private const uint k_NumOfPlayers = 2;

        public MemoryGame()
        {
            m_Board = null;
            m_CurrentPlayerIndex = 0;
            m_IsGameOver = false;
            m_Players = new Player[k_NumOfPlayers];
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

        public void SetPlayers()
        {
            string firstPlayerName = UI.GetPlayerName();
            m_Players[0] = new Player(firstPlayerName);
            eGameModes gameMode = UI.GetGameMode();
            if (gameMode == eGameModes.HumanVsComputer)
            {
                m_Players[1] = new Player();
            }
            else
            {
                string secondPlayerName = UI.GetPlayerName();
                m_Players[1] = new Player(secondPlayerName);
            }
        }

        public void SetBoard()
        {
            int numOfRows = UI.GetBoardNumOfRows();
            //TODO: verify num of rows is correct logically
            int numOfColumns = UI.GetBoardNumOfColumns();
            //TODO: verify num of columns is correct logically
            m_Board = new GameBoard(numOfRows, numOfColumns);
        }

        public void Play()
        {
            SetPlayers();
            SetBoard();
            UI.ShowGameBoard(m_Board);
            
        }
    }
}