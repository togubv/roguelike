namespace Roguelike
{
    public class StatData
    {
        public string source;
        public StatType type;
        public float fixedValue;
        public float percentValue;

        public StatData(string source, StatType type, float fixedValue, float percentValue = 1f)
        {
            this.source = source;
            this.type = type;
            this.fixedValue = fixedValue;
            this.percentValue = percentValue;
        }
    }
}
