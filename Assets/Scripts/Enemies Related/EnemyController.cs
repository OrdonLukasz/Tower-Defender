using UnityEngine;
using Quests.ObjectPool;
using System.Collections;

namespace Quests.Enemy
{
    public class EnemyController : MonoBehaviour, IPoolable
    {
        public EnemyNavMeshController  enemyNavMeshController;
        public EnemyAnimatorController enemyAnimatorController;
        public EnemyStats enemyStats; 
        public EnemyAttack enemyAttack;
        public EnemyHealth enemyHealth;
        private PrefabPool _pool;
        public bool active;
        
        private void FixedUpdate()
        {
            if (!active)
            {
                return;
            }
        }
       
        public void SetParentPool(PrefabPool pool)
        {
            _pool = pool;
        }

        public void PrepareForPooling()
        {
            active = false;
            Destroy(GetComponent<Rigidbody>());
            transform.rotation = new Quaternion(0, 0, 0, 0);
            enemyAnimatorController.ResetAnimator();
            gameObject.SetActive(false);
            return;
        }

        public void HandleSpawn()
        {
            active = true;
            gameObject.SetActive(true);
        }
   
        public void OnDead()
        {
            StartCoroutine(OnDeadCoroutine());
        }

        IEnumerator OnDeadCoroutine()
        {
            active = false;
            enemyNavMeshController.OnEnemyDead();
            transform.SetSiblingIndex(transform.parent.childCount);
            ShopManager.Instance.AddCash(enemyStats.prizeForKill);
            enemyAttack.StopAttak();
            EnemiesSpawner.Instance.OnUnitKill();
            yield return new WaitForSeconds(1);
            EnemiesSpawner.Instance.enemies.Remove(transform.transform);
            yield return new WaitForSeconds(2);
            enemyHealth.ResetStats();
            _pool.AcceptReturning(this);
        }
    }
}
