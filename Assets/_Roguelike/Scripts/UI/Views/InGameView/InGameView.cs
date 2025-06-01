using TMPro;
using UnityEngine.UI;

namespace Roguelike
{
    public class InGameView : IView
    {
        public Image experienceFillImage;
        public TMP_Text experienceFillText;
        public TMP_Text levelText;

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
    }
}
