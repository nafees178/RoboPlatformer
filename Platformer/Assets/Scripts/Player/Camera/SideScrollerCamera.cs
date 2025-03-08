using UnityEngine;
using System.Collections;

public class SideScrollerCamera : MonoBehaviour
{
    [Header("Camera Follow Settings")]
    [SerializeField] public Transform player;
    [SerializeField] private Vector3 offset = new Vector3(3f, 2f, -10f);
    [SerializeField] private float followSpeed = 5f;

    [Header("Landing Effect Settings")]
    [SerializeField] private float landYOffset = -0.3f; // Temporary dip when landing
    [SerializeField] private float landDipSpeed = 8f;   // Speed of dip down
    [SerializeField] private float landResetSpeed = 4f; // Speed of returning back up
    [SerializeField] private float landDipDuration = 0.15f; // How long the dip lasts

    private Vector3 originalOffset;
    private bool isLanding = false;

    private void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player == null)
        {
            Debug.LogError("Player not assigned to SideScrollerCamera script!");
            return;
        }

        originalOffset = offset;
    }

    private void LateUpdate()
    {
        if (player == null) return;

        Vector3 targetPosition = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }

    public void OnLand()
    {
        if (!isLanding)
        {
            isLanding = true;
            StopAllCoroutines();
            StartCoroutine(LandingEffect());
        }
    }

    private IEnumerator LandingEffect()
    {
        Vector3 targetOffset = originalOffset + new Vector3(0, landYOffset, 0);
        float elapsed = 0f;

        // Move camera down quickly
        while (elapsed < landDipDuration)
        {
            offset = Vector3.Lerp(offset, targetOffset, landDipSpeed * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.05f); // Brief pause at lowest point

        elapsed = 0f;

        // Move camera back up smoothly
        while (elapsed < landDipDuration)
        {
            offset = Vector3.Lerp(offset, originalOffset, landResetSpeed * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        offset = originalOffset;
        isLanding = false;
    }
}
