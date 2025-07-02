using System.Collections;
using UnityEngine;

namespace Roguelike
{
    public class SkillController : MonoBehaviour
    {
        public Animation skillAnimation;
        public float[] cooldown;
        public int[] damageValues;

        protected int _level = 1;
        protected float _damageMultiplicator = 0f;
        protected float _cooldownMultiplicator = 0f;

        public void IncreaseLevel()
        {
            _level += 1;
        }

        public void SetDamageMultiplicator(float multiplier)
        {
            _damageMultiplicator = multiplier;
        }

        public void SetCooldownMultiplicator(float multiplier)
        {
            _cooldownMultiplicator = multiplier;
        }

        protected void OnEnable()
        {
            StartCoroutine(CastingSkill());
        }

        protected virtual void CastSkill()
        {
            skillAnimation.Play();
        }

        protected IEnumerator CastingSkill()
        {
            while (true)
            {
                yield return new WaitForSeconds(GetCooldownValue());

                CastSkill();
            }
        }

        protected int GetLevelIndex(int length)
        {
            if (_level < length)
            {
                return _level - 1;
            }
            else
            {
                return length - 1;
            }
        }

        protected int GetDamageValue()
        {
            int index = GetLevelIndex(_level);
            return Mathf.RoundToInt(damageValues[index] + (damageValues[index] * _damageMultiplicator));
        }

        protected float GetCooldownValue()
        {
            int index = GetLevelIndex(_level);
            return cooldown[index] - (cooldown[index] * _cooldownMultiplicator);
        }
    }
}
