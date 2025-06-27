using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike
{
    public class SkillContainer : MonoBehaviour
    {
        public Image iconImage;
        public TMP_Text titleText;
        public TMP_Text levelText;

        public void SetInfo(Sprite icon, string title, int level)
        {
            iconImage.sprite = icon;
            titleText.text = title;
            levelText.text = $"{level}";
        }
    }
}
