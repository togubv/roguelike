using System;
using UnityEngine;

namespace Roguelike
{
    [CreateAssetMenu(fileName = "MobsPool", menuName = "MakeSo/MobsPool")]
    public class MobsPool : ScriptableObject
    {
        public MobData[] mobs;

        public MobData GetMobData(string mobId)
        {
            for (int i = 0; i < mobs.Length; i++)
            {
                if (mobs[i].id == mobId)
                {
                    return mobs[i];
                }
            }

            throw new NullReferenceException($"Not found MobData with id {mobId} in MobsPool");
        }
    }
}
