using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public InputAction jumpAction;
    public float jumpForce = 10;
    public float gravityModifier;
    public bool isOnGround = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        //equals physics.gravity = physics.gravity * gravityModifier;
        Physics.gravity *= gravityModifier;
        jumpAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (jumpAction.triggered && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;
    }
}
