using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody rb;

    [Header("Mouse Settings")]
    public float mouseSensitivity = 1f;
    private float xRotation = 0f;
    private float yRotation = 0f;
    public bool lockedCursor = true;

    [Header("Player Settings")]
    public GameObject player;
    public float speed = 5f;
    public float friction = 0.05f; // how fast you slow down when not pressing keys



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

        if(Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.forward * speed);
        }
        if(Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-transform.forward * speed);
        }
        if(Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-transform.right * speed);
        }
        if(Input.GetKey(KeyCode.D))
        {
            rb.AddForce(transform.right * speed);
        }
        if(Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(transform.up * speed);
        }
        if(Input.GetKey(KeyCode.LeftControl))
        {
            rb.AddForce(-transform.up * speed);
        }



        if(rb.linearVelocity.magnitude > 0.001f)
        {
            rb.linearVelocity -= rb.linearVelocity * friction; // Apply Friction

        }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity; 
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

        player.transform.Rotate(Vector3.up * mouseX);

    }
}
