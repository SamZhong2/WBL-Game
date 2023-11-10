using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Point : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            FindObjectOfType<GameManager>().addPoint();
        }
    }

}
