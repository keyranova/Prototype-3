using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;

    public float floatForce;
	public float bounceForce;
    private float gravityModifier = 1.5f;
	public float maxY = 15;
	public float minY = 10;
    private Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;
	public AudioClip groundedSound;


    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
		playerRb = GetComponent<Rigidbody>();

        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        // While space is pressed and player is low enough, float up
        if (Input.GetKey(KeyCode.Space) && !gameOver && transform.position.y < maxY)
        {
            playerRb.AddForce(Vector3.up * floatForce);
        }

		if (transform.position.y < minY)
		{
			playerRb.AddForce(Vector3.up * bounceForce);
		}
    }

    private void OnCollisionEnter(Collision other)
    {
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

		} else if (other.gameObject.CompareTag("Ground"))
		{
			playerAudio.PlayOneShot(groundedSound, 1.0f);
		}

    }

}
