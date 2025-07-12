using Anthill.Inject;
using Unity.Cinemachine;
using UnityEngine;

namespace Roguelike
{
    public class Game : MonoBehaviour
    {
        public Canvas canvas;
        public Settings settings;
        public CinemachineCamera cinemachineCamera;

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

            var mobsManager = FindAnyObjectByType<MobsManager>();
            if (mobsManager != null)
            {
                injectContainer.RegisterSingleton(mobsManager);
            }

            var bonusManager = FindAnyObjectByType<BonusManager>();
            if (bonusManager != null)
            {
                injectContainer.RegisterSingleton(bonusManager);
            }

            var playerStatsManager = FindAnyObjectByType<PlayerStatsManager>();
            if (playerStatsManager != null)
            {
                injectContainer.RegisterSingleton(playerStatsManager);
            }

            menu = new Menu();

            menu.Create<InGameController>();
            menu.Create<BonusController>();
            menu.Create<MainMenuController>();

            menu.Init();

            menu.Get<MainMenuController>().Show();
        }
    }
}
