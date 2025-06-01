using UnityEngine;

namespace Roguelike
{
    public class LevelMobData
    {
        public Transform mobTrans;
        public float movespeed;

        public LevelMobData(Transform mobTrans, float movespeed)
        {
            this.mobTrans = mobTrans;
            this.movespeed = movespeed;
        }
    }
}
