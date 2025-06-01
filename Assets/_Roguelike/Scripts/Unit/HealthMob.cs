using UnityEngine;

namespace Roguelike
{
    public class HealthMob : Health, IPoolable
    {
        public string GetObjectId { get { return _objectId; } }

        private MobFactory _factory;
        private string _objectId;

        public void Init(MobFactory mobFactory, string objectId)
        {
            _factory = mobFactory;
            _objectId = objectId;
        }

        public void Refresh()
        {
            Init();
        }

        public void Despawn()
        {
            _factory.DespawnMob(this);
        }

        protected override void Death()
        {
            base.Death();

            Despawn();
        }
    }
}
