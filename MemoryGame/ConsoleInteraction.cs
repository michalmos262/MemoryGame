using System;
using System.Collections.Generic;
using MemoryGame;

namespace UI
{
    public class ConsoleInteraction
    {
        private GameManager m_GameManager;
        private const uint k_BoardMinNumOfRows = 4;
        private const uint k_BoardMinNumOfColumns = 4;
        private const uint k_BoardMaxNumOfRows = 6;
        private const uint k_BoardMaxNumOfColumns = 6;
        private const string k_QuitButton = "Q";
        private const string k_PlayAnotherGameButton = "Y";
        private const int k_RevealTimeInMilliseconds = 2000;
        private bool m_IsQuit;

        private void clearScreen()
        {
            Ex02.ConsoleUtils.Screen.Clear();
        }

        private void showGameBoard()
        {
            printColumnLabels();
            Console.WriteLine("  " + new string('=', (int)(m_GameManager.Board.NumOfColumns * 4) + 1));
            printGameBoard();
        }

        private void printColumnLabels()
        {
            char columnLabel;

            Console.Write("   ");
            for (int col = 0; col < m_GameManager.Board.NumOfColumns; col++)
            {
                columnLabel = (char)('A' + col);
                Console.Write($" {columnLabel}  ");
            }

            Console.WriteLine();
        }

        private void printGameBoard()
        {
            Card currentCard;

            for (int row = 0; row < m_GameManager.Board.NumOfRows; row++)
            {
                Console.Write($"{row + 1} | ");
                for (int col = 0; col < m_GameManager.Board.NumOfColumns; col++)
                {
                    GameBoard.Position position = new GameBoard.Position(row, col);
                    currentCard = m_GameManager.Board.GetCardInPosition(position);
                    if (currentCard.IsRevealed)
                    {
                        char cardIconToPrint = (char)('A' + currentCard.Number);
                        Console.Write(cardIconToPrint);
                    }
                    else
                    {
                        Console.Write(" ");
                    }

                    Console.Write(" | ");
                }

                Console.WriteLine();
                Console.WriteLine("  " + new string('=', (int)(m_GameManager.Board.NumOfColumns * 4) + 1));
            }
        }

        private void sleep(int i_RevealTimeInMilliseconds = k_RevealTimeInMilliseconds)
        {
            System.Threading.Thread.Sleep(i_RevealTimeInMilliseconds);
        }

        private string getPlayerName(int i_PlayerIndex)
        {
            Console.WriteLine($"Please enter the name of player {i_PlayerIndex + 1}:");
            string name = Console.ReadLine();

            return name;
        }

        private eGameModes getGameMode()
        {
            uint optionNumberFromUser;
            string userInput;
            bool isOptionNumber;

            Console.WriteLine($"Who do you want to play with? Please insert the right option number:\n" +
                              $"({(int)eGameModes.HumanVsComputer}) Computer\n" +
                              $"({(int)eGameModes.HumanVsHuman}) Another player");
            userInput = Console.ReadLine();
            isOptionNumber = uint.TryParse(userInput, out optionNumberFromUser);
            while (!isOptionNumber || !isValidGameMode((eGameModes)optionNumberFromUser))
            {
                Console.WriteLine("Wrong input! Please try again:");
                userInput = Console.ReadLine();
                isOptionNumber = uint.TryParse(userInput, out optionNumberFromUser);
            }

            eGameModes gameMode = (eGameModes)optionNumberFromUser;

            return gameMode;
        }

        private bool isValidGameMode(eGameModes i_GameMode)
        {
            return i_GameMode == eGameModes.HumanVsHuman || i_GameMode == eGameModes.HumanVsComputer;
        }

        private uint getBoardNumOfRows()
        {
            Console.WriteLine("Enter the number of the board rows:");
            uint numOfRowsFromUser;
            string userInput = Console.ReadLine();
            bool isOptionNumber = uint.TryParse(userInput, out numOfRowsFromUser);

            while (!(isOptionNumber && isValidNumOfBoardRows(numOfRowsFromUser)))
            {
                Console.WriteLine(
                    $"Wrong input! the number of rows should be between {k_BoardMinNumOfRows} to {k_BoardMaxNumOfRows}. Please try again:");
                userInput = Console.ReadLine();
                isOptionNumber = uint.TryParse(userInput, out numOfRowsFromUser);
            }

            return numOfRowsFromUser;
        }

        private uint getBoardNumOfColumns()
        {
            Console.WriteLine("Enter the number of the board columns:");
            uint numOfColumnsFromUser;
            string userInput = Console.ReadLine();
            bool isOptionNumber = uint.TryParse(userInput, out numOfColumnsFromUser);

            while (!(isOptionNumber && isValidNumOfBoardColumns(numOfColumnsFromUser)))
            {
                Console.WriteLine(
                    $"Wrong input! the number of columns should be between {k_BoardMinNumOfColumns} to {k_BoardMaxNumOfColumns}. Please try again:");
                userInput = Console.ReadLine();
                isOptionNumber = uint.TryParse(userInput, out numOfColumnsFromUser);
            }

            return numOfColumnsFromUser;
        }

        private bool isValidPositionFormat(string i_UserInput)
        {
            bool isValidPosition = false;
            bool isFirstLetterCharacter = char.IsUpper(i_UserInput[0]);
            string inputWithoutFirstChar = i_UserInput.Substring(1);
            bool isDigitAfterCharacter = int.TryParse(inputWithoutFirstChar, out _);

            if (isFirstLetterCharacter && isDigitAfterCharacter)
            {
                GameBoard.Position position = getTextConvertedToPosition(i_UserInput);
                adjustPositionToLogics(ref position);
                ePositionStatus positionStatus = m_GameManager.GetPositionChoiceStatus(position);
                if (positionStatus == ePositionStatus.RevealedPosition)
                {
                    printPositionAlreadyRevealed(i_UserInput);
                }
                else if (positionStatus == ePositionStatus.OutsideBoard)
                {
                    printPositionNotInBoardRange(i_UserInput);
                }
                else
                {
                    isValidPosition = true;
                }
            }
            else
            {
                printPositionInputNotInsertedCorrectly(i_UserInput);
            }

            return isValidPosition;
        }

        private bool isValidNumOfBoardRows(uint i_NumOfBoardRows)
        {
            return i_NumOfBoardRows >= k_BoardMinNumOfRows && i_NumOfBoardRows <= k_BoardMaxNumOfRows;
        }

        private bool isValidNumOfBoardColumns(uint i_NumOfBoardColumns)
        {
            return i_NumOfBoardColumns >= k_BoardMinNumOfColumns && i_NumOfBoardColumns <= k_BoardMaxNumOfColumns;
        }

        private void printPositionNotInBoardRange(string i_PositionInput)
        {
            Console.WriteLine($"Position {i_PositionInput} is not in the board range!");
        }

        private void printPositionAlreadyRevealed(string i_PositionInput)
        {
            Console.WriteLine($"The card in position {i_PositionInput} is already revealed!");
        }

        private void printPositionInputNotInsertedCorrectly(string i_PositionInput)
        {
            Console.WriteLine($"Position {i_PositionInput} is not in the right format! " +
                              $"A correct position can be for example: E3 (mean column E, row 3)");
        }

        private GameBoard.Position getTextConvertedToPosition(string i_PositionText)
        {
            string rowString = i_PositionText.Substring(1);
            int row = int.Parse(rowString);
            int column = i_PositionText[0] - 'A';
            GameBoard.Position position = new GameBoard.Position(row, column);

            return position;
        }
        
        private void setQuitByInput(string i_UserInput)
        {
            if (i_UserInput == k_QuitButton)
            {
                m_IsQuit = true;
            }
        }

        private string getBoardPositionFromUser()
        {
            Console.WriteLine("Enter a board position to reveal a card (for example position E3 means column E, row 3):");
            string userInput = Console.ReadLine();
            setQuitByInput(userInput);
            while (!m_IsQuit && !isValidPositionFormat(userInput))
            {
                userInput = Console.ReadLine();
                setQuitByInput(userInput);
            }

            return userInput;
        }

        private void setPlayers()
        {
            string playerNameInput = getPlayerName(0);

            m_GameManager.AddPlayer(playerNameInput, true);
            eGameModes gameMode = getGameMode();
            for (int i = 1; i < m_GameManager.PossibleNumOfPlayers; i++)
            {
                if (gameMode == eGameModes.HumanVsHuman)
                {
                    playerNameInput = getPlayerName(i);
                    m_GameManager.AddPlayer(playerNameInput, true);
                }
                else
                {
                    m_GameManager.AddPlayer(null, false);
                }
            }
        }

        private void setBoard()
        {
            uint numOfBoardRows = getBoardNumOfRows();
            uint numOfBoardColumns = getBoardNumOfColumns();

            while (!GameBoard.AreDimensionsValid(numOfBoardRows, numOfBoardColumns))
            {
                Console.WriteLine("Board size should be even! Please try again:");
                numOfBoardRows = getBoardNumOfRows();
                numOfBoardColumns = getBoardNumOfColumns();
            }

            m_GameManager.SetBoardDimensions(numOfBoardRows, numOfBoardColumns);
        }

        private GameBoard.Position getBoardPosition(string i_BoardPositionInput)
        {
            GameBoard.Position chosenPosition = new GameBoard.Position();

            if (i_BoardPositionInput != null)
            {
                chosenPosition = getTextConvertedToPosition(i_BoardPositionInput);

                adjustPositionToLogics(ref chosenPosition);
            }

            return chosenPosition;
        }

        private void adjustPositionToLogics(ref GameBoard.Position io_BoardPosition)
        {
            io_BoardPosition.RowIndex--;
        }

        private void printCurrentPlayerTurn()
        {
            Player currentPlayer;

            currentPlayer = m_GameManager.GetActivePlayer();
            Console.WriteLine($"{currentPlayer.Name}'s turn:");
        }

        private void makePlayerTurn()
        {
            GameBoard.Position[] chosenBoardPositions = new GameBoard.Position[m_GameManager.PossibleTotalCardsToReveal];
            string validBoardPositionInput = null;

            printCurrentPlayerTurn();
            if (m_GameManager.IsInputNeeded)
            {
                validBoardPositionInput = getBoardPositionFromUser();
            }
            if (!m_IsQuit)
            {
                chosenBoardPositions[0] = getBoardPosition(validBoardPositionInput);
                askGameManagerToMakeFirstTurnAndShowBoard(ref chosenBoardPositions[0]);

                printCurrentPlayerTurn();
                if (m_GameManager.IsInputNeeded)
                {
                    validBoardPositionInput = getBoardPositionFromUser();
                }
                else
                {
                    sleep(1000);
                }
                if (!m_IsQuit)
                {
                    chosenBoardPositions[1] = getBoardPosition(validBoardPositionInput);
                    askGameManagerToMakeSecondTurnAndShowBoard(ref chosenBoardPositions);
                }
            }
        }

        private void askGameManagerToMakeFirstTurnAndShowBoard(ref GameBoard.Position io_FirstCardPosition)
        {
            m_GameManager.MakeActivePlayerFirstTurn(ref io_FirstCardPosition);
            clearScreen();
            showGameBoard();
        }

        private void askGameManagerToMakeSecondTurnAndShowBoard(ref GameBoard.Position[] io_ChosenBoardPositions)
        {
            bool isPairFound;

            m_GameManager.MakeActivePlayerSecondTurn(ref io_ChosenBoardPositions[0], ref io_ChosenBoardPositions[1]);
            isPairFound = m_GameManager.Board.IsValidPairAtPositions(io_ChosenBoardPositions[0], io_ChosenBoardPositions[1]);
            clearScreen();
            if (isPairFound)
            {
                showGameBoard();
            }
            else
            {
                showUserWrongPair(io_ChosenBoardPositions);
            }
        }

        private void showUserWrongPair(GameBoard.Position[] i_ChosenBoardPositions)
        {
            m_GameManager.RevealCardInBoard(i_ChosenBoardPositions[0]);
            m_GameManager.RevealCardInBoard(i_ChosenBoardPositions[1]);
            showGameBoard();
            sleep();
            m_GameManager.HidePairInBoard(i_ChosenBoardPositions[0], i_ChosenBoardPositions[1]);
            clearScreen();
            showGameBoard();
        }

        private void initGame()
        {
            m_GameManager = new GameManager();
            m_IsQuit = false;
            setPlayers();
        }

        private void setGameBoard()
        {
            setBoard();
            clearScreen();
        }

        private void startNewGame()
        {
            m_GameManager.ResetGame();
            m_IsQuit = false;
            setGameBoard();
            play();
        }

        private void play()
        {
            showGameBoard();
            while (!m_IsQuit && !m_GameManager.IsGameOver)
            {
                makePlayerTurn();
            }

            finishPlay();
        }

        public void PlayGame()
        {
            initGame();
            startNewGame();
        }

        private void printUserQuit()
        {
            Console.WriteLine("User quit the game");
        }

        private void finishPlay()
        {
            if (m_IsQuit)
            {
                printUserQuit();
            }
            else
            {
                Console.WriteLine("Game is over!\n");
                printStatistics();
                if (isReplayRequested())
                {
                    clearScreen();
                    startNewGame();
                }
                else
                {
                    printUserQuit();
                }
            }
        }

        private bool isReplayRequested()
        {
            string userInput;
            bool userChoice;

            Console.WriteLine($"Do you want to play another game? Enter {k_PlayAnotherGameButton} for yes, or any other button for no");
            userInput = Console.ReadLine();
            userChoice = userInput != null && userInput == k_PlayAnotherGameButton;
            
            return userChoice;
        }

        private void printStatistics()
        {
            List<Player> players = m_GameManager.Players;

            Console.WriteLine($"The winner is: {m_GameManager.GetWinner().Name}\n" +
                              $"Game statistics:");
            Console.WriteLine("{0,-15} {1,5}", "Name", "Score");
            Console.WriteLine(new string('-', 22));
            foreach (Player player in players)
            {
                Console.WriteLine("{0,-15} {1,5}", player.Name, player.Score);
            }

            Console.WriteLine();
        }
    }
}