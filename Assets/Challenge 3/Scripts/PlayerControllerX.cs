using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;

    public float floatForce;
    private float gravityModifier = 1.5f;
    private Rigidbody playerRb;

    public InputAction floatAction;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public bool isOnGround = true;
    public float jumpForce = 10;
    public AudioClip BounceSound;
    public float maxHeight = 16f;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
        floatAction.Enable();
        playerRb = GetComponent<Rigidbody>();
        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        // While space is pressed and player is low enough, float up
        if (floatAction.IsPressed() && !gameOver && transform.position.y < maxHeight)
        {
            playerRb.AddForce(Vector3.up * floatForce);
        }
    }
    void LateUpdate()
{
    if (transform.position.y > maxHeight)
    {
        // Keep the player at the maximum height
        Vector3 position = transform.position;
        position.y = maxHeight;
        transform.position = position;

        // Stop upward movement, but allow falling
        Vector3 velocity = playerRb.linearVelocity;
        velocity.y = Mathf.Min(velocity.y, 0f);
        playerRb.linearVelocity = velocity;
    }
}

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Hit: " + other.gameObject.name
        + " | Tag: " + other.gameObject.tag
        + " | Game Over: " + gameOver);
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);

        }
        if (other.gameObject.CompareTag("Ground") && !gameOver)
        {
            Debug.Log("Bounce triggered! Force: " + jumpForce);

            // Remove downward momentum
            Vector3 velocity = playerRb.linearVelocity;
            velocity.y = 0;
            playerRb.linearVelocity = velocity;

            // Bounce and play sound
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAudio.PlayOneShot(BounceSound, 1.0f);
            isOnGround = false;
        }
        
    }
}
