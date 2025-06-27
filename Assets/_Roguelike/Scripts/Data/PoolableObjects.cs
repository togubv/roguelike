using System;
using UnityEngine;

namespace Roguelike
{
    [CreateAssetMenu(fileName = "PoolablesObjects", menuName = "MakeSo/PoolablesObjects")]
    public class PoolableObjects : ScriptableObject
    {
        public PoolableObjectData[] poolables;

        public PoolableObjectData GetPoolableObjectData(string id)
        {
            for (int i = 0; i < poolables.Length; i++)
            {
                if (poolables[i].objectId == id)
                {
                    return poolables[i];
                }
            }

            throw new NullReferenceException($"Not found PoolableObjectData with id {id} in PoolableObjects");
        }
    }
}
