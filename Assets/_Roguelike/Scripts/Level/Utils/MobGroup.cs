using System.Collections.Generic;
using UnityEngine;

namespace Roguelike
{
    public class MobGroup
    {
        public string mobId;
        public string mobGroupId;
        public float movespeed;
        public List<Rigidbody2D> mobs = new List<Rigidbody2D>();

        public MobGroup(string mobId, string mobGroupId, float movespeed, List<Rigidbody2D> mobs)
        {
            this.mobId = mobId;
            this.mobGroupId = mobGroupId;
            this.movespeed = movespeed;
            this.mobs = mobs;
        }
    }
}
