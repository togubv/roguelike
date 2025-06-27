using System.Collections;
using UnityEngine;

namespace Roguelike
{
    public class SkillController : MonoBehaviour
    {
        public Animation skillAnimation;
        public float cooldown = 1f;
        public int[] damageValues;

        protected int _level = 1;
        protected int _damageValue;

        public void IncreaseLevel()
        {
            _level += 1;

            if (damageValues.Length >= _level)
            {
                _damageValue = damageValues[_level - 1];
            }
        }

        protected void Start()
        {
            _damageValue = damageValues[0];
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
                yield return new WaitForSeconds(cooldown);

                CastSkill();
            }
        }

        protected int GetLevelIndex(int length)
        {
            if (length < _level)
            {
                return _level;
            }
            else
            {
                return length;
            }
        }
    }
}
