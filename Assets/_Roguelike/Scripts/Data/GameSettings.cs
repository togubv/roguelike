using UnityEngine;

namespace Roguelike
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "MakeSo/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        public int baseExperienceRequired = 1;
        public float nextLevelExperienceMultiplier;

    }
}
