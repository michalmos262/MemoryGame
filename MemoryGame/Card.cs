using System;

namespace MemoryGame
{
    public struct Card
    {
        private int m_Number;
        private bool m_IsRevealed;
        public static int k_InvalidCardIndicator = -1;

        public Card(int i_Number)
        {
            m_Number = i_Number;
            m_IsRevealed = false;
        }

        public int Number
        {
            get
            {
                return m_Number;
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