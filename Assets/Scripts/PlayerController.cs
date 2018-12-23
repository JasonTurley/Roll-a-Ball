using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    
    public float speed;
    public Text countText;
    public Text winText;
    public Text timerText;

	private Rigidbody rb;
    private uint count;
    private float startTime;
    private float endTime;

	// Called on the first frame of the game
	void Start()
	{
		rb = GetComponent<Rigidbody>();
        count = 0;
        DisplayCountText();
        winText.text = "";
        startTime = 0.0f;
        DisplayTimerText(startTime);
	}

    // Player physics
	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertival = Input.GetAxis("Vertical");

		// y-dimension is set to 0 since the ball will not move up or down 
		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertival);

		rb.AddForce(movement * speed);
	}

    void Update()
    {
        // Check if player has won
        if (count >= 12)
        {
            winText.text = "You Win!";

            // stop timer
            endTime = startTime;
            DisplayTimerText(endTime);
        } else
        {
            startTime += Time.deltaTime;    // Time.deltaTime will increase timer by 1 second each frame
            DisplayTimerText(startTime);
        }
    }

    // Hide Pick Up object upon collision
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count++;
            DisplayCountText();
        }
    }

    void DisplayCountText()
    {
        countText.text = "Count: " + count.ToString();
    }

    void DisplayTimerText(float timer)
    {
        timerText.text = "Time: " + timer.ToString();
    }
}
