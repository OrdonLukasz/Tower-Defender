using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Quests.Enemy
{
    public class EnemyNavMeshController : MonoBehaviour
    {
        public NavMeshAgent navmeshAgent;
        public bool destinationReached;
        public Transform target;

        private EnemyController enemyController;
        private BoxCollider accessoryCollider;

        private void Start()
        {
            navmeshAgent = GetComponent<NavMeshAgent>();
            enemyController = GetComponent<EnemyController>();
            accessoryCollider = GetComponent<BoxCollider>();
            navmeshAgent.SetDestination(target.position);
        }

        private void Update()
        {
            CheckQueue(transform.position, transform.forward, 0.8f);
        }

        public bool CheckDestinationReached()
        {
            if (enemyController.active &&  navmeshAgent.enabled)
            {
                navmeshAgent.SetDestination(target.position);
                if (navmeshAgent.remainingDistance < 1 && navmeshAgent.remainingDistance > 0)
                {
                    if(!destinationReached)
                    OnDestinationReached();
                    return true;
                }
                return false;
            }
            return false;
        }

        private void OnDestinationReached()
        {
            if (enemyController.active)
            {
                enemyController.enemyAttack.Attack();
                destinationReached = true;
                transform.SetSiblingIndex(0);
                navmeshAgent.isStopped = true;
            }
        }

        public void ResetNavMesh()
        {
            navmeshAgent.enabled = true;
            destinationReached = false;
        }

        public void OnEnemyDead()
        {
            navmeshAgent.enabled = false;
        }
    
        private void CheckQueue(Vector3 rayStartPosition, Vector3 rayDirection, float lenght)
        {
            Debug.DrawRay(rayStartPosition, rayDirection * lenght, Color.green);

            RaycastHit hit;
            if (Physics.Raycast(rayStartPosition, rayDirection, out hit, lenght, LayerMask.GetMask("Enemy")))
            {
                float hitDistance = hit.distance;
                Debug.DrawRay(rayStartPosition, rayDirection * hitDistance, Color.red);
                if (hit.transform != null)
                {
                    navmeshAgent.speed = enemyController.enemyStats.walkSpeed;
                }
                else
                {
                    navmeshAgent.speed = enemyController.enemyStats.runSpeed;
                }
            }
            else
            {
                navmeshAgent.speed = enemyController.enemyStats.runSpeed;
            }
        }
    }
}
