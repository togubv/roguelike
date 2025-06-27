using System.Collections.Generic;
using UnityEngine;

namespace Roguelike
{
    public class MobsManager : MonoBehaviour
    {
        public Transform playerTrans;
        public float pushStrength = 1f;
        public float movementPeriod = 0.5f;
        public float separationRadius = 0.5f;
        public float respawnRadius;
        public float respawnMinRadius;
        public float respawnMaxRadius;

        public List<LevelMobData> mobs = new List<LevelMobData>();

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

                if (Vector2.Distance(playerTrans.position, newPos) > respawnRadius)
                {
                    newPos = GetRandomPointOutsideRadius(playerTrans.position, respawnMinRadius, respawnMaxRadius);
                }

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

        public Vector2 GetRandomPointOutsideRadius(Vector2 point, float minRadius, float maxRadius)
        {
            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            float distance = Random.Range(minRadius, maxRadius);

            float x = point.x + Mathf.Cos(angle) * distance;
            float y = point.y + Mathf.Sin(angle) * distance;

            return new Vector2(x, y);
        }
    }
}
