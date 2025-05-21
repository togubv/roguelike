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

            return null;
        }
    }
}
