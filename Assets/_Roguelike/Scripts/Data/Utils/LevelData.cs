using System;

namespace Roguelike
{
    [Serializable]
    public class LevelData
    {
        public LevelWaveData[] waves;
        public float rewardMultiplicator;        
    }
}
