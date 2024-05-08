using UnityEngine;
using UnityEngine.UI;
using Quests.Enemy;
public class TowerDestinationStats : MonoSingleton<TowerDestinationStats>
{
    public static TowerDestinationStats _Instance { get; private set; }
    public bool isDestroyed;
    public float healthPoints;

    [SerializeField] private Text textHealthPoints;

    public void GetDamage(float damageValue)
    {
        if (!isDestroyed)
        {
            if (healthPoints > 0)
            {
                healthPoints -= damageValue;
            }
            if (healthPoints < 0)
            {

                healthPoints = 0;
            }
            if (healthPoints == 0)
            {
                isDestroyed = true;
                TasksManager.Instance.EventTrigger("endGame");
            }
        }
        textHealthPoints.text = $"HP {healthPoints}";
    }

    public void SetHealth(float points)
    {
        healthPoints = points;
        textHealthPoints.text = $"HP {healthPoints}";
    }
}
