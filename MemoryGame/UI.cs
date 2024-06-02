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

        public UI(GameManager i_Game)
        {
            m_Game = i_Game;
            assignRandomLettersToCards();
        }

        private void assignRandomLettersToCards()
        {
            string pool = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random rand = new Random();
            int numOfPairsInGame;
            char[] charArray;

            numOfPairsInGame = m_Game.GetNumOfPairsInGame();
            m_CardsLetters = new char[numOfPairsInGame];
            charArray = pool.ToCharArray();
            charArray = charArray.OrderBy(c => rand.Next()).ToArray();
            m_CardsLetters = charArray.Take(numOfPairsInGame).ToArray();
        }

        public void ClearScreen()
        {
            Ex02.ConsoleUtils.Screen.Clear();
        }
    }
}
