using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Roguelike
{
    public class MobFactory : MonoBehaviour
    {
        public string debugMobId;
        public int debugMobsAmount = 5;
        public Transform playerTrans;
        public float movementPeriod = 0.5f;
        public float separationRadius = 0.5f;
        public float pushStrength = 1f;
        public Transform mobsParent;
        public Transform collectablesParent;
        public Transform projectilesParent;
        public List<MobGroup> mobGroups = new List<MobGroup>();
        public List<LevelMobData> mobs = new List<LevelMobData>();

        public List<ObjectPoolData> _objectsPool = new List<ObjectPoolData>();

        // DEBUG

        public TMP_Text counter;

        public void DespawnMob(HealthMob mob)
        {
            if (mob.expCollectable != string.Empty)
            {
                SpawnCollectable(mob.expCollectable, mob.transform.position);
            }

            for (int i = 0; i < mobs.Count; i++)
            {
                if (mobs[i].mobTrans == mob.transform)
                {
                    mobs.RemoveAt(i);
                    break;
                }
            }

            counter.text = $"{mobs.Count}";

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
        }

        public ProjectileController SpawnProjectile(string projectileId, Vector3 spawnPosition)
        {
            var objectData = Settings.Get<PoolableObjects>().GetPoolableObjectData(projectileId);
            GameObject objectPrefab = null;

            if (HasObjectInPool(objectData.objectId))
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

        private void Start()
        {

        }

        private void Update()
        {
            MoveMobs();
        }

        private void MoveMobs()
        {
            for (int i = 0; i < mobs.Count; i++)
            {
                Vector2 direction = (playerTrans.position - mobs[i].mobTrans.position).normalized;
                Vector2 newPos = (Vector2)mobs[i].mobTrans.position + direction * mobs[i].movespeed * Time.deltaTime;
                mobs[i].mobTrans.position = newPos;
            }

            for (int i = 0; i < mobs.Count; i++)
            {
                for (int k = i + 1; k < mobs.Count; k++)
                {
                    Vector2 direction = mobs[i].mobTrans.position - mobs[k].mobTrans.position;
                    float distance = direction.magnitude;

                    if (distance < separationRadius)
                    {
                        if (distance < 0.001f)
                        {
                            direction = Random.insideUnitCircle.normalized;
                            distance = 0.001f;
                        }

                        Vector3 push = direction.normalized * (separationRadius - distance) * pushStrength * Time.deltaTime;

                        var enemyA = mobs[i];
                        var enemyB = mobs[k];

                        enemyA.mobTrans.position += push;
                        enemyB.mobTrans.position -= push;

                        mobs[i] = enemyA;
                        mobs[k] = enemyB;
                    }
                }
            }
        }

        private void SpawnMobDebug()
        {
            SpawnMob(debugMobId, debugMobsAmount);
        }

        private void SpawnMob(string mobId, int amount = 1, float period = 1f)
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
                    mobPrefab.transform.position = Vector2.zero;
                    mobPrefab.SetActive(true);
                }
                else
                {
                    mobPrefab = Instantiate(mobData.prefab, Vector2.zero, Quaternion.identity, mobsParent);
                }

                var spawnedMobData = new LevelMobData(mobPrefab.transform, mobData.movespeed);
                
                if (mobPrefab.TryGetComponent<IPoolable>(out IPoolable iPoolable))
                {
                    iPoolable.Init(this, mobId);
                    iPoolable.Refresh();
                }

                mobs.Add(spawnedMobData);
            }

            counter.text = $"{mobs.Count}";
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
