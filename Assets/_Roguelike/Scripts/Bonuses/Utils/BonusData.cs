using System;
using UnityEngine;

namespace Roguelike
{
    [Serializable]
    public class BonusData
    {
        public string id;
        public string title;
        public string description;
        public Sprite portrait;
        public BonusStatData[] stats;
        public int maxLevel = 1;
        public GameObject skillPrefab;

        [HideInInspector]
        public int level;
    }
}
