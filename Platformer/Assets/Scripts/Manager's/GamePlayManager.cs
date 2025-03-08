using UnityEngine;
using UnityEngine.InputSystem;

public class GamePlayManager : MonoBehaviour
{
    [HideInInspector] public static GamePlayManager instance;

    [Header("Refrences")]
    public GameObject playerPrefab;

    private SideScrollerCamera cameraScript;


    GameObject player;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        cameraScript = Camera.main.GetComponent<SideScrollerCamera>(); //Gets the refrence from main camera
        SpawnPlayer(CheckPointManager.instance.CurrentCheckPoint);
    }

    void Update()
    {
        //THIS IS FOR DEBUGGING PURPOSES ONLY WILL BE REMOVED LATER
        //if (Input.GetKeyDown(KeyCode.Q)) 
        //{ 
        //    DestroyPlayer();
        //}
        //if (Input.GetKeyDown(KeyCode.E)) 
        //{ 
        //    SpawnPlayer(CheckPointManager.instance.CurrentCheckPoint);
        //}
    }

    public void SpawnPlayer(Transform spawnPoint)
    {
        if(player != null)
        {
            DestroyPlayer();
        }
        player = Instantiate(playerPrefab, spawnPoint.position,spawnPoint.rotation);
        cameraScript.player = player.transform; //Camera will move towards player

    }

    public void DestroyPlayer()
    {
        cameraScript.player = CheckPointManager.instance.CurrentCheckPoint; //Camera will move towards player spawnpoint
        Destroy(player);
    }

}
