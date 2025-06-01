using System;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike
{
    [Serializable]
    public class ObjectPoolData
    {
        public string objectId;
        public List<GameObject> go = new List<GameObject>();

        public ObjectPoolData(string objectId, GameObject go)
        {
            this.objectId = objectId;
            this.go.Add(go);
        }
    }
}
