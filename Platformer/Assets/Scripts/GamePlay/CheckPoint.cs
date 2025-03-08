using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckPointManager.instance.SetCheckPoint(transform);
            gameObject.SetActive(false);
        }
    }
}
