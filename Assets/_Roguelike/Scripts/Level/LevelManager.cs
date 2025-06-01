using System;
using UnityEngine;

namespace Roguelike
{
    public class LevelManager : MonoBehaviour
    {
        public event Action<int, int> EventUpdatePlayerExperience;
        public event Action<int> EventUpdatePlayerLevel;

        private int _playerExperienceValue;
        private int _playerExperienceRequired;
        private int _playerLevel;
        private float _nextLevelExperienceMultiplier;

        public void IncreasePlayerExperience(int value)
        {
            _playerExperienceValue += value;

            if (_playerExperienceValue >= _playerExperienceRequired)
            {
                PlayerLevelUp();
            }

            EventUpdatePlayerExperience?.Invoke(_playerExperienceValue, _playerExperienceRequired);
        }

        private void Start()
        {
            var gameSettings = Settings.Get<GameSettings>();
            _playerExperienceRequired = gameSettings.baseExperienceRequired;
            _nextLevelExperienceMultiplier = gameSettings.nextLevelExperienceMultiplier;
            _playerLevel = 1;

            EventUpdatePlayerExperience?.Invoke(_playerExperienceValue, _playerExperienceRequired);
            EventUpdatePlayerLevel?.Invoke(_playerLevel);
        }

        private void PlayerLevelUp()
        {
            _playerExperienceValue -= _playerExperienceRequired;
            _playerLevel += 1;
            _playerExperienceRequired = _playerExperienceRequired + (int)(_playerExperienceRequired * _nextLevelExperienceMultiplier);

            EventUpdatePlayerLevel?.Invoke(_playerLevel);

            if (_playerExperienceValue >= _playerExperienceRequired)
            {
                PlayerLevelUp();
            }
        }
    }
}
