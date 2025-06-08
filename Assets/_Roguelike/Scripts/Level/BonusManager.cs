using System.Collections.Generic;
using Anthill.Inject;
using UnityEngine;

namespace Roguelike
{
    public class BonusManager : MonoBehaviour
    {
        public Transform skillsParent;

        [Inject] private LevelManager _levelManager;
        private BonusController _bonusController;
        private BonusesPool _bonusesPool;

        private List<string> _availableBonuses = new List<string>();
        private List<BonusData> _bonusesDraft = new List<BonusData>();

        private List<PlayerBonusData> _activeBonuses = new List<PlayerBonusData>();

        public void ChoiceBonus(int bonusIndex)
        {
            var playerBonus = GetPlayerBonusData(_bonusesDraft[bonusIndex].id);
            int bonusLevel = 0;

            if (playerBonus == null)
            {
                bonusLevel = 1;
                var spawnedSkill = Instantiate(_bonusesDraft[bonusIndex].skillPrefab, skillsParent);
                _activeBonuses.Add(new PlayerBonusData(_bonusesDraft[bonusIndex].id, bonusLevel, spawnedSkill));
            }
            else
            {
                bonusLevel = playerBonus.bonusLevel + 1;
                playerBonus.bonusLevel = bonusLevel;
                playerBonus.skill.IncreaseLevel();
            }

            var bonusData = _bonusesPool.GetBonusData(_bonusesDraft[bonusIndex].id);

            if (bonusData.maxLevel == bonusLevel)
            {
                RemoveBonusFromAvailable(_bonusesDraft[bonusIndex].id);
            }
            
            _bonusesDraft.Clear();
            Time.timeScale = 1f;
        }

        private void Awake()
        {
            this.InjectMono();

            _bonusController = Game.menu.Get<BonusController>();
            _bonusesPool = Settings.Get<BonusesPool>();

            InitAvailableBonuses();
        }

        private void OnEnable()
        {
            _levelManager.EventUpdatePlayerLevel += UpdatePlayerLevelHandler;
        }

        private void OnDisable()
        {
            _levelManager.EventUpdatePlayerLevel -= UpdatePlayerLevelHandler;
        }

        private void UpdatePlayerLevelHandler(int playerLevel)
        {
            if (playerLevel < 2)
            {
                return;
            }

            ShowBonusesChoice();
        }

        private void InitAvailableBonuses()
        {
            var bonusesPool = Settings.Get<BonusesPool>().bonuses;

            for (int i = 0; i < bonusesPool.Length; i++)
            {
                _availableBonuses.Add(bonusesPool[i].id);
            }
        }

        private void ShowBonusesChoice()
        {
            if (_availableBonuses.Count < 1)
            {
                return;
            }

            List<BonusData> availableBonuses = new List<BonusData>();
            
            for (int i = 0; i < _availableBonuses.Count; i++)
            {
                var bonusData = Settings.Get<BonusesPool>().GetBonusData(_availableBonuses[i]);
                availableBonuses.Add(bonusData);
            }

            for (int i = 0; i < 3; i++)
            {
                if (availableBonuses.Count < 1)
                {
                    break;
                }

                int randomIndex = Random.Range(0, availableBonuses.Count);
                _bonusesDraft.Add(availableBonuses[randomIndex]);
                availableBonuses.RemoveAt(randomIndex);
            }

            _bonusController.ShowBonusesChoice(_bonusesDraft.ToArray());
            Time.timeScale = 0f;
        }

        private void RemoveBonusFromAvailable(string bonusId)
        {
            for (int i = 0; i < _availableBonuses.Count; i++)
            {
                if (_availableBonuses[i] == bonusId)
                {
                    _availableBonuses.RemoveAt(i);
                    return;
                }
            }
        }

        private PlayerBonusData GetPlayerBonusData(string bonusId)
        {
            for (int i = 0; i < _activeBonuses.Count; i++)
            {
                if (_activeBonuses[i].bonusId == bonusId)
                {
                    return _activeBonuses[i];
                }
            }

            return null;
        }
    }
}
