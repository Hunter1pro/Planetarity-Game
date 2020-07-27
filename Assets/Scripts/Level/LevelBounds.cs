using UnityEngine;

public class LevelBounds : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        MonoBehaviour.Destroy(collision.gameObject);
    }
}
