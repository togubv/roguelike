using System;

namespace Roguelike
{
    [Serializable]
    public class LevelWaveData
    {
        public float delay;
        public string mobId;
        public int amount;
        public float duration;

        public float GetMobSpawnPeriod()
        {
            return duration / amount;
        }
    }
}