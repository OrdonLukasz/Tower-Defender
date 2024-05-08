using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quests.Tower;

public class TurretRotation : MonoBehaviour
{
    [SerializeField] private TurretController towerController;

    private void Update()
    {
        if (towerController.towerTarget)
        {
            Vector3 targetDirection = towerController.towerTarget.position - transform.position;
            float singleStep = towerController.turretStats.rotationSpeed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }
}
