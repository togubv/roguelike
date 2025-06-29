using System;
using UnityEngine;

namespace Roguelike
{
    [Serializable]
    public class MobData
    {
        public string id;
        public float movespeed;
        public int baseHealth;
        public GameObject prefab;
    }
}
