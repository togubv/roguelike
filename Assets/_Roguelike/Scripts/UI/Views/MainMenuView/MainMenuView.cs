using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike
{
    public class MainMenuView : IView
    {
        public event Action EventStartGame;
        public event Action<int> EventLevelClicked;

        public Button startButton;
        public LevelContainer levelContainerPrefab;
        public Transform levelContainersParent;

        private List<LevelContainer> _levelContainers = new List<LevelContainer>();

        public void InitLevels(LevelData[] levels)
        {
            for (int i =  0; i < levels.Length; i++)
            {
                var spawnedLevelContainer = Instantiate(levelContainerPrefab, levelContainersParent);
                spawnedLevelContainer.titleText.text = levels[i].levelTitle;
                int index = i;
                spawnedLevelContainer.button.onClick.AddListener(() => LevelContainerButtonHandler(index));
            }
        }

        public void ClearLevels()
        {
            for (int i = 0; i < _levelContainers.Count; i++)
            {
                _levelContainers[i].button.onClick.RemoveAllListeners();
                Destroy(_levelContainers[i].gameObject);
            }

            _levelContainers.Clear();
        }

        #region Event Handlers

        private void StartHandler()
        {
            EventStartGame?.Invoke();
        }

        private void LevelContainerButtonHandler(int index)
        {
            EventLevelClicked?.Invoke(index);
        }

        #endregion Event Handlers

        protected override void AddHandlers()
        {
            startButton.onClick.AddListener(StartHandler);
        }

        protected override void RemoveHandlers()
        {
            startButton.onClick.RemoveListener(StartHandler);
        }
    }
}
