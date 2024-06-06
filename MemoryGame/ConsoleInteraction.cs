using System;
using System.Linq;
using MemoryGame;

namespace UI
{
    public class ConsoleInteraction
    {
        private GameManager m_GameManager;
        private const string k_ComputerName = "Computer";
        private const uint k_BoardMinNumOfRows = 4;
        private const uint k_BoardMinNumOfColumns = 4;
        private const uint k_BoardMaxNumOfRows = 6;
        private const uint k_BoardMaxNumOfColumns = 6;
        private const int k_ErrorIndicator = -1;
        private const string k_QuitButton = "Q";
        private bool m_IsQuit = false;
        private const string k_PossibleCardIcons = "ABCDEFGHIJKLMNOPRSTUVWXYZ";
        private const uint k_PossibleNumOfCardsToReveal = 2;
        private const int k_RevealTimeInMilliseconds = 2000;

        public ConsoleInteraction()
        {
            m_GameManager = new GameManager();
        }

        public void ClearScreen()
        {
            Ex02.ConsoleUtils.Screen.Clear();
        }

        public void ShowGameBoard()
        {
            printColumnLabels();
            Console.WriteLine("  " + new string('=', (int)(m_GameManager.Board.NumOfColumns * 4) + 1));
            printGameBoard();
        }

        private void printColumnLabels()
        {
            Console.Write("   ");
            for (int col = 0; col < m_GameManager.Board.NumOfColumns; col++)
            {
                char columnLabel = (char)('A' + col);
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
            System.Threading.Thread.Sleep(k_RevealTimeInMilliseconds);
        }

        public string GetPlayerName(int playerIndex)
        {
            Console.WriteLine($"Please the name of player {playerIndex}:" );
            string name = Console.ReadLine();

            return name;
        }

        public eGameModes GetGameMode()
        {
            Console.WriteLine($"Who do you want to play with? Please insert the right option number:\n" +
                              $"({(int)eGameModes.HumanVsComputer}) Computer\n" +
                              $"({(int)eGameModes.HumanVsHuman}) Another player");

            uint optionNumberFromUser;
            string userInput = Console.ReadLine();
            bool isOptionNumber = uint.TryParse(userInput, out optionNumberFromUser);

            while (!isOptionNumber || (optionNumberFromUser != (int)eGameModes.HumanVsHuman && optionNumberFromUser != (int)eGameModes.HumanVsComputer))
            {
                Console.WriteLine("Wrong input! Please try again:");
                userInput = Console.ReadLine();
                isOptionNumber = uint.TryParse(userInput, out optionNumberFromUser);
            }

            eGameModes gameMode = (eGameModes)optionNumberFromUser;

            return gameMode;
        }

        public uint GetBoardNumOfRows()
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

        public uint GetBoardNumOfColumns()
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
            bool isRightLength = i_UserInput.Length == 2;
            

            if (isRightLength)
            {
                bool isFirstLetterCharacter = char.IsUpper(i_UserInput[0]);
                bool isSecondLetterDigit = char.IsDigit(i_UserInput[1]);//TODO: position P100 should be range error
                if (isFirstLetterCharacter && isSecondLetterDigit)
                {
                    GameBoard.Position position = getTextConvertedToPosition(i_UserInput);
                    adjustPositionToLogics(ref position);
                    ePositionStatus positionStatus = m_GameManager.GetPositionChoiceStatus(position);
                    if (positionStatus == ePositionStatus.RevealedPosition)
                    {
                        PrintPositionAlreadyRevealed(i_UserInput);//TODO: doesn't work, need to understand what happens in the manager, looks like it doesn't update the status of the positions
                    }

                    if (positionStatus == ePositionStatus.OutsideBoard)
                    {
                        PrintPositionNotInBoardRange(i_UserInput);
                    }
                    else
                    {
                        isValidPosition = true;
                    }
                }
                else
                {
                    PrintPositionInputNotInsertedCorrectly(i_UserInput);
                }
            }
            else
            {
                PrintPositionInputNotInsertedCorrectly(i_UserInput);
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

        public void PrintPositionNotInBoardRange(string i_Position)
        {
            Console.WriteLine($"Position {i_Position} is not in the board range!");
        }

        public void PrintPositionAlreadyRevealed(string i_Position)
        {
            Console.WriteLine($"The card in position {i_Position} is already revealed!");
        }

        public void PrintPositionInputNotInsertedCorrectly(string positionInput)
        {
            Console.WriteLine($"Position {positionInput} is not in the right format! " +
                              $"A correct position can be for example: E3 (mean column E, row 3)");
        }

        public void PrintCardAlreadyRevealed(GameBoard.Position position)
        {
            Console.WriteLine($"The card in position {position} is already revealed." +
                              $"You need to choose a hidden card position");
        }

        private string getPositionConvertedToText(GameBoard.Position position)
        {
            char row = (char)(position.RowIndex + 1);
            char column = (char)('A' + position.ColumnIndex);

            return $"{column}{row}";
        }

        private GameBoard.Position getTextConvertedToPosition(string positionText)
        {
            int row = int.Parse(positionText[1].ToString());
            int column = positionText[0] - 'A';
            GameBoard.Position position = new GameBoard.Position(row, column);

            return position;
        }

        public void PrintComputerChoice(GameBoard.Position computerChosenPosition)
        {
            Console.Write($"Computer chose position {getPositionConvertedToText(computerChosenPosition)}");
        }

        private void setQuitByInput(string userInput)
        {
            if (userInput == k_QuitButton)
            {
                m_IsQuit = true;
            }
        }

        public string GetBoardPositionFromUser()
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
            string[] playerNames = new string[m_GameManager.NumOfPlayers];
            bool[] arePlayersHumans = new bool[m_GameManager.NumOfPlayers];

            playerNames[0] = GetPlayerName(1);
            arePlayersHumans[0] = true;
            eGameModes gameMode = GetGameMode();

            for (int i = 1; i < m_GameManager.NumOfPlayers; i++)
            {
                if (gameMode == eGameModes.HumanVsHuman)
                {
                    playerNames[i] = GetPlayerName(i + 1);
                    arePlayersHumans[i] = true;
                }
                else
                {
                    playerNames[i] = $"{k_ComputerName}{i + 1}";
                    arePlayersHumans[i] = false;
                }
            }

            m_GameManager.SetPlayers(playerNames, arePlayersHumans);
        }

        private void setBoard()
        {
            uint numOfBoardRows = GetBoardNumOfRows();
            uint numOfBoardColumns = GetBoardNumOfColumns();

            while (!GameBoard.AreDimensionsValid(numOfBoardRows, numOfBoardColumns))
            {
                Console.WriteLine("Board size should be even! Please try again:");
                numOfBoardRows = GetBoardNumOfRows();
                numOfBoardColumns = GetBoardNumOfColumns();
            }

            m_GameManager.SetBoardDimensions(numOfBoardRows, numOfBoardColumns);
        }

        private GameBoard.Position getBoardPosition(string i_BoardPositionInput)
        {
            GameBoard.Position chosenPosition = getTextConvertedToPosition(i_BoardPositionInput);

            adjustPositionToLogics(ref chosenPosition);
        
            return chosenPosition;
        }

        private void adjustPositionToLogics(ref GameBoard.Position i_BoardPosition)
        {
            i_BoardPosition.RowIndex--;
        }

        private GameBoard.Position[] makeHumanPlayerTurn()
        {
            GameBoard.Position[] chosenBoardPositions = new GameBoard.Position[k_PossibleNumOfCardsToReveal];

            string boardPositionInput = GetBoardPositionFromUser();

            if (!m_IsQuit)
            {
                chosenBoardPositions[0] = getBoardPosition(boardPositionInput);
                m_GameManager.MakeActivePlayerFirstTurn(chosenBoardPositions[0]);
                ClearScreen();
                ShowGameBoard();
                boardPositionInput = GetBoardPositionFromUser();

                if (!m_IsQuit)
                {
                    chosenBoardPositions[1] = getBoardPosition(boardPositionInput);
                }
            }

            return chosenBoardPositions;
        }

        private GameBoard.Position[] makeComputerPlayerTurn()
        {
            GameBoard.Position[] chosenBoardPositions = new GameBoard.Position[k_PossibleNumOfCardsToReveal];

            chosenBoardPositions[0] = m_GameManager.ChooseRandomHiddenCellInBoard();
            m_GameManager.MakeActivePlayerFirstTurn(chosenBoardPositions[0]);
            sleep(1000);
            ClearScreen();
            ShowGameBoard();
            chosenBoardPositions[1] = m_GameManager.ChooseRandomHiddenCellInBoard();

            return chosenBoardPositions;
        }

        private void makePlayerTurn()
        {
            Player currentPlayer = m_GameManager.GetActivePlayer();
            Console.WriteLine($"{currentPlayer.Name}'s turn:");
            GameBoard.Position[] chosenBoardPositions;

            if (currentPlayer.IsHuman)
            {
                chosenBoardPositions = makeHumanPlayerTurn();
            }
            else
            {
                chosenBoardPositions = makeComputerPlayerTurn();
            }

            if (!m_IsQuit)
            {
                bool isPairFound = m_GameManager.MakeActivePlayerSecondTurn(chosenBoardPositions[0], chosenBoardPositions[1]);
                ClearScreen();
                ShowGameBoard();
                if (!isPairFound)
                {
                    sleep();
                    m_GameManager.HidePairInBoardAndPassTurn(chosenBoardPositions[0], chosenBoardPositions[1]);
                    ClearScreen();
                    ShowGameBoard();
                }
            }
        }

        public void Play()
        {
            setPlayers();
            setBoard();
            ShowGameBoard();

            
            while (!m_IsQuit)
            {
                makePlayerTurn();
            }

            if (m_IsQuit)
            {
                Console.WriteLine("BYE");
            }
            //TODO: print winner/tie
        }
    }
}