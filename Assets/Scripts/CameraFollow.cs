using UnityEngine;

// Put on the Main Camera. We don't make the camera a child of the ball,
// because the rolling ball would spin the camera with it.
public class CameraFollow : MonoBehaviour
{
    [SerializeField] 
    private Transform target; // drag the Player here in the Inspector
    [SerializeField]
    private Vector3 offset;

    private void Start()
    {
        // Keep whatever distance you set up in the Scene view.
        offset = transform.position - target.position;
    }

    // LateUpdate runs after all Update calls, so the ball has already moved this frame.
    private void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}
