using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    public DestroyablePlatform platform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            platform.StartFallCountdown();
        }
    }
}
