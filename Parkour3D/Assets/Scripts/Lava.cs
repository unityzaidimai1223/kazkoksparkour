using UnityEngine;

public class Lava : MonoBehaviour
{
    public Vector3 respawnPosition; // Position to teleport the player upon touching lava

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player has collided with the lava
        if (collision.gameObject.CompareTag("Player"))
        {
            // Teleport the player to the respawn position
            collision.gameObject.transform.position = respawnPosition;
        }
    }
}
