using System;
using UnityEngine;

namespace Roguelike
{
    public class Health : MonoBehaviour, IDamagable
    {
        public event Action<int, int> EventUpdateHealthValue;

        protected bool _isAlive;
        protected int _currentHealth;
        protected int _maxHealth;

        public bool IsAlive()
        {
            return _isAlive;
        }

        public virtual bool TakeDamage(int damageValue)
        {
            _currentHealth -= damageValue;

            Game.menu.Get<InGameController>().AddDamageNumber(damageValue.ToString(), transform.position);

            if (_currentHealth <= 0)
            {
                Death();
                return true;
            }

            return false;
        }

        public virtual void Init(int baseHealthValue)
        {
            _maxHealth = baseHealthValue;
            _currentHealth = _maxHealth;
            _isAlive = true;
        }

        protected virtual void Death()
        {
            _currentHealth = 0;

            EventUpdateHealthValue?.Invoke(_currentHealth, _maxHealth);
        }

        protected virtual void IncreaseHealth(int value)
        {
            _currentHealth += value;

            EventUpdateHealthValue?.Invoke(_currentHealth, _maxHealth);
        }

        protected virtual void DecreaseHealth(int value)
        {
            _currentHealth -= value;

            EventUpdateHealthValue?.Invoke(_currentHealth, _maxHealth);
        }

        protected bool IsLethalDamage(int damageValue)
        {
            return _currentHealth - damageValue <= 0;
        }
    }
}
