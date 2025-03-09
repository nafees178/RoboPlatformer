using System;
using UnityEngine;

public class BasicBattery : MonoBehaviour, ICollectible
{
    public static event HandleBasicBatteryCollected OnBasicBatteryCollected;
    public delegate void HandleBasicBatteryCollected(ItemData itemData);

    public ItemData BasicBatteryData;
    public void Collect()
    {
        Destroy(gameObject);
        OnBasicBatteryCollected?.Invoke(BasicBatteryData);
        Debug.Log("Basic Battery");
    }
}
