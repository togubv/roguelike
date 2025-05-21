using System.Collections.Generic;
using UnityEngine;

namespace Roguelike
{
    public class MobGroup
    {
        public string mobId;
        public string mobGroupId;
        public float movespeed;
        public List<Transform> mobs = new List<Transform>();

        public MobGroup(string mobId, string mobGroupId, float movespeed, List<Transform> mobs)
        {
            this.mobId = mobId;
            this.mobGroupId = mobGroupId;
            this.movespeed = movespeed;
            this.mobs = mobs;
        }
    }
}
