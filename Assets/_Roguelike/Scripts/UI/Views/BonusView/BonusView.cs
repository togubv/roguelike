using System;
using UnityEngine;

namespace Roguelike
{
    public class BonusView : IView
    {
        public event Action<int> EventBonusClicked;

        public GameObject bonusesContainer;
        public BonusContainer[] bonusContainers;

        #region Event Handlers

        private void BonusClickHandler(int bonusIndex)
        {
            for (int i = 0; i < bonusContainers.Length; i++)
            {
                bonusContainers[i].gameObject.SetActive(false);
            }

            bonusesContainer.SetActive(false);

            EventBonusClicked?.Invoke(bonusIndex);
        }

        #endregion Event Handlers

        public void ShowBonusesChoice(BonusData[] bonuses)
        {
            bonusesContainer.SetActive(true);

            for (int i = 0; i < bonuses.Length; i++)
            {
                bonusContainers[i].gameObject.SetActive(true);
                bonusContainers[i].SetInfo(bonuses[i].portrait, bonuses[i].title, bonuses[i].description, bonuses[i].level);
            }
        }

        #region Private Methods

        protected override void AddHandlers()
        {
            for (int i = 0; i < bonusContainers.Length; i++)
            {
                int index = i;

                bonusContainers[i].button.onClick.AddListener(() => BonusClickHandler(index));
            }
        }

        protected override void RemoveHandlers()
        {
            for (int i = 0; i < bonusContainers.Length; i++)
            {
                bonusContainers[i].button.onClick.RemoveAllListeners();
            }
        }

        #endregion Private Methods
    }
}
