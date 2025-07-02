using UnityEngine;

namespace Roguelike
{
    public class SkillDamageArea : SkillController
    {
        public float[] radiuses;
        public Transform damagePoint;
        public LayerMask layerMask;

        private float _radiusMultiplicator = 0f;

        protected override void CastSkill()
        {
            base.CastSkill();

            var targets = Physics2D.OverlapCircleAll(damagePoint.position, GetRadiusValue(), layerMask);

            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i].TryGetComponent<IDamagable>(out IDamagable iDamagable))
                {
                    iDamagable.TakeDamage(GetDamageValue());
                }
            }
        }

        protected float GetRadiusValue()
        {
            int index = GetLevelIndex(_level);
            return radiuses[index] + (radiuses[index] * _radiusMultiplicator);
        }
    }
}
