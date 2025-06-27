using UnityEngine;

namespace Roguelike
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "MakeSo/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        public float playerBaseMovespeed = 1f;
        public int baseExperienceRequired = 1;
        public float nextLevelExperienceMultiplier;

    }
}
