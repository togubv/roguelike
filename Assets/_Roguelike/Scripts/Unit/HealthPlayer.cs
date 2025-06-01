using System.Collections;
using UnityEngine;

namespace Roguelike
{
    public class HealthPlayer : Health
    {
        private int _mobsInRange;
        private bool _isTakingDamage;

        private Coroutine _takingDamage;

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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Mob"))
            {
                UpdateMobsAmount(1);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Mob"))
            {
                UpdateMobsAmount(-1);
            }
        }

        private void UpdateMobsAmount(int amount)
        {
            _mobsInRange += amount;

            if (_isTakingDamage)
            {
                if (_mobsInRange < 1)
                {
                    _isTakingDamage = false;
                    StopCoroutine(_takingDamage);
                    _takingDamage = null;
                }
            }
            else
            {
                if (_mobsInRange > 1)
                {
                    _isTakingDamage = true;
                    _takingDamage = StartCoroutine(TakingDamage());
                }
            }
        }

        private IEnumerator TakingDamage()
        {
            while (_isTakingDamage)
            {
                TakeDamage(_mobsInRange);

                yield return new WaitForSeconds(1f);                
            }
        }
    }
}
