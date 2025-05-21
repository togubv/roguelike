using System;
using UnityEngine;

namespace Roguelike
{
    public class Health : MonoBehaviour, IDamagable
    {
        public event Action<int, int> EventUpdateHealthValue;

        public int baseHealthValue = 1;

        protected bool _isAlive;
        protected int _currentHealth;
        protected int _maxHealth;

        private void Awake()
        {
            _maxHealth = baseHealthValue;
            _currentHealth = _maxHealth;
            _isAlive = true;
        }

        public bool IsAlive()
        {
            return _isAlive;
        }

        public virtual bool TakeDamage(int damageValue)
        {
            return false;
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
