using System.Collections;
using Anthill.Inject;
using UnityEngine;

namespace Roguelike
{
    public class HealthPlayer : Health
    {
        [Inject] LevelManager _levelManager;

        public Transform skillsParent;

        private int _mobsInRange;
        private bool _isTakingDamage;

        private Coroutine _takingDamage;

        public override bool TakeDamage(int damageValue)
        {
            bool isLethal = IsLethalDamage(damageValue);

            if (isLethal)
            {
                UpdatePlayerHealth();
                Death();
            }
            else
            {
                DecreaseHealth(damageValue);
                UpdatePlayerHealth();
            }

            return isLethal;
        }

        private void Awake()
        {
            this.InjectMono();
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

        public override void Init(int baseHealthValue)
        {
            base.Init(baseHealthValue);

            UpdatePlayerHealth();
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
                if (_mobsInRange > 0)
                {
                    _isTakingDamage = true;
                    _takingDamage = StartCoroutine(TakingDamage());
                }
            }
        }

        private void UpdatePlayerHealth()
        {
            _levelManager.UpdatePlayerHealth(_currentHealth, _maxHealth);
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
