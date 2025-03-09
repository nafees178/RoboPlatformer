using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image inventoryIcon;
    public TextMeshProUGUI stackSizeText;

    public void ClearSlot()
    {
        inventoryIcon.enabled = false;
        stackSizeText.enabled = false;
    }

    public void DrawSlot(InventoryItem item)
    {
        if (item == null)
        {
            ClearSlot();
            return;
        }

        inventoryIcon.enabled = true;
        stackSizeText.enabled = true;

        inventoryIcon.sprite = item.itemData.icon;
        stackSizeText.text = item.stackSize.ToString();

    }


}
