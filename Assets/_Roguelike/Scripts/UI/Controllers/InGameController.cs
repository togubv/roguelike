using Anthill.Inject;

namespace Roguelike
{
    public class InGameController : IController
    {
        [Inject] private Game _game;
        [Inject] private LevelManager _levelManager;
        [Inject] private BonusManager _bonusManager;
        private InGameView _view;
        private BonusesPool _bonusesPool;

        public void Create()
        {
            AntInject.Inject(this);
            _view = ObjectFactory.Create<InGameView>("UI/InGameView/InGameView", _game.canvas.transform);
            _view.Hide();
        }

        public void Init()
        {
            _bonusesPool = Settings.Get<BonusesPool>();
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

        private void UpdateSkillsHandler()
        {
            _view.ClearSkillContainers();
            var bonuses = _bonusManager.activeBonuses;

            for (int i = 0; i < bonuses.Count; i++)
            {
                var bonusData = _bonusesPool.GetBonusData(bonuses[i].bonusId);
                _view.SpawnSkillContainer(bonusData.portrait, bonusData.title, bonuses[i].bonusLevel);
            }
        }

        #region Private Methods

        private void AddHandlers()
        {
            _levelManager.EventUpdatePlayerExperience += UpdatePlayerExperienceHandler;
            _levelManager.EventUpdatePlayerLevel += UpdatePlayerLevelHandler;
            _bonusManager.EventUpdateSkills += UpdateSkillsHandler;
        }

        private void RemoveHandlers()
        {
            _levelManager.EventUpdatePlayerExperience -= UpdatePlayerExperienceHandler;
            _levelManager.EventUpdatePlayerLevel -= UpdatePlayerLevelHandler;
            _bonusManager.EventUpdateSkills -= UpdateSkillsHandler;
        }

        #endregion Private Methods
    }
}
