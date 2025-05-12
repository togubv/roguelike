using UnityEngine;

namespace Roguelike
{
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