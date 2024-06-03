namespace MemoryGame
{
    public class Player
    {
        private string m_Name;
        private bool m_isHuman;
        private int m_Score;

        public Player(string i_Name, bool i_IsHuman)
        {
            m_Name = i_Name;
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
