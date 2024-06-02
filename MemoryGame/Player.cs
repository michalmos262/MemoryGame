namespace MemoryGame
{
    public class Player
    {
        private string m_Name;
        private const string k_ComputerName = "Computer";
        private bool m_isHuman;
        private int m_Score;

        public Player()
        {
            m_Name = k_ComputerName;
            m_isHuman = false;
            m_Score = 0;
        }

        public Player(string i_Name)
        {
            m_Name = i_Name;
            m_isHuman = true;
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
            set
            {
                m_isHuman = value;
            }
        }

        internal string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
            }
        }
    }
}
