using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    [Header("Rotation Sway Settings")]
    [SerializeField] private float rotationAmount = 1f; // How much it rotates
    [SerializeField] private float rotationSpeed = 1f; // How fast it rotates

    private Quaternion startRotation;

    private void Start()
    {
        startRotation = transform.localRotation;
    }

    private void Update()
    {
        float swayX = Mathf.Sin(Time.time * rotationSpeed) * rotationAmount;
        float swayY = Mathf.Sin(Time.time * rotationSpeed * 0.5f) * (rotationAmount * 0.5f);

        transform.localRotation = startRotation * Quaternion.Euler(swayY, swayX, 0);
    }
}
