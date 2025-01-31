using UnityEngine;

public class ExtratimeRotator : MonoBehaviour
{
    public float rotationSpeedMultiplier = 2f; // Multiplier for faster rotation
    public float verticalMovementSpeed = 0.5f; // Speed for vertical movement
    public float verticalMovementAmplitude = 0.2f; // Amplitude for vertical movement

    private Vector3 initialPosition;

    void Start()
    {
        // Store the initial position of the object
        initialPosition = transform.position;
    }

    void Update()
    {
        // Faster rotation
        transform.Rotate(new Vector3(15, 30, 45) * rotationSpeedMultiplier * Time.deltaTime);

        // Vertical movement (sinusoidal up and down)
        float newY = initialPosition.y + Mathf.Sin(Time.time * verticalMovementSpeed) * verticalMovementAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
