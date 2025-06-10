using UnityEngine;

namespace Roguelike
{
    public class SkillDamageArea : SkillController
    {
        public Transform damagePoint;
        public float radius;
        public LayerMask layerMask;

        protected override void CastSkill()
        {
            base.CastSkill();

            var targets = Physics2D.OverlapCircleAll(damagePoint.position, radius, layerMask);

            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i].TryGetComponent<IDamagable>(out IDamagable iDamagable))
                {
                    iDamagable.TakeDamage(_damageValue);
                }
            }
        }
    }
}
