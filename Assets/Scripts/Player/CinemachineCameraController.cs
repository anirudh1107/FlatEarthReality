using UnityEngine;

public class CinemachineCameraController : MonoBehaviour
{
    [Header("References")]
    [Tooltip("The target object that the virtual camera follows.")]
    public Transform cameraFollowTarget;

    [Header("Movement Settings")]
    [Tooltip("Speed for keyboard and edge scrolling movement.")]
    public float moveSpeed = 10f;
    [Tooltip("Multiplier for movement when dragging with the right mouse button.")]
    public float dragSpeed = 0.1f;
    [Tooltip("Distance in pixels from the screen edge to start moving the camera.")]
    public float edgeThreshold = 10f;

    [Header("Horizontal Range")]
    public float minX = -50f;
    public float maxX = 50f;

    [Header("Vertical Range")]
    public float minY = -20f;
    public float maxY = 20f;

    // To track the last mouse position during right mouse drag
    private Vector3 lastMousePosition;

    void Update()
    {
        if (cameraFollowTarget == null)
        {
            Debug.LogWarning("Camera follow target not assigned!");
            return;
        }

        // --- Option 1: Keyboard Input (Arrow keys or WASD) ---
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 keyboardMove = new Vector3(horizontalInput, verticalInput, 0f);
        cameraFollowTarget.Translate(keyboardMove * moveSpeed * Time.deltaTime, Space.World);

        // --- Option 2: Mouse at Screen Edge Scrolling ---
        Vector3 mousePos = Input.mousePosition;
        Vector3 edgeMove = Vector3.zero;
        if (mousePos.x < edgeThreshold)
            edgeMove.x = -1;
        else if (mousePos.x > Screen.width - edgeThreshold)
            edgeMove.x = 1;
        if (mousePos.y < edgeThreshold)
            edgeMove.y = -1;
        else if (mousePos.y > Screen.height - edgeThreshold)
            edgeMove.y = 1;
        cameraFollowTarget.Translate(edgeMove.normalized * moveSpeed * Time.deltaTime, Space.World);

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;

        // --- Option 3: Right Mouse Button Drag ---
        if (Input.GetMouseButtonDown(1))
        {
            lastMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(1))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            // Move the follow target opposite to the mouse movement for a natural drag feel.
            cameraFollowTarget.Translate(-delta * dragSpeed * Time.deltaTime, Space.World);
            lastMousePosition = Input.mousePosition;
        }
    }
}
