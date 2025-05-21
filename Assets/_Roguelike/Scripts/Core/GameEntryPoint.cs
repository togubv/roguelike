using UnityEngine;

namespace Roguelike
{
    [DefaultExecutionOrder(-10000)]
    public class GameEntryPoint : MonoBehaviour
    {
        public Game game;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            game.Init();
        }
    }
}