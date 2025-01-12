using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    [SerializeField] private Vector3 teleportCoordinates;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Teleport"))
        {
            transform.position = teleportCoordinates;
        }
    }
}
