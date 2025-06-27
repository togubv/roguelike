using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike
{
    public class InGameView : IView
    {
        public Image experienceFillImage;
        public TMP_Text experienceFillText;
        public TMP_Text levelText;
        public SkillContainer skillContainerPrefab;
        public Transform skillsParent;

        private List<SkillContainer> _skillContainers = new List<SkillContainer>();

        public void UpdatePlayerExperienceValue(int value, int maxValue)
        {
            float percent = (float)value / maxValue;
            experienceFillImage.fillAmount = percent;
            experienceFillText.text = $"{value}/{maxValue}";
        }

        public void UpdatePlayerLevel(int level)
        {
            levelText.text = $"Lvl. {level}";
        }

        public void SpawnSkillContainer(Sprite icon, string title, int level)
        {
            var spawnedContainer = Instantiate(skillContainerPrefab, skillsParent);
            spawnedContainer.SetInfo(icon, title, level);
            _skillContainers.Add(spawnedContainer);
        }

        public void ClearSkillContainers()
        {
            for (int i = 0; i < _skillContainers.Count; i++)
            {
                Destroy(_skillContainers[i].gameObject);
            }

            _skillContainers.Clear();
        }
    }
}
