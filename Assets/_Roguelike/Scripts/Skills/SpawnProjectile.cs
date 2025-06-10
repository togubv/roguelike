using Anthill.Inject;
using UnityEngine;

namespace Roguelike
{
    public class SpawnProjectile : MonoBehaviour
    {
        [Inject] private MobFactory _mobFactory;

        public string projectileId;
        public Transform spawnPoint;

        public void Spawn(int damageValue)
        {
            var spawnedProjectile = _mobFactory.SpawnProjectile(projectileId, spawnPoint.position);
            spawnedProjectile.transform.rotation = transform.rotation;
            spawnedProjectile.SetData(damageValue);
        }

        private void Awake()
        {
            this.InjectMono();
        }
    }
}
