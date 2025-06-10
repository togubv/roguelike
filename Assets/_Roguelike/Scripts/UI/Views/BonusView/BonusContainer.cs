using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike
{
    public class BonusContainer : MonoBehaviour
    {
        public Image portraitImage;
        public TMP_Text titleText;
        public TMP_Text descriptionText;
        public Button button;

        public void SetInfo(Sprite sprite, string title, string description, int level = 1)
        {
            portraitImage.sprite = sprite;
            titleText.text = $"{title} ({level})";
            descriptionText.text = description;
        }
    }
}
