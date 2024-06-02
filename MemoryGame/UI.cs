using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame
{
    internal class UI
    {
        private GameManager m_Game;
        private char[] m_CardsLetters;
        private const int k_RevealTimeInMiliseconds = 2000;

        public UI(GameManager i_Game)
        {
            m_Game = i_Game;
            assignRandomLettersToCards();
        }

        internal char[] CardsLetters
        {
            get 
            { 
                return m_CardsLetters; 
            }
        }

        private void assignRandomLettersToCards()
        {
            string pool = "ABCDEFGHIJKLMNOPRSTUVWXYZ";
            Random rand = new Random();
            int numOfPairsInGame;
            char[] charArray;

            numOfPairsInGame = m_Game.GetNumOfPairsInGame();
            m_CardsLetters = new char[numOfPairsInGame + 1];
            charArray = pool.ToCharArray();
            charArray = charArray.OrderBy(c => rand.Next()).ToArray();
            m_CardsLetters = charArray.Take(numOfPairsInGame + 1).ToArray();
            m_CardsLetters[0] = ' ';
        }

        public void ClearScreen()
        {
            Ex02.ConsoleUtils.Screen.Clear();
        }

        public void ShowGameBoard()
        {
            printColumnLabels();
            // Print horizontal line
            Console.WriteLine("  " + new string('=', (m_Game.GameBoard.NumOfColumns * 4) + 1));
            printGameBoard();
        }

        private void printColumnLabels()
        {
            Console.Write("   ");
            for (int col = 0; col < m_Game.GameBoard.NumOfColumns; col++)
            {
                char columnLabel = (char)('A' + col);
                Console.Write($" {columnLabel}  ");
            }
            Console.WriteLine();
        }

        private void printGameBoard()
        {
            Card currentPrintedCard;
            char currentPrintedCardLetter;
        
            for (int row = 0; row < m_Game.GameBoard.NumOfRows; row++)
            {
                Console.Write($"{row + 1} |");
                for (int col = 0; col < m_Game.GameBoard.NumOfColumns; col++)
                {
                    GameBoard.Position position = new GameBoard.Position(row, col);
                    currentPrintedCard = m_Game.GameBoard.GetCard(position);
                    currentPrintedCardLetter = m_CardsLetters[currentPrintedCard.Number];
                    Console.Write(" ");
                    Console.Write(currentPrintedCardLetter);
                    Console.Write(" |");
                }
                Console.WriteLine();
                Console.WriteLine("  " + new string('=', (m_Game.GameBoard.NumOfColumns * 4) + 1));
            }
        }

        private void sleep()
        {
            System.Threading.Thread.Sleep(k_RevealTimeInMiliseconds);
        }
    }
}
