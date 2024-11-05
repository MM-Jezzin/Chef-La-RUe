using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] public Transform player; // Item to use as focus for camera
    [SerializeField] public Vector3 v3_offsetDistance; // The quantity of offset used
    [SerializeField] public float f_smoothFactor; // The amount that acts as smoothing
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void FixedUpdate()
    {
        Vector3 v3_targetPos = player.position + v3_offsetDistance; // Calculates target position - player position + offset
        Vector3 v3_smoothPos = Vector3.Slerp(transform.position, v3_targetPos, f_smoothFactor * Time.deltaTime); // Moves smoothly cameras current position to the target position (player + offset), based on the smooth factor.
        transform.position = v3_targetPos; // sets cameras position to the target position
    }
}
