using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quests.Tower;
public class TurretBuilder : MonoBehaviour
{
    public bool canBuild = true;
    public GameObject turretRangeLine;
    public bool isBuilding = true;

    [SerializeField] private GameObject colliderBox;

    private TurretController towerController;
    private Vector3 lastPoint;
  

    private void Start()
    {
        towerController = GetComponent<TurretController>();
        turretRangeLine.transform.localPosition = new Vector3(0, 0.2f, 0);
        turretRangeLine.transform.localScale *= towerController.turretStats.shootingRange;
    }

    private void Update()
    {
        if (isBuilding)
        {
            transform.position = GetMouseHitPoint();

            if (canBuild && Input.GetMouseButtonDown(0))
            {
                if (ShopManager.Instance.getCurrentCash() >= towerController.turretStats.price)
                {
                    ShopManager.Instance.SubtractCash(towerController.turretStats.price);
                    isBuilding = false;
                    towerController.towerShooting.isShooting = true;
                    turretRangeLine.SetActive(false);
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                Destroy(this.gameObject);
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                transform.Rotate(Vector3.down * 2.5f, Space.Self);
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                transform.Rotate(Vector3.up * 2.5f, Space.Self);
            }
        }
    }

    Vector3 GetMouseHitPoint()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Terrain")))
        {
            lastPoint = hit.point;
            return lastPoint;
        }
        else
        {
            return lastPoint;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "terrain" && isBuilding)
        {
            canBuild = false;
            colliderBox.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isBuilding)
        {
            canBuild = true;
            colliderBox.SetActive(false);
        }
    }
}
