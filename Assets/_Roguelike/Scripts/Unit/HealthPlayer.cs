using UnityEngine;

namespace Roguelike
{
    public class HealthPlayer : Health
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Mob"))
            {
                TakeDamage(1);
            }
        }

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
