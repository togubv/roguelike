using System;

namespace Roguelike
{
    [Serializable]
    public class LevelData
    {
        public string levelTitle;
        public LevelWaveData[] waves;
        public float rewardMultiplicator;        
    }
}
