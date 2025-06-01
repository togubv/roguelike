using System.Collections;
using UnityEngine;

namespace Roguelike
{
    public class SkillController : MonoBehaviour
    {
        public Animation skillAnimation;
        public float cooldown = 1f;

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
