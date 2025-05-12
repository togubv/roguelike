using Anthill.Inject;
using UnityEngine;

namespace Roguelike
{
    public class Game : MonoBehaviour
    {
        public Canvas canvas;

        public IInjectContainer injectContainer;

        public static Menu menu;

        public void Init()
        {
            var container = new AntInjectContainer();
            AntInject.SetInjector(container);
            injectContainer = container;

            injectContainer.RegisterSingleton(this);

            menu = new Menu();

            menu.Create<InGameController>();

            menu.Init();
        }
    }
}
