using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed = 8f;
    public TextMeshProUGUI countText;
    private Rigidbody rb = null;
    private float movementX;
    private float movementY;
    private int count = 0;
    public int timeToAdd = 5;

    // Slowdown variables
    public float slowDownMultiplier = 0.5f; // Factor to reduce speed
    public float slowDownDuration = 3f;    // Duration of slowdown in seconds
    private bool isSlowedDown = false;
    private float normalSpeed;

    // Reference to TimerScript
    private TimerScript timerScript;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        normalSpeed = speed;
        setCountText();

        timerScript = Object.FindFirstObjectByType<TimerScript>();

        if (timerScript == null)
        {
            Debug.LogError("TimerScript not found in the scene!");
        }
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") && !isSlowedDown)
        {
            StartCoroutine(SlowDownPlayer());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count++;
            setCountText();

            if (count == 15)
            {
                countText.text = "You Win!";
                rb.isKinematic = true;
            }
        }

        if (other.gameObject.CompareTag("Extratime"))
        {
            other.gameObject.SetActive(false);
            StartCoroutine(DisplayExtraTime("+ " + timeToAdd + " sec"));
            timerScript.AddTime(timeToAdd);
        }
    }

    private IEnumerator SlowDownPlayer()
    {
        isSlowedDown = true;
        float previousSpeed = speed;
        speed *= slowDownMultiplier; // Reduce speed
        Debug.Log("Player slowed down! New speed: " + speed);

        float remainingTime = slowDownDuration;

        while (remainingTime > 0)
        {
            countText.text = $"Slowed Down: {remainingTime:F1} seconds left";
            remainingTime -= Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        speed = previousSpeed; // Restore original speed
        isSlowedDown = false;
        Debug.Log("Player speed restored! Speed: " + speed);

        setCountText(); // Revert the text back to normal
    }

    private IEnumerator DisplayExtraTime(string message)
    {
        countText.text = message;
        yield return new WaitForSeconds(2f); // Display the message for 2 seconds
        setCountText();
    }

    void setCountText()
    {
        countText.text = "Count - " + count.ToString();
    }
}
