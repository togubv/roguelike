using TMPro;
using UnityEngine;

namespace Roguelike
{
    public class DamageNumberData
    {
        public string damageNumber;
        public Vector2 worldPoint;
        public TMP_Text damageNumberText;
        public float lifetime;
        public float lifetimer;

        public DamageNumberData(string damageNumber, Vector2 worldPoint, TMP_Text damageNumberText)
        {
            this.damageNumber = damageNumber;
            this.worldPoint = worldPoint;
            this.damageNumberText = damageNumberText;
        }
    }
}
