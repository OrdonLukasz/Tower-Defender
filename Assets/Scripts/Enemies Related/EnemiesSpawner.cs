using System.Collections;
using System.Collections.Generic;
using MEC;
using UnityEngine;
using Quests.ObjectPool;
using UnityEngine.UI;
using UnityEngine.AI;

namespace Quests.Enemy
{
    public class EnemiesSpawner : MonoSingleton<EnemiesSpawner>
    {
        public static EnemiesSpawner _Instance { get; private set; }
        public List<Transform> enemies = new List<Transform>();

        public int[] waveEnemiesCount;
        public Transform enemiesParrent;

        [HideInInspector] public int killedUnits;

        [SerializeField] private Transform spawnZone;
        [SerializeField] private Transform target;
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private Text waveText;
        [SerializeField] private Text killedUnitsText;

        private int currentWave;
        private PrefabPool _pool;
        private Vector3 spawnPoint;

        private void Awake()
        {
            _pool = new PrefabPool(enemyPrefab, 20);
        }

        void Start()
        {
            NavMeshHit closestHit;

            if (NavMesh.SamplePosition(spawnZone.transform.position, out closestHit, 500f, NavMesh.AllAreas))
            {
                spawnPoint = closestHit.position;
            }
            else
            {
                Debug.LogError("Could not find position on NavMesh!");
            }
        }

        public void StartGame()
        {
            SetupWave(waveEnemiesCount[currentWave]);
        }

        private void SetupWave(int enemiesCount)
        {
            Timing.RunCoroutine(SpawningCoroutine(enemiesCount));
            UpdateUI();
        }

        public void Reset()
        {
            foreach (Transform enemy in enemies)
            {
                enemy.GetComponent<EnemyController>().OnDead();
            }

            currentWave = 0;
            killedUnits = 0;
            UpdateUI();
        }

        private void UpdateUI()
        {
            waveText.text = $"Current wave: {currentWave}";
            killedUnitsText.text = $"Killed units: {killedUnits}/{waveEnemiesCount[currentWave]}";
        }

        public void OnUnitKill()
        {
            killedUnits++;
            killedUnitsText.text = $"Killed units: {killedUnits}/{waveEnemiesCount[currentWave]}";
            if (killedUnits == waveEnemiesCount[currentWave])
            {
                killedUnits = 0;
                currentWave++;
                if (currentWave < waveEnemiesCount.Length)
                {
                    if (!TowerDestinationStats.Instance.isDestroyed)
                        SetupWave(waveEnemiesCount[currentWave]);
                }
                else
                {
                    InformationManager.Instance.OpenAlert("Nice Job!");
                }
            }
        }

        private IEnumerator<float> SpawningCoroutine(int waveEnemiesCount)
        {
            for (int i = 0; i < waveEnemiesCount; i++)
            {
                yield return  Timing.WaitForSeconds(Random.Range(0.2f, 0.3f));
                var enemy = _pool.GetInstance() as EnemyController;
                enemy.transform.position = spawnPoint;
                enemy.enemyNavMeshController.target = target;
                enemy.transform.parent = enemiesParrent;
                enemy.enemyHealth.maxHealth = enemy.enemyStats.maxHealthPoints * currentWave;
                enemy.enemyNavMeshController.ResetNavMesh();
                enemies.Add(enemy.transform);
            }
        }
    }
}