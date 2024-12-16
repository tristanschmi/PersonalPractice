using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;           // The target object to follow (e.g., the player)
    public Vector3 offset = new Vector3(0, 2, -5); // Default offset from the target position
    public float rotationSpeed = 5.0f; // Speed of rotation with the mouse

    private float currentRotationX = 0.0f;
    private float currentRotationY = 0.0f;

    void Start()
    {
        if (target == null)
        {
            Debug.LogError("Target not set for ThirdPersonCamera.");
            return;
        }

        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the game window
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

        // Rotate based on mouse input
        currentRotationX += mouseX;
        currentRotationY -= mouseY;
        
        // Clamp the vertical rotation to prevent camera flipping
        currentRotationY = Mathf.Clamp(currentRotationY, -35f, 60f); 

        // Convert rotation into a Quaternion
        Quaternion rotation = Quaternion.Euler(currentRotationY, currentRotationX, 0);

        // Calculate the final position and apply the rotation
        Vector3 desiredPosition = target.position + rotation * offset;
        transform.position = desiredPosition;
        transform.LookAt(target.position);
    }
}
