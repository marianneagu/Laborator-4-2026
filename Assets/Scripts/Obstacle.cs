using UnityEngine;

// Put on the wall PREFAB. Collider with "Is Trigger" UNCHECKED.
// The restart logic lives in GameManager: the wall only reports what happened.
//
// No GameManager field needed: the singleton is reachable from anywhere,
// so this works on the prefab too (a prefab can't hold references to scene objects).
public class Obstacle : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Call a PUBLIC method on another script, through the singleton.
            GameManager.Instance.GameOver();
        }
    }
}