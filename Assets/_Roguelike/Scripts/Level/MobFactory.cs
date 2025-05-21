using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike
{
    public class MobFactory : MonoBehaviour
    {
        public string debugMobId;
        public int debugMobsAmount = 5;
        public Transform playerTrans;
        public float movementPeriod = 0.5f;
        public List<MobGroup> mobGroups = new List<MobGroup>();

        private void Start()
        {
            SpawnMobGroup();
            StartCoroutine(MoveMobGroups());
        }

        private IEnumerator MoveMobGroups()
        {
            while (true)
            {
                yield return new WaitForSeconds(movementPeriod);

                if (mobGroups.Count < 1)
                {
                    continue;
                }

                for (int i = 0; i < mobGroups.Count; i++)
                {
                    for (int k = 0; k < mobGroups[i].mobs.Count; k++)
                    {
                        mobGroups[i].mobs[k].linearVelocity = Vector2.zero;
                        var direction = (playerTrans.position - mobGroups[i].mobs[k].transform.position).normalized;
                        mobGroups[i].mobs[k].AddForce(direction * mobGroups[i].movespeed, ForceMode2D.Impulse);
                    }
                }
            }
        }

        public void SpawnMobGroup()
        {
            var mobs = new List<Rigidbody2D>();
            var mobData = Settings.Get<MobsPool>().GetMobData(debugMobId);

            for (int i = 0; i < debugMobsAmount; i++)
            {
                var spawnedMob = Instantiate(mobData.prefab, Vector2.zero, Quaternion.identity);
                Rigidbody2D mobRb = spawnedMob.GetComponent<Rigidbody2D>();
                mobs.Add(mobRb);

                var randomPush = Random.insideUnitCircle.normalized * Random.Range(0.1f, 0.5f);
                mobRb.AddForce(randomPush, ForceMode2D.Impulse);
            }

            mobGroups.Add(new MobGroup(debugMobId, $"{debugMobId}{debugMobsAmount}", mobData.movespeed, mobs));
        }
    }
}
