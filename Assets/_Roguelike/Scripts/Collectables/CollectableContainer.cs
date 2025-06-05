using UnityEngine;

namespace Roguelike
{
    public class CollectableContainer : MonoBehaviour, IPoolable, ICollectable
    {
        public string GetObjectId { get { return _objectId; } }
        public Transform trans { get { return transform; } }

        private MobFactory _factory;
        private string _objectId;

        public void Init(MobFactory mobFactory, string objectId)
        {
            _factory = mobFactory;
            _objectId = objectId;
        }

        public void Refresh()
        {

        }

        public void Despawn()
        {

        }
    }
}
