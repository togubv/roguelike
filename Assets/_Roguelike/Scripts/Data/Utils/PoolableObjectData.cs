using System;
using UnityEngine;

namespace Roguelike
{
    [Serializable]
    public class PoolableObjectData
    {
        public string objectId;
        public GameObject prefab;
    }
}
