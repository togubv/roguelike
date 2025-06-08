using System;
using UnityEngine;

namespace Roguelike
{
    [CreateAssetMenu(fileName = "BonusesPool", menuName = "MakeSo/BonusesPool")]
    public class BonusesPool : ScriptableObject
    {
        public BonusData[] bonuses;

        public BonusData GetBonusData(string id)
        {
            for (int i = 0; i < bonuses.Length; i++)
            {
                if (bonuses[i].id == id)
                {
                    return bonuses[i];
                }
            }

            throw new NullReferenceException($"Not found BonusData with id {id} in BonusesPool");
        }
    }
}
