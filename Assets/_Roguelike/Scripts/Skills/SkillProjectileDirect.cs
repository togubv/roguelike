namespace Roguelike
{
    public class SkillProjectileDirect : SkillController
    {
        public SpawnProjectile spawnProjectile;

        protected override void CastSkill()
        {
            spawnProjectile.Spawn(damageValues[GetLevelIndex(damageValues.Length)]);
        }
    }
}
