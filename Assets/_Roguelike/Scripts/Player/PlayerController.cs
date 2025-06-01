using System.Collections;
using Anthill.Inject;
using UnityEngine;

namespace Roguelike
{
    public class PlayerController : MonoBehaviour
    {
        public Transform characterTrans;

        [Inject] private InputReader _inputReader;

        private void Awake()
        {
            this.InjectMono();
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

                characterTrans.Translate(Vector2.up * 1f * Time.deltaTime);                           
            }
        }
    }
}
