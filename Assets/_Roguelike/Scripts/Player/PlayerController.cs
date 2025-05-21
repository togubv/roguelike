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
                characterTrans.Translate(_inputReader.axisInput * 1f * Time.deltaTime);
            }
        }
    }
}
