using UnityEngine;
using System.Collections.Generic;
using Anthill.Inject;

namespace Roguelike
{
    public class PlayerCollector : MonoBehaviour
    {
        public float magnetizeSpeed = 1f;
        public float collectRange = 0.5f;

        public List<CollectableContainer> _collects = new List<CollectableContainer>();

        [Inject] private LevelManager _levelManager;
        [Inject] private MobFactory _mobFactory;

        private void Awake()
        {
            this.InjectMono();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<CollectableContainer>(out CollectableContainer collectableContainer))
            {
                _collects.Add(collectableContainer);
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
                _collects[i].trans.position = Vector2.MoveTowards(_collects[i].trans.position, transform.position, magnetizeSpeed * Time.deltaTime);

                if (Vector2.Distance(transform.position, _collects[i].trans.position) <= collectRange)
                {
                    Collect(_collects[i]);
                }
            }
        }

        private void Collect(CollectableContainer collectable)
        {
            _collects.Remove(collectable);
            _levelManager.IncreasePlayerExperience(1);
            _mobFactory.DespawnCollectable(collectable);
        }
    }
}
