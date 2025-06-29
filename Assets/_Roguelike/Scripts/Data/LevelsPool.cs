using System;
using UnityEngine;

namespace Roguelike
{
    [CreateAssetMenu(fileName = "LevelsPool", menuName = "MakeSo/LevelsPool")]
    public class LevelsPool : ScriptableObject
    {
        public LevelData[] levels;

        public LevelData GetLevelData(int index)
        {
            for (int i = 0; i < levels.Length; i++)
            {
                if (i == index)
                {
                    return levels[i];
                }
            }

            throw new NullReferenceException($"Not found LevelData with index {index} in LevelsPool");
        }
    }
}
