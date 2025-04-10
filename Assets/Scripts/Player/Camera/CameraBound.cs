using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBound : MonoBehaviour
{
    public Transform player;  // Assign the Player in Inspector
    public BoxCollider2D cameraBounds; // Assign a BoxCollider2D defining the vertical range
    public float smoothSpeed = 5f; // Smooth movement speed

    private Camera cam;
    private float camHeight;
    private Vector2 minBounds;
    private Vector2 maxBounds;

    void Start()
    {
        cam = Camera.main;
        camHeight = cam.orthographicSize;
        float camWidth = cam.aspect * camHeight;

        if (cameraBounds != null)
        {
            minBounds = cameraBounds.bounds.min;
            maxBounds = cameraBounds.bounds.max;
        }
    }

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 targetPosition = player.position;

        // Restrict only up-down movement
        float freeX = targetPosition.x; // Allow full movement in X-axis
        float boundY = Mathf.Clamp(targetPosition.y, minBounds.y + camHeight, maxBounds.y - camHeight);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, new Vector3(freeX, boundY, transform.position.z), smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }

}
