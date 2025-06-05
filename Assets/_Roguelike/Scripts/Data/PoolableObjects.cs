using UnityEngine;

namespace Roguelike
{
    [CreateAssetMenu(fileName = "PoolablesObjects", menuName = "MakeSo/PoolablesObjects")]
    public class PoolableObjects : ScriptableObject
    {
        public PoolableObjectData[] collectables;

        public PoolableObjectData GetPoolableObjectData(string id)
        {
            for (int i = 0; i < collectables.Length; i++)
            {
                if (collectables[i].objectId == id)
                {
                    return collectables[i];
                }
            }

            return null;
        }
    }
}
