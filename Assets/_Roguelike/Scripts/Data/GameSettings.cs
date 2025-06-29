using UnityEngine;

namespace Roguelike
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "MakeSo/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        public float playerBaseMovespeed = 1f;
        public int baseExperienceRequired = 1;
        public float nextLevelExperienceMultiplier;
        public float respawnRadius;
        public float respawnMinRadius;
        public float respawnMaxRadius;
        public bool showDamageNumber = true;
        public float damageNumberLifetime = 1f;
    }
}
