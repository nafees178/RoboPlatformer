using UnityEngine;

[CreateAssetMenu(fileName = "BatteryData", menuName = "Inventory/BatteryData")]
public class BatteryData : ScriptableObject
{
    public string id;
    public string batteryName;
    public Sprite icon;
    public GameObject prefab;

}
