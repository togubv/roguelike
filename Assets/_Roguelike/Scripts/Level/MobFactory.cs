using System.Collections;
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
        public List<MobGroup> mobGroups = new List<MobGroup>();

        // DEBUG

        public TMP_Text counter;

        private void Start()
        {
            SpawnMobGroup();
            //StartCoroutine(MoveMobGroups());
        }

        private void Update()
        {
            MoveMobGroups();
        }

        private void MoveMobGroups()
        {
            for (int i = 0; i < mobGroups.Count; i++)
            {
                var mobGroup = mobGroups[i];

                for (int k = 0; k < mobGroups[i].mobs.Count; k++)
                {
                    var enemy = mobGroup.mobs[k];
                    Vector3 dir = (playerTrans.position - enemy.position).normalized;
                    Vector3 newPos = enemy.position + dir * mobGroup.movespeed * Time.deltaTime;
                    enemy.position = newPos;
                    mobGroup.mobs[k] = enemy;

                    //// Простая проверка на смерть
                    //if (Vector2.Distance(player.position, enemy.Position) < 0.5f)
                    //{
                    //    enemy.IsAlive = false;
                    //    enemy.View.SetActive(false);
                    //}

                    //enemies[i] = enemy;
                }

                for (int a = 0; a < mobGroup.mobs.Count; a++)
                {
                    for (int b = a + 1; b < mobGroup.mobs.Count; b++)
                    {
                        Vector3 dir = mobGroup.mobs[a].position - mobGroup.mobs[b].position;
                        float dist = dir.magnitude;

                        if (dist < separationRadius)
                        {
                            if (dist < 0.001f)
                            {
                                dir = Random.insideUnitCircle.normalized;
                                dist = 0.001f;
                            }

                            Vector3 push = dir.normalized * (separationRadius - dist) * pushStrength * Time.deltaTime;

                            var enemyA = mobGroup.mobs[a];
                            var enemyB = mobGroup.mobs[b];

                            enemyA.position += push;
                            enemyB.position -= push;

                            mobGroup.mobs[a] = enemyA;
                            mobGroup.mobs[b] = enemyB;
                        }
                    }
                }

                mobGroups[i] = mobGroup;
            }
        }

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

        public void SpawnMobGroup()
        {
            var mobs = new List<Transform>();
            var mobData = Settings.Get<MobsPool>().GetMobData(debugMobId);

            for (int i = 0; i < debugMobsAmount; i++)
            {
                var spawnedMob = Instantiate(mobData.prefab, Vector2.zero, Quaternion.identity);
                mobs.Add(spawnedMob.transform);
            }

            mobGroups.Add(new MobGroup(debugMobId, $"{debugMobId}{debugMobsAmount}", mobData.movespeed, mobs));

            int count = 0;

            for (int i = 0; i < mobGroups.Count; i++)
            {
                count += mobGroups[i].mobs.Count;
            }

            counter.text = $"{count}";
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
