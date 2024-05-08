using System.Collections.Generic;
using UnityEngine;
using Quests.Tower;
public class TurretSpawner : MonoSingleton<TurretSpawner>
{
    public static TurretSpawner _Instance { get; private set; }
    public GameObject TurretRangeLine;

    public List<Transform> turrets = new List<Transform>();
    public void SpawnTower(GameObject towerPrefab)
    {
        GameObject turret = Instantiate(towerPrefab);
        turret.GetComponent<TurretController>().turretBuilder.turretRangeLine = Instantiate(TurretRangeLine, turret.transform);
        turrets.Add(turret.transform);
    }

    public void Reset()
    {
        for (int i = 0; i < turrets.Count; i++)
        {
            turrets[i].GetComponent<TurretController>().DestroyTurret();
        }
    }
}


