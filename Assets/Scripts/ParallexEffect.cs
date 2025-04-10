using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallexEffect : MonoBehaviour
{
    public float parallaxMultiplier = 0.5f; // Adjust for different layers
    private Transform cam;
    private Vector3 lastCamPos;

    void Start()
    {
        cam = Camera.main.transform;
        lastCamPos = cam.position;
    }

    void Update()
    {
        Vector3 deltaMovement = cam.position - lastCamPos;
        transform.position += new Vector3(deltaMovement.x * parallaxMultiplier, 0);
        lastCamPos = cam.position;
    }
}
