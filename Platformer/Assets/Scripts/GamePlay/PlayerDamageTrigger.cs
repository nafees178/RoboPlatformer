using UnityEngine;

public class PlayerDamageTrigger : MonoBehaviour
{
    public int damageAmount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealthManager.instance.DamagePlayer(damageAmount);
        }
    }
}
