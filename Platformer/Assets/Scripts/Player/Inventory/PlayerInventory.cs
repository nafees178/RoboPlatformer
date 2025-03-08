using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    [Header("Player Inventory")]
    [Space]
    [Tooltip("The Input action to open the inventory UI")]
    [SerializeField] private InputAction inventoryIA;
    [Space]
    [Tooltip("The reference to the inventory menu in the inspector")]
    [SerializeField] private GameObject inventoryUI;


    // Private Variables
    bool isInventoryVisible;

    private void OnEnable()
    {
        inventoryIA.Enable();
        isInventoryVisible = false;
        inventoryUI.SetActive(false);


        if (inventoryUI == null)
        {
            Debug.LogError("Inventory UI Needs to be assigned in the inspector.");
        }
    }

    private void Update()
    {
        InventoryBind();
    }

    private void InventoryBind()
    {

        if (inventoryIA.WasPressedThisFrame() && !isInventoryVisible)
        {
            inventoryUI.SetActive(true);
            isInventoryVisible = true;
        } 

        else if (inventoryIA.WasPressedThisFrame() && isInventoryVisible)
        {
            inventoryUI.SetActive(false);
            isInventoryVisible = false;
        }
    }
}
