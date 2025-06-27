using Anthill.Inject;
using UnityEngine;

namespace Roguelike
{
    public class PlayerController : MonoBehaviour
    {
        public Transform characterTrans;

        [Inject] private InputReader _inputReader;

        private float _baseMovespeed;

        private void Awake()
        {
            this.InjectMono();

            _baseMovespeed = Settings.Get<GameSettings>().playerBaseMovespeed;
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            if (_inputReader.axisInput != Vector2.zero)
            {
                float angle = Mathf.Atan2(_inputReader.axisInput.y, _inputReader.axisInput.x) * Mathf.Rad2Deg - 90;
                characterTrans.rotation = Quaternion.Euler(0, 0, angle);

                characterTrans.Translate(Vector2.up * _baseMovespeed * Time.deltaTime);                           
            }
        }
    }
}
