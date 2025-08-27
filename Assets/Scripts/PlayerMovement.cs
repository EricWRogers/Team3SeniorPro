using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody rb;

    [Header("Mouse Settings")]
    public float mouseSensitivity = 1f;
    private float xRotation = 0f;
    private float yRotation = 0f;
    private float zRotation = 0f;
    public bool canMove = true;

    [Header("Player Settings")]
    public GameObject player;
    public float speed = 5f;
    public float maxSpeed = 10f; // maximum speed the player can reach
    public float friction = 0.05f; // how fast you slow down when not pressing keys 
    public float pitchSpeed = 50f;

    [Header("Teather Settings")]
    [SerializeField] private Transform teatherAnchor;
    [SerializeField] private float teatherMaxDistance = 50f;
    [SerializeField] private float teatherMinDistance = 40f; // Minimum distance before stopping
    [SerializeField] private float teatherDampening = 0.5f; // Slows down as you get closer
    private Vector3 teatherTarget; // Store the position we're tethering to
    private bool usingTeather = true; 
    




    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = gameObject;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            usingTeather = !usingTeather;
            canMove = !canMove;
        }

        if (canMove)
        {

            rb.isKinematic = false;
            if (Input.GetKey(KeyCode.W)) rb.AddForce(transform.forward * speed);
            if (Input.GetKey(KeyCode.S)) rb.AddForce(-transform.forward * speed);
            if (Input.GetKey(KeyCode.A)) rb.AddForce(-transform.right * speed);
            if (Input.GetKey(KeyCode.D)) rb.AddForce(transform.right * speed);
            if (Input.GetKey(KeyCode.Space)) rb.AddForce(transform.up * speed);
            if (Input.GetKey(KeyCode.LeftControl)) rb.AddForce(-transform.up * speed);

            if (Input.GetKey(KeyCode.V))
            {
                //zRotation += pitchSpeed * Time.deltaTime;
                transform.Rotate(Vector3.forward * pitchSpeed * Time.deltaTime, Space.Self);
            }

            if (Input.GetKey(KeyCode.B))
            {
                transform.Rotate(-Vector3.forward * pitchSpeed * Time.deltaTime, Space.Self);
                //zRotation -= pitchSpeed * Time.deltaTime;
            }


            if (usingTeather)
        {        
                    Vector3 directionToAnchor = teatherAnchor.position - transform.position;
                    float distanceToAnchor = directionToAnchor.magnitude;

                    if (distanceToAnchor > teatherMinDistance)
                    {
                        float dampingFactor = Mathf.Clamp01((distanceToAnchor - teatherMinDistance) / (teatherMaxDistance - teatherMinDistance));
                        dampingFactor = 1f - Mathf.Pow(1f - dampingFactor, teatherDampening); // Apply exponential damping

                        // Apply force toward the tether anchor
                        Vector3 tetherForce = directionToAnchor.normalized * speed * dampingFactor;
                        rb.AddForce(tetherForce);

                        // Clamp position to max tether distance
                        if (distanceToAnchor > teatherMaxDistance)
                        {
                            Vector3 clampedPosition = teatherAnchor.position - directionToAnchor.normalized * teatherMaxDistance;
                            transform.position = clampedPosition;
                        }
                    }
                
            }

 


            if (rb.linearVelocity.magnitude > 0.001f)
            {
                rb.linearVelocity -= rb.linearVelocity * friction; // Apply Friction
                rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxSpeed); // Limit speed
            }

            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            yRotation += mouseX;
            xRotation -= mouseY;
            //xRotation = Mathf.Clamp(xRotation, -90f, 90f); clamps vertical look
            transform.Rotate(Vector3.up, mouseX, Space.Self);
            transform.Rotate(Vector3.right, -mouseY, Space.Self);


            //transform.localRotation = Quaternion.Euler(xRotation, yRotation, zRotation);


        }
        else
        {

            rb.isKinematic = true; // Disable physics when cursor is not locked
        }
    }
}
