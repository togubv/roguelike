using System.Collections.Generic;
using Anthill.Inject;

namespace Roguelike
{
    public class BonusController : IController
    {
        [Inject] private Game _game;
        [Inject] private BonusManager _bonusManager;
        private BonusView _view;

        public void Create()
        {
            AntInject.Inject(this);
            _view = ObjectFactory.Create<BonusView>("UI/BonusView/BonusView", _game.canvas.transform);
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

        public void ShowBonusesChoice(BonusData[] bonuses)
        {
            _view.ShowBonusesChoice(bonuses);
        }

        #region Event Handlers

        #endregion Event Handlers

        private void BonusClickHandler(int bonusIndex)
        {
            _bonusManager.ChoiceBonus(bonusIndex);
        }

        #region Private Methods

        private void AddHandlers()
        {
            _view.EventBonusClicked += BonusClickHandler;
        }

        private void RemoveHandlers()
        {
            _view.EventBonusClicked -= BonusClickHandler;
        }

        #endregion Private Methods
    }
}
