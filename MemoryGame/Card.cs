using System;

namespace MemoryGame
{
    public struct Card
    {
        private int m_Number;
        private bool m_IsRevealed;
        private const int k_NotRevealedNumber = 0;

        public Card(int i_Number)
        {
            m_Number = i_Number;
            m_IsRevealed = false;
        }

        public int Number
        {
            get
            {
                int number;

                if (m_IsRevealed)
                {
                    number = m_Number;
                }
                else
                {
                    number = k_NotRevealedNumber;
                }

                return number;
            }
        }

        internal bool IsRevealed
        {
            get
            {
                return m_IsRevealed;
            }
            set
            {
                m_IsRevealed = value;
            }
        }
    }
}