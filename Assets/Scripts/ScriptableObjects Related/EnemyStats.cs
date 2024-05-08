using UnityEngine;
[CreateAssetMenu(menuName = "Enemy Stats")]
public class EnemyStats : ScriptableObject
{
    public float maxHealthPoints;
    public float prizeForKill;

    public float runSpeed;
    public float walkSpeed;

    public float attackPoints;
    public float attackRate;
}
