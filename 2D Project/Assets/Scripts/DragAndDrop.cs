using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndDrop : MonoBehaviour
{
    private bool isDragging = false;

    private Vector3 offset;

    private Camera mainCamera;

    private Collider2D objectCollider;
    private void Awake()
    {
        mainCamera = Camera.main;

        objectCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (Pointer.current == null) return;

        Vector2 screenPosition = Pointer.current.position.ReadValue();

        if (Pointer.current.press.wasPressedThisFrame)
        {
            Vector3 worldPoint = GetWorldPosition(screenPosition);

            if(objectCollider != null && objectCollider.OverlapPoint(worldPoint))
            {
                offset = transform.position - worldPoint;
                isDragging = true;
            }
        }

        if (isDragging)
        {
            Vector3 currentWorldPosition = GetWorldPosition(screenPosition);

            transform.position = currentWorldPosition + offset;
        }

        if(Pointer.current.press.wasReleasedThisFrame)
        {
            isDragging = false;
        }
    }

    private Vector3 GetWorldPosition(Vector2 screenPosition)
    {
        Vector3 screenWithZ = new Vector3(screenPosition.x,screenPosition.y, -mainCamera.transform.position.z);
        return mainCamera.ScreenToWorldPoint(screenWithZ);
    }
}
