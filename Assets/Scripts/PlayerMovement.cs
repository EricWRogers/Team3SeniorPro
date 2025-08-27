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
    [SerializeField] private float teatherPullSpeed = 10f;
    [SerializeField] private float teatherMinDistance = 1f; // Minimum distance before stopping
    [SerializeField] private float teatherDampening = 0.5f; // Slows down as you get closer
    private Vector3 teatherTarget; // Store the position we're tethering to
    private bool isReturning = false; // Track if we're returning to base
    private bool usingTeather = false; 
    




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
            if (Input.GetKey(KeyCode.W))
            {
                rb.AddForce(transform.forward * speed);
            }
            if (Input.GetKey(KeyCode.S))
            {
                rb.AddForce(-transform.forward * speed);
            }
            if (Input.GetKey(KeyCode.A))
            {
                rb.AddForce(-transform.right * speed);
            }
            if (Input.GetKey(KeyCode.D))
            {
                rb.AddForce(transform.right * speed);
            }
            if (Input.GetKey(KeyCode.Space))
            {
                rb.AddForce(transform.up * speed);
            }
            if (Input.GetKey(KeyCode.LeftControl))
            {
                rb.AddForce(-transform.up * speed);
            }


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
            if (usingTeather)
            {
                float distanceToTarget = Vector3.Distance(transform.position, teatherTarget);

                if (distanceToTarget > teatherMinDistance)
                {
                    Vector3 directionToAnchor = (teatherTarget - transform.position).normalized;

                    // Calculate pull force based on distance
                    float pullForce = teatherPullSpeed * (distanceToTarget / teatherMaxDistance);
                    pullForce = Mathf.Clamp(pullForce, 0, teatherPullSpeed);

                    // Apply dampening as we get closer
                    pullForce *= (1 - (teatherDampening * (1 - distanceToTarget / teatherMaxDistance)));

                    rb.AddForce(directionToAnchor * pullForce);

                    // Optionally slow down existing velocity
                    rb.linearVelocity *= 0.98f;
                }
                else
                {
                    // We've reached the target
                    usingTeather = false;
                    canMove = true;
                    isReturning = false;
                    rb.linearVelocity = Vector3.zero;
                }
                return;
            }

            rb.isKinematic = true; // Disable physics when cursor is not locked
        }
    }
}
