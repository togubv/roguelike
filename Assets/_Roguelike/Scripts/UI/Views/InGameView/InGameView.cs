using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Roguelike
{
    public class InGameView : IView
    {
        public SkillContainer skillContainerPrefab;
        public Transform skillsParent;

        [Header("Health")]
        public Image healthFillImage;
        public TMP_Text healthText;

        [Header("Experience")]
        public Image experienceFillImage;
        public TMP_Text experienceFillText;
        public TMP_Text levelText;        

        [Header("Damage Number")]
        public TMP_Text damageNumberContainerPrefab;
        public Transform damageNumbersParent;

        private Camera _cam;
        private float _damageNumberLifetime;

        private List<SkillContainer> _skillContainers = new List<SkillContainer>();
        private List<DamageNumberData> _damageNumbers = new List<DamageNumberData>();
        private List<TMP_Text> _damageNumbersPool = new List<TMP_Text>();

        public override void Init()
        {
            _cam = Camera.main;
            _damageNumberLifetime = Settings.Get<GameSettings>().damageNumberLifetime;
        }

        public void UpdatePlayerHealth(int value, int maxValue)
        {
            float percent = (float)value / maxValue;
            healthFillImage.fillAmount = percent;
            healthText.text = $"{value}/{maxValue}";
        }

        public void UpdatePlayerExperienceValue(int value, int maxValue)
        {
            float percent = (float)value / maxValue;
            experienceFillImage.fillAmount = percent;
            experienceFillText.text = $"{value}/{maxValue}";
        }

        public void UpdatePlayerLevel(int level)
        {
            levelText.text = $"Lvl. {level}";
        }

        public void SpawnSkillContainer(Sprite icon, string title, int level)
        {
            var spawnedContainer = Instantiate(skillContainerPrefab, skillsParent);
            spawnedContainer.SetInfo(icon, title, level);
            _skillContainers.Add(spawnedContainer);
        }

        public void ClearSkillContainers()
        {
            for (int i = 0; i < _skillContainers.Count; i++)
            {
                Destroy(_skillContainers[i].gameObject);
            }

            _skillContainers.Clear();
        }

        public void AddDamageNumber(string damageNumber, Vector2 worldPoint)
        {
            TMP_Text spawnedDamageNumber = null;

            if (_damageNumbersPool.Count < 1)
            {
                spawnedDamageNumber = Instantiate(damageNumberContainerPrefab, damageNumbersParent);
            }
            else
            {
                spawnedDamageNumber = _damageNumbersPool[_damageNumbersPool.Count - 1];
                spawnedDamageNumber.gameObject.SetActive(true);
                _damageNumbersPool.RemoveAt(_damageNumbersPool.Count - 1);
            }

            var damageNumberData = new DamageNumberData(damageNumber, worldPoint, spawnedDamageNumber);
            damageNumberData.lifetime = _damageNumberLifetime;
            damageNumberData.lifetimer = _damageNumberLifetime;
            spawnedDamageNumber.text = damageNumber;
            spawnedDamageNumber.transform.position = worldPoint;
            spawnedDamageNumber.transform.localScale = new Vector2(0.1f, 0.1f);
            spawnedDamageNumber.transform.DOScale(1f, damageNumberData.lifetime * 0.25f).OnComplete(() => spawnedDamageNumber.transform.DOScale(0.1f, damageNumberData.lifetime * 0.75f));
            _damageNumbers.Add(damageNumberData);
        }

        private void Update()
        {
            MoveDamageNumbers();
        }

        private void MoveDamageNumbers()
        {
            if (_damageNumbers.Count < 1)
            {
                return;
            }

            for (int i = _damageNumbers.Count - 1; i >= 0; i--)
            {
                var screenPoint = _cam.WorldToScreenPoint(_damageNumbers[i].worldPoint);
                _damageNumbers[i].damageNumberText.transform.position = screenPoint;
                _damageNumbers[i].lifetimer -= Time.deltaTime;

                if (_damageNumbers[i].lifetimer <= 0)
                {
                    _damageNumbers[i].damageNumberText.gameObject.SetActive(false);
                    _damageNumbersPool.Add(_damageNumbers[i].damageNumberText);
                    _damageNumbers.RemoveAt(i);
                }
            }
        }
    }
}
