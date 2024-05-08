using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quests.Enemy;
namespace Quests.Tower
{
    public class TurretShooting : MonoBehaviour
    {
        public bool isShooting = false;

        [SerializeField] private TurretController towerController;
        [SerializeField] private ParticleSystem towerParticles;

        private void Start()
        {
            towerController = GetComponent<TurretController>();
            StartCoroutine(TowerShootingCoroutine());
        }

        IEnumerator TowerShootingCoroutine()
        {
            if (isShooting)
            {
                if (towerController.towerTarget != null)
                {
                    if (towerController.towerTarget.GetComponent<EnemyController>().active)
                    {
                        if (towerParticles)
                        {
                            towerParticles.Play();
                        }
                        towerController.towerTarget.GetComponent<EnemyHealth>().ReactForHit(towerController.turretStats.shootingValue);
                    }
                }
            }
            yield return new WaitForSeconds(1/towerController.turretStats.shootingRate);
            StartCoroutine(TowerShootingCoroutine());
        }
    }
}