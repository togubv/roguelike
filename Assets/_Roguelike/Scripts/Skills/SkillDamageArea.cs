using UnityEngine;

namespace Roguelike
{
    public class SkillDamageArea : SkillController
    {
        public float[] radiuses;
        public Transform damagePoint;
        public LayerMask layerMask;

        protected override void CastSkill()
        {
            base.CastSkill();

            var targets = Physics2D.OverlapCircleAll(damagePoint.position, radiuses[GetLevelIndex(radiuses.Length)], layerMask);

            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i].TryGetComponent<IDamagable>(out IDamagable iDamagable))
                {
                    iDamagable.TakeDamage(damageValues[GetLevelIndex(damageValues.Length)]);
                }
            }
        }
    }
}
