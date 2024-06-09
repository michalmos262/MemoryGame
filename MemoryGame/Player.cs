namespace MemoryGame
{
    public class Player
    {
        private string m_Name;
        private bool m_isHuman;
        private int m_Score;
        private const string k_ComputerName = "Computer";
        private static int k_ComputerPlayersCounter = 0;

        public Player(string i_Name, bool i_IsHuman)
        {
            if (!i_IsHuman)
            {
                k_ComputerPlayersCounter++;
                m_Name = $"{k_ComputerName}_{k_ComputerPlayersCounter}";
            }
            else
            {
                m_Name = i_Name;
            }

            m_isHuman = i_IsHuman;
            m_Score = 0;
        }

        internal int Score
        {
            get
            {
                return m_Score;
            }
            set
            {
                m_Score = value;
            }
        }

        internal bool IsHuman
        {
            get
            {
                return m_isHuman;
            }
        }

        internal string Name
        {
            get
            {
                return m_Name;
            }
        }
    }
}
