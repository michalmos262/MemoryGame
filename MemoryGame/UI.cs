using System;
using System.Linq;

namespace MemoryGame
{
    public class UI
    {
        public const int k_ErrorIndicator = -1;
        public const char k_quitButton = 'Q';
        public const string k_PossibleCardIcons = "ABCDEFGHIJKLMNOPRSTUVWXYZ";
        public const int k_RevealTimeInMiliseconds = 2000;

        public static void ClearScreen()
        {
            Ex02.ConsoleUtils.Screen.Clear();
        }

        public static void ShowGameBoard(GameBoard board)
        {
            printColumnLabels(board);
            Console.WriteLine("  " + new string('=', (board.NumOfColumns * 4) + 1));
            printGameBoard(board);
        }

        private static void printColumnLabels(GameBoard board)
        {
            Console.Write("   ");
            for (int col = 0; col < board.NumOfColumns; col++)
            {
                char columnLabel = (char)('A' + col);
                Console.Write($" {columnLabel}  ");
            }

            Console.WriteLine();
        }

        private static void printGameBoard(GameBoard board)
        {
            Card currentCard;
        
            for (int row = 0; row < board.NumOfRows; row++)
            {
                Console.Write($"{row + 1} | ");
                for (int col = 0; col < board.NumOfColumns; col++)
                {
                    Console.Write(" ");
                    GameBoard.Position position = new GameBoard.Position(row, col);
                    currentCard = board.GetCardInPosition(position);
                    if (currentCard.IsRevealed)
                    {
                        char cardIconToPrint = (char)('A' + currentCard.Number);
                        Console.Write(cardIconToPrint);
                    }

                    Console.Write(" | ");
                }

                Console.WriteLine();
                Console.WriteLine("  " + new string('=', (board.NumOfColumns * 4) + 1));
            }
        }

        private void sleep()
        {
            System.Threading.Thread.Sleep(k_RevealTimeInMiliseconds);
        }

        public static string GetPlayerName()
        {
            Console.WriteLine("Please enter your name:");
            string name = Console.ReadLine();

            return name;
        }

        public static eGameModes GetGameMode()
        {
            int optionNumberFromUser;
            eGameModes gameMode = eGameModes.ErrorMode;

            Console.WriteLine($"Who do you want to play with? Please insert the right option number:\n" +
                              $"({(int)eGameModes.HumanVsComputer}) Computer\n" +
                              $"({(int)eGameModes.HumanVsHuman}) Another player");
            string userInput = Console.ReadLine();
            bool isOptionNumber = int.TryParse(userInput, out optionNumberFromUser);
            if (isOptionNumber)
            {
                if (optionNumberFromUser == (int)eGameModes.HumanVsComputer)
                {
                    gameMode = eGameModes.HumanVsComputer;
                }
                else if (optionNumberFromUser == (int)eGameModes.HumanVsHuman)
                {
                    gameMode = eGameModes.HumanVsHuman;
                }
            }

            return gameMode;
        }

        private static int getNumberFromUserAndCheckValidity()
        {
            string userInput = Console.ReadLine();
            int numberFromUser;
            bool isNumber = int.TryParse(userInput, out numberFromUser);
            if (!isNumber)
            {
                numberFromUser = k_ErrorIndicator;
            }

            return numberFromUser;
        }

        public static int GetBoardNumOfRows()
        {
            Console.WriteLine("Enter the number of the board rows:");
            int numberFromUser = getNumberFromUserAndCheckValidity();

            return numberFromUser;
        }

        public static int GetBoardNumOfColumns()
        {
            Console.WriteLine("Enter the number of the board columns:");
            int numberFromUser = getNumberFromUserAndCheckValidity();

            return numberFromUser;
        }

        private static bool isValidPositionInput(string userInput, GameBoard board)
        {
            bool isRightLength = userInput.Length == 2;
            bool isFirstLetterCharacter = userInput[0] >= 'A' && userInput[0] <= (char)('A' + board.MaxNumOfColumns);
            bool isSecondLetterDigit = userInput[0] >= '0' && userInput[0] <= (char)('0' + board.MaxNumOfRows);

            return isRightLength && isFirstLetterCharacter && isSecondLetterDigit;
        }

        public static void PrintPositionNotInBoardRange(GameBoard.Position position)
        {
            Console.WriteLine($"Position {position} is not in the board range!");
        }

        public static void PrintPositionNotInsertedCorrectly(GameBoard.Position position)
        {
            Console.WriteLine($"Position {position} was not inserted correctly!" +
                              $"A correct position can be for example: E3 (mean column E, row 3)");
        }

        public static void PrintCardAlreadyRevealed(GameBoard.Position position)
        {
            Console.WriteLine($"The card in position {position} is already revealed." +
                              $"You need to choose a hidden card position");
        }

        private string getPositionAsText(GameBoard.Position position)
        {
            char row = (char)(position.RowIndex + 1);
            char column = (char)('A' + position.ColumnIndex);

            return $"{column}{row}";
        }

        public void printComputerChoose(GameBoard.Position computerChosenPosition)
        {
            Console.Write($"Computer chose position {getPositionAsText(computerChosenPosition)}");
        }

        public static string GetPositionFromUser(GameBoard board)
        {
            Console.WriteLine("Enter a board position to reveal a card (for example position E3 means column E, row 3):");
            string userInput = Console.ReadLine();
            //TODO: if (!isValidPositionInput(userInput, board) && isPositionInBoardRange() && !isCardRevealed())
            return userInput;
        }
    }
}
