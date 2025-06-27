using Anthill.Inject;
using UnityEngine;

namespace Roguelike
{
    public class MainMenuController : IController
    {
        [Inject] private Game _game;
        private MainMenuView _view;

        public void Create()
        {
            AntInject.Inject(this);
            _view = ObjectFactory.Create<MainMenuView>("UI/MainMenuView/MainMenuView", _game.canvas.transform);
            _view.Hide();
        }

        public void Init()
        {
            _view.Init();
        }

        public void Show()
        {
            _view.Show();
            AddHandlers();
            Time.timeScale = 0f;
        }

        public void Hide()
        {
            _view.Hide();
            RemoveHandlers();
            Time.timeScale = 1f;
        }

        #region Event Handlers

        #endregion Event Handlers

        private void StartHandler()
        {
            Game.menu.Get<InGameController>().Show();
            Game.menu.Get<BonusController>().Show();

            Hide();
        }

        #region Private Methods

        private void AddHandlers()
        {
            _view.EventStartGame += StartHandler;
        }

        private void RemoveHandlers()
        {
            _view.EventStartGame -= StartHandler;
        }

        #endregion Private Methods
    }
}
