using System;
using System.Collections;
using Anthill.Inject;
using UnityEngine;

namespace Roguelike
{
    public class LevelManager : MonoBehaviour
    {
        public event Action<int, int> EventUpdatePlayerHealth;
        public event Action<int, int> EventUpdatePlayerExperience;
        public event Action<int> EventUpdatePlayerLevel;

        public HealthPlayer characterPrefab;

        [Inject] private Game _game;
        [Inject] private MobFactory _mobFactory;
        [Inject] private MobsManager _mobsManager;
        [Inject] private PlayerStatsManager _playerStatsManager;

        private int _playerExperienceValue;
        private int _playerExperienceRequired;
        private int _playerLevel;
        private float _nextLevelExperienceMultiplier;

        public void StartLevel(int index)
        {
            var levelData = Settings.Get<LevelsPool>().GetLevelData(index);
            InitPlayer();
            StartCoroutine(PlayingLevel(levelData));
        }

        public void IncreasePlayerExperience(int value)
        {
            _playerExperienceValue += value;

            if (_playerExperienceValue >= _playerExperienceRequired)
            {
                PlayerLevelUp();
            }

            EventUpdatePlayerExperience?.Invoke(_playerExperienceValue, _playerExperienceRequired);
        }

        public void UpdatePlayerHealth(int value, int maxValue)
        {
            EventUpdatePlayerHealth?.Invoke(value, maxValue);
        }

        private void Awake()
        {
            this.InjectMono();
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

        private void InitPlayer()
        {
            var spawnedPlayer = Instantiate(characterPrefab, _playerStatsManager.transform);
            spawnedPlayer.Init(_playerStatsManager.baseHealthValue);
            _game.cinemachineCamera.Follow = spawnedPlayer.transform;
            _playerStatsManager.InitCharacter(spawnedPlayer);
            _mobsManager.SetPlayerTransform(spawnedPlayer.transform);
        }

        private IEnumerator PlayingLevel(LevelData data)
        {
            for (int i = 0; i < data.waves.Length; i++)
            {
                yield return new WaitForSeconds(data.waves[i].delay);

                StartCoroutine(PlayingWave(data.waves[i]));
            }
        }

        private IEnumerator PlayingWave(LevelWaveData wave)
        {
            for (int i = 0; i < wave.amount; i++)
            {
                float period = wave.GetMobSpawnPeriod();

                yield return new WaitForSeconds(period);

                _mobFactory.SpawnMob(wave.mobId);
            }
        }
    }
}
