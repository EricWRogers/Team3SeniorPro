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
    public bool insideShip;

    [Header("Teather Settings")]
    private LineRenderer teatherLine;
    [SerializeField] private Material teatherMaterial;
    [SerializeField] private float teatherWidth = 0.2f;
    [SerializeField] private Transform teatherAnchor;
    [SerializeField] private float teatherMaxDistance = 50f;
    [SerializeField] private float teatherMinDistance = 40f; // Minimum distance before stopping
    [SerializeField] private float teatherDampening = 0.5f; // Slows down as you get closer
    private Vector3 teatherTarget; // Store the position we're tethering to
    private bool usingTeather = true;

    [Header("Grav Boots")]
    public float gravBootsForce = 10f;
    public float minGroundDistance = 2f;
    public bool usingGravBoots = false;
    private Transform gravityObject;
    private Quaternion surfaceAlignedRotation;
    private float currentYaw = 0f;

    [Header("Grapple Hook Settings")]
    public Transform grappleFirePoint;
    public float hookSpeed;
    public float cooldown;
    public float m_currentCooldown;
    public GameObject hookPrefab;
    private GameObject m_hook;
    private bool m_hookedLaunched;

    [Header("Ship vars")]
    public Transform ship;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = gameObject;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        teatherLine = gameObject.GetComponent<LineRenderer>() == null ? gameObject.AddComponent<LineRenderer>() : gameObject.GetComponent<LineRenderer>();
        teatherLine.positionCount = 2;
        teatherLine.material = teatherMaterial;
        // Update is called once per frame

    }
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
            if (Input.GetKeyDown(KeyCode.C))
            {
                usingGravBoots = !usingGravBoots;
                if (usingGravBoots) CheckGravBoots();

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
            if (usingTeather)
            {
                Vector3 directionToAnchor = teatherAnchor.position - transform.position;
                float distanceToAnchor = directionToAnchor.magnitude;

                //Teather line rednering
                teatherLine.enabled = true;
                teatherLine.startWidth = teatherWidth;
                teatherLine.endWidth = teatherWidth;
                teatherLine.SetPosition(0, transform.position);
                teatherLine.SetPosition(1, teatherAnchor.position);

                // Check if we're beyond the minimum distance
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

            if (!usingGravBoots && !insideShip)
            {
                // Normal space rotation
                transform.Rotate(Vector3.up, mouseX, Space.Self);
                transform.Rotate(Vector3.right, -mouseY, Space.Self);
            }
            if(usingGravBoots && !insideShip)
            {
                Vector3 gravityDirection = (transform.position - gravityObject.position).normalized;
                rb.AddForce(gravityDirection * -gravBootsForce, ForceMode.Acceleration);

                // Calculate base rotation aligned with surface
                surfaceAlignedRotation = Quaternion.FromToRotation(Vector3.up, gravityDirection) * Quaternion.Euler(0, currentYaw, 0);

                // Update yaw based on mouse input
                currentYaw += mouseX;

                // Calculate pitch rotation
                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, -70f, 70f);

                // Combine surface alignment with pitch and yaw
                Quaternion finalRotation = surfaceAlignedRotation * Quaternion.Euler(xRotation, 0, 0);

                // Apply smooth rotation
                transform.rotation = Quaternion.Slerp(transform.rotation, finalRotation, Time.deltaTime *10);


            }

            if (insideShip)
            {
                surfaceAlignedRotation = Quaternion.FromToRotation(transform.up, ship.up) * Quaternion.Euler(0, currentYaw, 0);
                rb.AddForce(ship.up * -gravBootsForce, ForceMode.Acceleration);

                currentYaw += mouseY;
                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, -70f, 70);
                Quaternion finalRotation = surfaceAlignedRotation * Quaternion.Euler(xRotation, 0, 0);

                transform.rotation = Quaternion.Slerp(transform.rotation, finalRotation, Time.deltaTime * 10);

            }

        }
        else
        {
            rb.isKinematic = true; // Disable physics when cursor is not locked
        }

        if (m_currentCooldown > 0)
        {
            m_currentCooldown -= Time.deltaTime;
        }
        else
        {
            m_hookedLaunched = false;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(m_hookedLaunched && m_hook != null) CancelGrapple();

            if (m_currentCooldown <= 0)
            {
                m_currentCooldown = cooldown;
                if (!m_hookedLaunched)
                {
                    ShootGrapple();
                    m_hookedLaunched = true;
                }
            }
            
        }
    }


    void CheckGravBoots()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 0.5f, -transform.up, out hit, minGroundDistance))
        {
            gravityObject = hit.transform.gameObject.transform;


        }
        else
        {
            usingGravBoots = false;
        }
    }
    public void ShootGrapple()
    {
        Transform ray = Camera.main.transform;
        if (Physics.Raycast(new Ray(ray.position, ray.forward), out RaycastHit hit, 30))
            grappleFirePoint.transform.LookAt(hit.point);
        else
            grappleFirePoint.transform.LookAt(ray.position + ray.forward * 30);
        m_hook = Instantiate(hookPrefab, grappleFirePoint.position, grappleFirePoint.rotation);
        Rigidbody rb = m_hook.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = grappleFirePoint.forward * hookSpeed;
            rb.useGravity = false;
        }
    }
    public void CancelGrapple()
    {
        m_currentCooldown = 0;
        m_hook.GetComponent<GrappleHook>().Retract();
    }
}
    
