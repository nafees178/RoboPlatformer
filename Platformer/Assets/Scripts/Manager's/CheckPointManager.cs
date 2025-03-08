using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    [HideInInspector] public static CheckPointManager instance;

    public Transform CurrentCheckPoint;
    public Transform playerSpawnPoint;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        CurrentCheckPoint = playerSpawnPoint; //Initial CheckPointWill be the spawnPoint
    }

    public void SetCheckPoint(Transform checkPoint)
    {
        CurrentCheckPoint = checkPoint;
    }

   
}
