using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quests.Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        private EnemyController enemyController;

        private void Start()
        {
            enemyController = GetComponent<EnemyController>();
        }

        public void Attack()
        {
            StartCoroutine(AttackCoroutine());
        }

        public void StopAttak()
        {
            StopCoroutine(AttackCoroutine());
        }

        IEnumerator AttackCoroutine()
        {
            if (enemyController.active)
            {
                enemyController.enemyNavMeshController.target.GetComponent<TowerDestinationStats>().GetDamage(enemyController.enemyStats.attackPoints);
                yield return new WaitForSeconds(enemyController.enemyStats.attackRate);
                StartCoroutine(AttackCoroutine());
            }
        }
    }
}