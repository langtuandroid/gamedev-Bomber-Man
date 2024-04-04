using System;

namespace XX
{
    [Serializable]
    public class LevelComplete
    {
        public int mId;

        public bool mCompleted;

        public LevelComplete(int mId, bool mCompleted)
        {
            this.mId = mId;
            this.mCompleted = mCompleted;
        }
    }
}