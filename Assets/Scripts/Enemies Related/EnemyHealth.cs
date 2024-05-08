using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Quests.Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        public float maxHealth;

        [SerializeField] private Canvas healthCanvas;
        [SerializeField] private Image healthBar;

        private EnemyController enemyController;

        private float currentHealthPoints;

        private void Start()
        {
            enemyController = GetComponent<EnemyController>();
            maxHealth = enemyController.enemyStats.maxHealthPoints;
            currentHealthPoints = maxHealth;
            healthBar.fillAmount = 1;
        }

        public void ResetStats()
        {
            currentHealthPoints = enemyController.enemyStats.maxHealthPoints;
            healthBar.fillAmount = 1;
            healthCanvas.gameObject.SetActive(false);
        }

        public void ReactForHit(float hitValue)
        {
            currentHealthPoints -= hitValue;
            healthBar.fillAmount = currentHealthPoints / enemyController.enemyStats.maxHealthPoints;
            if (currentHealthPoints < enemyController.enemyStats.maxHealthPoints)
            {
                healthCanvas.gameObject.SetActive(true);
            }
            if (currentHealthPoints <= 0)
            {
                enemyController.OnDead();
            }
        }
    }
}
