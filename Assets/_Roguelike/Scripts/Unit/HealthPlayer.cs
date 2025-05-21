namespace Roguelike
{
    public class HealthPlayer : Health
    {
        public override bool TakeDamage(int damageValue)
        {
            bool isLethal = IsLethalDamage(damageValue);

            if (isLethal)
            {
                Death();
            }
            else
            {
                DecreaseHealth(damageValue);
            }

            return isLethal;
        }
    }
}
