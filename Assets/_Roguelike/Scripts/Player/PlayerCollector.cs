using UnityEngine;
using System.Collections.Generic;
using Anthill.Inject;

namespace Roguelike
{
    public class PlayerCollector : MonoBehaviour
    {
        public float magnetizeSpeed = 1f;
        public float collectRange = 0.5f;

        public List<Transform> _collects = new List<Transform>();

        [Inject] private LevelManager _levelManager;

        private void Awake()
        {
            this.InjectMono();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<ICollectable>(out ICollectable iCollectable))
            {
                _collects.Add(iCollectable.trans);
            }
        }

        private void Update()
        {
            Magnetism();
        }

        private void Magnetism()
        {
            if (_collects.Count < 1)
            {
                return;
            }

            for (int i = _collects.Count - 1; i >= 0; i--)
            {
                _collects[i].position = Vector2.MoveTowards(_collects[i].position, transform.position, magnetizeSpeed * Time.deltaTime);

                if (Vector2.Distance(transform.position, _collects[i].position) <= collectRange)
                {
                    Collect(_collects[i]);
                }
            }
        }

        private void Collect(Transform collectable)
        {
            _collects.Remove(collectable);
            collectable.gameObject.SetActive(false);
            _levelManager.IncreasePlayerExperience(1);
        }
    }
}
