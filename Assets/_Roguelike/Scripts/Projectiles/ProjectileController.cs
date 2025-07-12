using UnityEngine;

namespace Roguelike
{
    public class ProjectileController : MonoBehaviour, IPoolable
    {
        public string GetObjectId { get { return _objectId; } }

        public float movespeed = 1f;
        public float lifeDistance = 10f;
        public int acrossAmount = 0;

        protected MobFactory _factory;
        protected string _objectId;

        protected Vector2 _startPoint;
        protected int _damageValue = 0;

        protected string[] _damageTags;

        public void SetData(int damageValue, string[] damageTags)
        {
            _damageValue = damageValue;
            _damageTags = damageTags;
        }

        public void Init(MobFactory mobFactory, string objectId)
        {
            _factory = mobFactory;
            _objectId = objectId;
        }

        public void Refresh()
        {
            _startPoint = transform.position;
        }

        public void Despawn()
        {
            _factory.DespawnProjectile(this);
        }

        protected virtual void Update()
        {
            Move();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<IDamagable>(out IDamagable iDamagable))
            {
                if (ProjectUtils.CanTagDamage(collision.tag, _damageTags))
                {
                    iDamagable.TakeDamage(_damageValue);
                }
            }
        }

        protected virtual void Move()
        {
            if (Vector2.Distance(transform.position, _startPoint) > lifeDistance)
            {
                Despawn();
            }
        }
    }
}
