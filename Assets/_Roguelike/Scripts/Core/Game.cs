using Anthill.Inject;
using UnityEngine;

namespace Roguelike
{
    public class Game : MonoBehaviour
    {
        public Canvas canvas;
        public Settings settings;

        public IInjectContainer injectContainer;

        public static Menu menu;

        public void Init()
        {
            var container = new AntInjectContainer();
            AntInject.SetInjector(container);
            injectContainer = container;
            settings.Init();

            injectContainer.RegisterSingleton(this);

            var inputReader = FindAnyObjectByType<InputReader>();
            if (inputReader != null)
            {
                injectContainer.RegisterSingleton(inputReader);
            }

            var levelManager = FindAnyObjectByType<LevelManager>();
            if (levelManager != null)
            {
                injectContainer.RegisterSingleton(levelManager);
            }

            var mobFactory = FindAnyObjectByType<MobFactory>();
            if (mobFactory != null)
            {
                injectContainer.RegisterSingleton(mobFactory);
            }

            var bonusManager = FindAnyObjectByType<BonusManager>();
            if (bonusManager != null)
            {
                injectContainer.RegisterSingleton(bonusManager);
            }

            menu = new Menu();

            menu.Create<InGameController>();
            menu.Create<BonusController>();

            menu.Init();

            menu.Get<InGameController>().Show();
            menu.Get<BonusController>().Show();
        }
    }
}
