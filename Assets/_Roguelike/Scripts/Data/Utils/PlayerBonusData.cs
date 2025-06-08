using System;
using UnityEngine;

namespace Roguelike
{
    [Serializable]
    public class PlayerBonusData
    {
        public string bonusId;
        public int bonusLevel;
        public SkillController skill;

        public PlayerBonusData(string bonusId, int bonusLevel, GameObject skill)
        {
            this.bonusId = bonusId;
            this.bonusLevel = bonusLevel;
            this.skill = skill.GetComponent<SkillController>();
        }
    }
}
