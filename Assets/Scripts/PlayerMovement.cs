using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    private InputAction moveAction;
    [SerializeField]
    private float speed = 0.2f;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float force = 2f;

    private bool canMove;

    Vector3 direction;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canMove = true;
        moveAction = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        SetMoveDirection();

    }

    private void SetMoveDirection()
    {
        if (!canMove)
            return;
        Vector2 input = moveAction.ReadValue<Vector2>();
        direction = new Vector3(input.x, 0f, input.y);
    }

    private void FixedUpdate()
    {
        rb.AddForce(direction * force);
        
    }

    public bool GetCanMove()
    {
        return canMove;
    }

    public void SetCanMove(bool newCanMove)
    {
        canMove = newCanMove;
    }
}
