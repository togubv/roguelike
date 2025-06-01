using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
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
        public List<MobGroup> mobGroups = new List<MobGroup>();
        public List<LevelMobData> mobs = new List<LevelMobData>();

        public List<ObjectPoolData> _objectsPool = new List<ObjectPoolData>();

        // DEBUG

        public TMP_Text counter;

        public void DespawnMob(HealthMob mob)
        {
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
            SpawnMobDebug();
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

        //private void MoveMobGroups()
        //{
        //    for (int i = 0; i < mobGroups.Count; i++)
        //    {
        //        var mobGroup = mobGroups[i];

        //        for (int k = 0; k < mobGroups[i].mobs.Count; k++)
        //        {
        //            var enemy = mobGroup.mobs[k];
        //            Vector3 dir = (playerTrans.position - enemy.position).normalized;
        //            Vector3 newPos = enemy.position + dir * mobGroup.movespeed * Time.deltaTime;
        //            enemy.position = newPos;
        //            mobGroup.mobs[k] = enemy;

        //            //// Простая проверка на смерть
        //            //if (Vector2.Distance(player.position, enemy.Position) < 0.5f)
        //            //{
        //            //    enemy.IsAlive = false;
        //            //    enemy.View.SetActive(false);
        //            //}

        //            //enemies[i] = enemy;
        //        }

        //        for (int a = 0; a < mobGroup.mobs.Count; a++)
        //        {
        //            for (int b = a + 1; b < mobGroup.mobs.Count; b++)
        //            {
        //                Vector3 dir = mobGroup.mobs[a].position - mobGroup.mobs[b].position;
        //                float dist = dir.magnitude;

        //                if (dist < separationRadius)
        //                {
        //                    if (dist < 0.001f)
        //                    {
        //                        dir = Random.insideUnitCircle.normalized;
        //                        dist = 0.001f;
        //                    }

        //                    Vector3 push = dir.normalized * (separationRadius - dist) * pushStrength * Time.deltaTime;

        //                    var enemyA = mobGroup.mobs[a];
        //                    var enemyB = mobGroup.mobs[b];

        //                    enemyA.position += push;
        //                    enemyB.position -= push;

        //                    mobGroup.mobs[a] = enemyA;
        //                    mobGroup.mobs[b] = enemyB;
        //                }
        //            }
        //        }

        //        mobGroups[i] = mobGroup;
        //    }
        //}

        //private IEnumerator MoveMobGroups()
        //{
        //    while (true)
        //    {
        //        yield return new WaitForSeconds(movementPeriod);

        //        if (mobGroups.Count < 1)
        //        {
        //            continue;
        //        }

        //        for (int i = 0; i < mobGroups.Count; i++)
        //        {
        //            for (int k = 0; k < mobGroups[i].mobs.Count; k++)
        //            {
        //                mobGroups[i].mobs[k].linearVelocity = Vector2.zero;
        //                var direction = (playerTrans.position - mobGroups[i].mobs[k].transform.position).normalized;
        //                mobGroups[i].mobs[k].AddForce(direction * mobGroups[i].movespeed, ForceMode2D.Impulse);
        //            }
        //        }
        //    }
        //}
        
        public void SpawnMobDebug()
        {
            SpawnMob(debugMobId, debugMobsAmount);
        }

        public void SpawnMob(string mobId, int amount = 1, float period = 1f)
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
                    mobPrefab = Instantiate(mobData.prefab, Vector2.zero, Quaternion.identity);
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

        //public void SpawnMobGroup()
        //{
        //    var mobs = new List<Rigidbody2D>();
        //    var mobData = Settings.Get<MobsPool>().GetMobData(debugMobId);

        //    for (int i = 0; i < debugMobsAmount; i++)
        //    {
        //        var spawnedMob = Instantiate(mobData.prefab, Vector2.zero, Quaternion.identity);
        //        Rigidbody2D mobRb = spawnedMob.GetComponent<Rigidbody2D>();
        //        mobs.Add(mobRb);

        //        var randomPush = Random.insideUnitCircle.normalized * Random.Range(0.1f, 0.5f);
        //        mobRb.AddForce(randomPush, ForceMode2D.Impulse);
        //    }

        //    mobGroups.Add(new MobGroup(debugMobId, $"{debugMobId}{debugMobsAmount}", mobData.movespeed, mobs));
        //}
    }
}
