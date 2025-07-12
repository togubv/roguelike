using Anthill.Inject;
using UnityEngine;

namespace Roguelike
{
    public class PlayerController : MonoBehaviour
    {
        [Inject] private InputReader _inputReader;
        [Inject] private PlayerStatsManager _playerStatsManager;

        private Transform _characterTrans;
        private float _baseMovespeed;
        private float _additionalMovespeed;
        private float _movespeed { get { return _baseMovespeed + _additionalMovespeed; } }

        private void Awake()
        {
            this.InjectMono();

            _baseMovespeed = Settings.Get<GameSettings>().playerBaseMovespeed;
        }

        private void OnEnable()
        {
            _additionalMovespeed = _playerStatsManager.GetAdditionalStatFixedValue(StatType.Movespeed);

            _playerStatsManager.EventUpdatePlayerStat += UpdatePlayerStatHandler;
            _playerStatsManager.EventInitializeCharacter += InitializeCharacterHandler;
        }

        private void OnDisable()
        {
            _playerStatsManager.EventUpdatePlayerStat -= UpdatePlayerStatHandler;
            _playerStatsManager.EventInitializeCharacter -= InitializeCharacterHandler;
        }

        private void Update()
        {
            Move();
        }

        private void UpdatePlayerStatHandler(StatType type, float fixedValue, float percentValue)
        {
            _additionalMovespeed = fixedValue;
            _additionalMovespeed += (_baseMovespeed * percentValue);
        }

        private void InitializeCharacterHandler(HealthPlayer character)
        {
            _characterTrans = character.transform;
        }

        private void Move()
        {
            if (_inputReader.axisInput != Vector2.zero)
            {
                float angle = Mathf.Atan2(_inputReader.axisInput.y, _inputReader.axisInput.x) * Mathf.Rad2Deg - 90;
                _characterTrans.rotation = Quaternion.Euler(0, 0, angle);

                _characterTrans.Translate(Vector2.up * _movespeed * Time.deltaTime);                           
            }
        }
    }
}
