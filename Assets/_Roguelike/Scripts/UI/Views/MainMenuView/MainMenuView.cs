using System;
using UnityEngine.UI;

namespace Roguelike
{
    public class MainMenuView : IView
    {
        public event Action EventStartGame;

        public Button startButton;

        private void StartHandler()
        {
            EventStartGame?.Invoke();
        }

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
