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

        public void IncreaseLevel()
        {
            _level += 1;
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
            if (_level < length)
            {
                return _level - 1;
            }
            else
            {
                return length - 1;
            }
        }
    }
}
