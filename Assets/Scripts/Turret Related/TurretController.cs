using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quests.Enemy;


namespace Quests.Tower
{
    [RequireComponent(typeof(TurretStats))]
    [RequireComponent(typeof(TurretShooting))]
    [RequireComponent(typeof(TurretBuilder))]
    [RequireComponent(typeof(TurretShooting))]
    public class TurretController : MonoBehaviour
    {
        public TurretRotation towerRotation;
        public Transform towerTarget;
        public TurretShooting towerShooting;
        public TurretStats turretStats;
        public TurretBuilder turretBuilder;

        private void Start()
        {
          StartCoroutine(GetClosestEnemyCoroutine());
        }

        public void OnMouseEnter()
        {
            if (!turretBuilder.isBuilding)
            {
                turretBuilder.turretRangeLine.SetActive(true);
                TurretStatsWindow.Instance.ShowWindow(turretStats.turretName.ToString(), turretStats.shootingRate.ToString(), turretStats.shootingValue.ToString(), turretStats.shootingRange.ToString(), Camera.main.WorldToScreenPoint(transform.position + new Vector3(-3, 0, 0))); ;
            }
        }

        public void OnMouseExit()
        {
            if (!turretBuilder.isBuilding)
            {
                turretBuilder.turretRangeLine.SetActive(false);
                TurretStatsWindow.Instance.HideWindow();
            }
        }

        public void GetClosestEnemy()
        {
            StartCoroutine(GetClosestEnemyCoroutine());
        }

        public void DestroyTurret()
        {
            TurretSpawner.Instance.turrets.Remove(transform);
            Destroy(transform.gameObject);
        }

        public IEnumerator GetClosestEnemyCoroutine()
        {
            if (EnemiesSpawner.Instance.enemiesParrent.childCount > 0)
            {
                int i = 0;
                while (true)
                {
                    if (i < EnemiesSpawner.Instance.enemiesParrent.childCount)
                    {
                        if (EnemiesSpawner.Instance.enemiesParrent.GetChild(i).transform.GetComponent<EnemyController>().active)
                        {
                            if (Vector3.Distance(transform.position, EnemiesSpawner.Instance.enemiesParrent.GetChild(i).transform.position) < turretStats.shootingRange)
                            {
                                towerTarget = EnemiesSpawner.Instance.enemiesParrent.GetChild(i).transform;
                                break;
                            }
                        }
                        i++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            yield return new WaitForSeconds(0.05f);
            StartCoroutine(GetClosestEnemyCoroutine());
        }
    }
}
