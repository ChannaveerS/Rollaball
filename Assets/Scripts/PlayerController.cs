using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
// This class is responsible for controlling the player
public class PlayerController : MonoBehaviour
{
    public float speed = 08;
    public TextMeshProUGUI countText;
    private Rigidbody rb = null;
    private float movementX;
    private float movementY;
    private int count = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // initially get the Rigidbody component and store in rb variable
        rb = GetComponent<Rigidbody>();
        setCountText();
    }

    // this function is called when the player moves
    // InputValue is a class in UnityEngine.InputSystem and is used to store the value of the input
    void OnMove(InputValue movementValue) 
    {
        // 2d vector to store the movement value
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
         
    }
    // FixedUpdate is called once per frame
    // it is called before physics calculations
    // it is used to apply forces to the Rigidbody
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);

    }

    // this function is called when the player collides with other object
    // Collider is a class in UnityEngine and is used to store the collision data
    // other is the collider that the player collided with
    // OnTriggerEnter is called when the Collider other enters the trigger
    void OnTriggerEnter(Collider other)
    {
        // Check if the object the player collided with has the "PickUp" tag.

        if (other.gameObject.CompareTag("Pickup"))
        { // Deactivate the collided object (making it disappear).

            other.gameObject.SetActive(false);
            count++;
            setCountText();
            if (count == 15)
            {
                countText.text = "You Win!";
            }
        }
    }

    void setCountText()
    {
        countText   .text = "Count - " + count.ToString();
    }
}
