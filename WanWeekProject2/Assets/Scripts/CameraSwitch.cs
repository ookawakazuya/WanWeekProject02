using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    [Header("ÉJÉÅÉâê›íË")]
    [SerializeField] Transform player;
    [SerializeField] Vector3 offset = new Vector3(0, 3, -5);
    [SerializeField] Vector3 fixedRotation = new Vector3(20, 0, 0);

    void LateUpdate()
    {
        if (player == null) return;

        transform.position = player.position + offset;
        transform.rotation = Quaternion.Euler(fixedRotation);
    }
}
