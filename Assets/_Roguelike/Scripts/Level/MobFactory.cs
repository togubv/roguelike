using System.Collections.Generic;
using Anthill.Inject;
using TMPro;
using UnityEngine;

namespace Roguelike
{
    public class MobFactory : MonoBehaviour
    {
        public MobsManager mobsManager;
        public string debugMobId;
        public int debugMobsAmount = 5;
        public Transform mobsParent;
        public Transform collectablesParent;
        public Transform projectilesParent;

        public List<ObjectPoolData> _objectsPool = new List<ObjectPoolData>();

        [Inject] private MobsManager _mobsManager;

        // DEBUG

        public TMP_Text counter;

        public void SpawnMob(string mobId, int amount = 1)
        {
            for (int i = 0; i < amount; i++)
            {
                var mobData = Settings.Get<MobsPool>().GetMobData(mobId);
                GameObject mobPrefab = null;

                if (HasObjectInPool(mobId))
                {
                    var objectPool = GetObjectPool(mobId).go;
                    mobPrefab = objectPool[objectPool.Count - 1];
                    objectPool.RemoveAt(objectPool.Count - 1);
                    mobPrefab.transform.position = _mobsManager.GetRandomPointOutsideRadius();
                    mobPrefab.SetActive(true);
                }
                else
                {
                    mobPrefab = Instantiate(mobData.prefab, _mobsManager.GetRandomPointOutsideRadius(), Quaternion.identity, mobsParent);
                    mobPrefab.GetComponent<Health>().Init(mobData.baseHealth);
                }

                var spawnedMobData = new LevelMobData(mobPrefab.transform, mobData.movespeed);

                if (mobPrefab.TryGetComponent<IPoolable>(out IPoolable iPoolable))
                {
                    iPoolable.Init(this, mobId);
                    iPoolable.Refresh();
                }

                mobsManager.mobs.Add(spawnedMobData);
            }

            counter.text = $"{mobsManager.mobs.Count}";
        }

        public void DespawnMob(HealthMob mob)
        {
            if (mob.expCollectable != string.Empty)
            {
                SpawnCollectable(mob.expCollectable, mob.transform.position);
            }

            for (int i = 0; i < mobsManager.mobs.Count; i++)
            {
                if (mobsManager.mobs[i].mobTrans == mob.transform)
                {
                    mobsManager.mobs.RemoveAt(i);
                    break;
                }
            }

            counter.text = $"{mobsManager.mobs.Count}";

            if (HasObjectPool(mob.GetObjectId))
            {
                var objectPool = GetObjectPool(mob.GetObjectId);
                objectPool.go.Add(mob.gameObject);
            }
            else
            {
                var newObjectPool = new ObjectPoolData(mob.GetObjectId, mob.gameObject);
                _objectsPool.Add(newObjectPool);
            }

            mob.gameObject.SetActive(false);
        }

        public void DespawnCollectable(CollectableContainer collectable)
        {
            if (HasObjectPool(collectable.GetObjectId))
            {
                var objectPool = GetObjectPool(collectable.GetObjectId);
                objectPool.go.Add(collectable.gameObject);
            }
            else
            {
                var newObjectPool = new ObjectPoolData(collectable.GetObjectId, collectable.gameObject);
                _objectsPool.Add(newObjectPool);
            }

            collectable.gameObject.SetActive(false);
        }

        public void DespawnProjectile(ProjectileController projectile)
        {
            if (HasObjectPool(projectile.GetObjectId))
            {
                var objectPool = GetObjectPool(projectile.GetObjectId);
                objectPool.go.Add(projectile.gameObject);
            }
            else
            {
                var newObjectPool = new ObjectPoolData(projectile.GetObjectId, projectile.gameObject);
                _objectsPool.Add(newObjectPool);
            }

            projectile.gameObject.SetActive(false);
        }

        public ProjectileController SpawnProjectile(string projectileId, Vector3 spawnPosition)
        {
            var objectData = Settings.Get<PoolableObjects>().GetPoolableObjectData(projectileId);
            GameObject objectPrefab = null;

            if (HasObjectInPool(projectileId))
            {
                var objectPool = GetObjectPool(projectileId).go;
                objectPrefab = objectPool[objectPool.Count - 1];
                objectPool.RemoveAt(objectPool.Count - 1);
                objectPrefab.transform.position = spawnPosition;
                objectPrefab.SetActive(true);
            }
            else
            {
                objectPrefab = Instantiate(objectData.prefab, spawnPosition, Quaternion.identity, projectilesParent);
            }

            if (objectPrefab.TryGetComponent<IPoolable>(out IPoolable iPoolable))
            {
                iPoolable.Init(this, projectileId);
                iPoolable.Refresh();
            }

            return objectPrefab.GetComponent<ProjectileController>();
        }

        private void Awake()
        {
            this.InjectMono();
        }

        private bool HasObjectInPool(string id)
        {
            for (int i = 0; i < _objectsPool.Count; i++)
            {
                if (_objectsPool[i].objectId == id)
                {
                    if (_objectsPool[i].go.Count > 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool HasObjectPool(string id)
        {
            for (int i = 0; i < _objectsPool.Count; i++)
            {
                if (_objectsPool[i].objectId == id)
                {
                    return true;
                }
            }

            return false;
        }

        private ObjectPoolData GetObjectPool(string id)
        {
            for (int i = 0; i < _objectsPool.Count; i++)
            {
                if (_objectsPool[i].objectId == id)
                {
                    return _objectsPool[i];
                }
            }

            return null;
        }

        private void SpawnCollectable(string collectableId, Vector3 spawnPosition)
        {
            var objectData = Settings.Get<PoolableObjects>().GetPoolableObjectData(collectableId);
            GameObject objectPrefab = null;

            if (HasObjectInPool(objectData.objectId))
            {
                var objectPool = GetObjectPool(collectableId).go;
                objectPrefab = objectPool[objectPool.Count - 1];
                objectPool.RemoveAt(objectPool.Count - 1);
                objectPrefab.transform.position = spawnPosition;
                objectPrefab.SetActive(true);
            }
            else
            {
                objectPrefab = Instantiate(objectData.prefab, spawnPosition, Quaternion.identity, collectablesParent);
            }

            if (objectPrefab.TryGetComponent<IPoolable>(out IPoolable iPoolable))
            {
                iPoolable.Init(this, collectableId);
                iPoolable.Refresh();
            }
        }
    }
}
