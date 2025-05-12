using Anthill.Inject;

namespace Roguelike
{
    public class InGameController : IController
    {
        [Inject] private Game _game;
        private InGameView _view;

        public void Create()
        {
            AntInject.Inject(this);
            _view = ObjectFactory.Create<InGameView>("UI/InGameView/InGameView", _game.canvas.transform);
            _view.Init();
        }

        public void Init()
        {

        }
    }
}
