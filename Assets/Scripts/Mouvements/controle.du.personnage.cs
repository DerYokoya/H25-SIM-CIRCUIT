using UnityEngine;

public class CubeController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of cube movement
    public float mouseSensitivity = 100f; // Sensitivity for camera rotation
    public Transform cameraTransform; // Reference to the camera

    private float xRotation = 0f; // Tracks vertical camera rotation

    void Start()
    {
        // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true; //Afficher la souris
    }

    void Update()
    {
        Cursor.visible = true; //Afficher la souris
        // Camera Rotation (Mouse Look)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Vertical rotation (up and down)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limit vertical rotation

        // Apply rotation to the camera
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Horizontal rotation (left and right) - Rotate the camera's parent (empty object)
        transform.Rotate(Vector3.up * mouseX);

        // Cube Movement (WASD) - Relative to camera direction
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calculate movement direction relative to the camera
        Vector3 move = (cameraTransform.right * moveX + cameraTransform.forward * moveZ).normalized;
        move.y = 0; // Ensure movement is only horizontal

        // Move the cube
        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);

        // Mouse Click (Raycast from center of the screen)
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Clicked on: " + hit.transform.name);
                // Add your click logic here
            }
        }
    }
}