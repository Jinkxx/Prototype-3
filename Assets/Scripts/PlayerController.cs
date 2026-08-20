using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public InputAction jumpAction;
    public float jumpForce = 10;
    public float gravityModifier;
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
        if (jumpAction.triggered)
        {
            playerRb.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }
    }
}
