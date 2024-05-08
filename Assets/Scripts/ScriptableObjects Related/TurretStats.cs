using UnityEngine;
[CreateAssetMenu(menuName = "Turret Stats")]
public class TurretStats : ScriptableObject
{
    public string turretName;
    public float shootingRate;
    public float shootingValue;
    public float rotationSpeed;
    public float shootingRange;
    public float price;
}
