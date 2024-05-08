using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quests.Enemy;
using Quests.Spell;
using Quests.Tower;
using UnityEngine.Events;
using DG.Tweening;
public class TasksManager : MonoSingleton<TasksManager>
{
    public static TasksManager _Instance { get; private set; }
    public TowerDestinationStats towerDestinationStats;

    [SerializeField] private CameraController cameraController;

    private EnemiesSpawner enemiesSpawner;
    private TurretSpawner turretSpawner;
    private SpellSpawner spellSpawner;
    private ShopManager shopManager;
    private InformationManager informationManager;
   
    void Awake()
    {
        SetupGame();
    }

    private void Start()
    {
        EventTrigger("startGame");
    }

    public void SetupGame()
    {
        enemiesSpawner = GetComponent<EnemiesSpawner>();
        turretSpawner = GetComponent<TurretSpawner>();
        spellSpawner = GetComponent<SpellSpawner>();
        shopManager = GetComponent<ShopManager>();
        informationManager = GetComponent<InformationManager>();
        towerDestinationStats = TowerDestinationStats.Instance;
    }

    public void EventTrigger(string eventName)
    {
        switch (eventName)
        {
            case "startGame":
                informationManager.OpenAlert("Welcome in my tower defense!" +
               "\n1. Use your mouse to move camera with Screen Corners" +
               "\n2. Use scroll to rotate turret" +
               "\n3. Try spells!" +
               "\nGood luck! ");
                cameraController.ChangeCameraPosition(new Vector3(37, -90, 0), new Vector3(150, 8, 62), 13, 25);
                informationManager.AlertClosed += enemiesSpawner.StartGame;
                informationManager.AlertClosed += () => { cameraController.KillTween(); };
                informationManager.AlertClosed += () => { cameraController.ChangeCameraPosition(new Vector3(39, 0, 0), new Vector3(150, 27, 31), 19, 2); };
                informationManager.AlertClosed += () => { informationManager.CloseAlertMessage();
                };

                break;
            
            case "endGame":
                informationManager.OpenAlert("Sorry, game is over" +
              "\n1. If you want to play again please close this window");
                informationManager.AlertClosed += ResetGame;

                enemiesSpawner.Reset();
                turretSpawner.Reset();

                break;

            case "resetGame":
                towerDestinationStats.SetHealth(100);
                enemiesSpawner.StartGame();
                towerDestinationStats.isDestroyed = false;
                break;
        }
    }

    public void ResetGame()
    {
        EventTrigger("resetGame");
    }
}
