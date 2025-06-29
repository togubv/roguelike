using System;
using UnityEngine.UI;

namespace Roguelike
{
    public class MainMenuView : IView
    {
        public event Action EventStartGame;
        public event Action<int> EventLevelClicked;

        public Button startButton;
        public LevelContainer[] levelContainers;

        private void StartHandler()
        {
            EventStartGame?.Invoke();
        }

        #region Event Handlers

        private void LevelContainerButtonHandler(int index)
        {
            EventLevelClicked?.Invoke(index);
        }

        #endregion Event Handlers

        protected override void AddHandlers()
        {
            startButton.onClick.AddListener(StartHandler);

            for (int i = 0; i < levelContainers.Length; i++)
            {
                int index = i;
                levelContainers[i].button.onClick.AddListener(() => LevelContainerButtonHandler(index));
            }
        }

        protected override void RemoveHandlers()
        {
            startButton.onClick.RemoveListener(StartHandler);

            for (int i = 0; i < levelContainers.Length; i++)
            {
                levelContainers[i].button.onClick.RemoveAllListeners();
            }
        }
    }
}
