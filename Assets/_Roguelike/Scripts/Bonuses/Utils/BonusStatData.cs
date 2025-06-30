using System;

namespace Roguelike
{
    [Serializable]
    public class BonusStatData
    {
        public StatType type;
        public float fixedValuePerLevel;
        public float percentPerLevel;

        public float GetPercentValue(int level)
        {
            return percentPerLevel * level;
        }

        public float GetFixedValue(int level)
        {
            return fixedValuePerLevel * level;
        }
    }
}
