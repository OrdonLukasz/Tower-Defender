using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quests.Enemy
{
    public class EnemyAnimatorController : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private EnemyController enemyController;
        private Vector3 previous;

        private void Start()
        {
            enemyController = GetComponent<EnemyController>();
        }
      
        private void Update()
        {
            float velocity = (transform.position - previous).magnitude / Time.deltaTime;
            previous = transform.position;

            if (enemyController.active)
            {
                if (!enemyController.enemyNavMeshController.CheckDestinationReached())
                {
                    if (velocity > enemyController.enemyStats.walkSpeed)
                    {
                        animator.SetBool("isRunning", true);
                        animator.SetBool("isWalking", false);
                    }
                    if (velocity <= enemyController.enemyStats.walkSpeed && velocity > 0.3f)
                    {
                        animator.SetBool("isRunning", false);
                        animator.SetBool("isWalking", true);
                    }
                    if (velocity <= 0.3f)
                    {
                        animator.SetBool("isWalking", false);
                    }
                }
                else
                {
                    animator.SetBool("isRunning", false);
                    animator.SetBool("isWalking", false);
                    animator.SetBool("isAttacking", true);
                }
            }
            else
            {
                animator.SetBool("isAttacking", false);
                animator.SetBool("isDeath", true);
            }
        }

        public void ResetAnimator()
        {
            animator.SetBool("isDeath", false);
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);
            animator.SetBool("isAttacking", false);
        }
    }
}
