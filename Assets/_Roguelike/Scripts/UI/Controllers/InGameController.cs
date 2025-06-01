using Anthill.Inject;

namespace Roguelike
{
    public class InGameController : IController
    {
        [Inject] private Game _game;
        [Inject] private LevelManager _levelManager;
        private InGameView _view;

        public void Create()
        {
            AntInject.Inject(this);
            _view = ObjectFactory.Create<InGameView>("UI/InGameView/InGameView", _game.canvas.transform);
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
        }

        public void Hide()
        {
            _view.Hide();
            RemoveHandlers();
        }

        #region Event Handlers

        #endregion Event Handlers

        private void UpdatePlayerExperienceHandler(int value, int maxValue)
        {
            _view.UpdatePlayerExperienceValue(value, maxValue);
        }

        private void UpdatePlayerLevelHandler(int level)
        {
            _view.UpdatePlayerLevel(level);
        }

        #region Private Methods

        private void AddHandlers()
        {
            _levelManager.EventUpdatePlayerExperience += UpdatePlayerExperienceHandler;
            _levelManager.EventUpdatePlayerLevel += UpdatePlayerLevelHandler;
        }

        private void RemoveHandlers()
        {
            _levelManager.EventUpdatePlayerExperience -= UpdatePlayerExperienceHandler;
            _levelManager.EventUpdatePlayerLevel -= UpdatePlayerLevelHandler;
        }

        #endregion Private Methods
    }
}
