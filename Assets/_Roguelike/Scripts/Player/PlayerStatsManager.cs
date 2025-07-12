using System;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike
{
    public class PlayerStatsManager : MonoBehaviour
    {
        public event Action<StatType, float, float> EventUpdatePlayerStat;
        public event Action<HealthPlayer> EventInitializeCharacter;

        public int baseHealthValue = 1;

        public List<StatData> _stats = new List<StatData>();

        public void InitCharacter(HealthPlayer character)
        {
            EventInitializeCharacter?.Invoke(character);
        }

        public void AddBonusStats(BonusData bonusData, int level)
        {
            for (int i = 0; i < bonusData.stats.Length; i++)
            {
                AddStat(bonusData.id, bonusData.stats[i].type, bonusData.stats[i].GetFixedValue(level), bonusData.stats[i].GetPercentValue(level));
            }
        }

        public void RemoveBonusStats(BonusData bonusData, int level)
        {
            for (int i = 0; i < bonusData.stats.Length; i++)
            {
                RemoveStat(bonusData.id, bonusData.stats[i].type);
            }
        }

        public void AddStat(string source, StatType type, float fixedValue, float percentValue = 1)
        {
            if (HasStats(source, type))
            {
                int index = GetStatsIndex(source, type);
                _stats[index].fixedValue = fixedValue;
                _stats[index].percentValue = percentValue;
            }
            else
            {
                _stats.Add(new StatData(source, type, fixedValue, percentValue));
            }

            UpdatePlayerStat(type);
        }

        public void RemoveStat(string source, StatType type)
        {
            if (HasStats(source, type) == false)
            {
                return;
            }

            int index = GetStatsIndex(source, type);
            _stats.RemoveAt(index);

            UpdatePlayerStat(type);
        }

        public float GetAdditionalStatFixedValue(StatType type)
        {
            float value = 0f;

            for (int i = 0; i < _stats.Count; i++)
            {
                if (_stats[i].type == type)
                {
                    value += _stats[i].fixedValue;
                }
            }

            return value;
        }

        public float GetAdditionalStatPercentValue(StatType type)
        {
            float value = 0f;

            for (int i = 0; i < _stats.Count; i++)
            {
                if (_stats[i].type == type)
                {
                    value += _stats[i].percentValue;
                }
            }

            return value;
        }
        

        private void UpdatePlayerStat(StatType type)
        {
            float newFixedValue = GetAdditionalStatFixedValue(type);
            float newPercentValue = GetAdditionalStatPercentValue(type);

            EventUpdatePlayerStat?.Invoke(type, newFixedValue, newPercentValue);
        }

        private bool HasStats(string source, StatType type)
        {
            for (int i = 0; i < _stats.Count; i++)
            {
                if (_stats[i].source == source && _stats[i].type == type)
                {
                    return true;
                }
            }

            return false;
        }

        private int GetStatsIndex(string source, StatType type)
        {
            for (int i = 0; i < _stats.Count; i++)
            {
                if (_stats[i].source == source && _stats[i].type == type)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
