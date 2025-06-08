using System.Collections;
using UnityEngine;

namespace Roguelike
{
    public class SkillController : MonoBehaviour
    {
        public Animation skillAnimation;
        public float cooldown = 1f;

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
                CastSkill();

                yield return new WaitForSeconds(cooldown);
            }
        }
    }
}
