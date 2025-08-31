using SuperPupSystems.Helper;
using UnityEngine;

public class GrappleHook : Bullet
{
    [Header("Grapple Hook Settings")]
    public Vector3 hookPos;
    private Transform m_playerPos;
    public GameObject m_player;
    public float maxGrappleDistance;
    public float grapplePullSpeed;
    public float rectractSpeed;
    private bool m_retract = false;
    private bool m_isHooked = false;

    void Start()
    {
        m_retract = false;
        m_isHooked = false;
        m_player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        hookPos = transform.position;
        m_playerPos = m_player.transform;
        if (Vector3.Distance(hookPos, m_playerPos.position) > maxGrappleDistance && !m_retract)
        {
            m_retract = true;
        }
        if (m_retract)
        {
            Retract();
            if (m_isHooked)
            {
                speed = rectractSpeed;
                GetComponent<Rigidbody>().isKinematic = false;
                m_player.GetComponent<SpringJoint>().spring = 0;
            }
            
        }
        if (Vector3.Distance(hookPos, m_playerPos.position) < 1 && (m_retract || m_isHooked))
        {
            m_player.GetComponent<SpringJoint>().spring = 0;
            m_isHooked = false;
            Destroy(this.gameObject);
        }
        if (m_isHooked && !m_retract)
        {
            PullPlayer();
        }
    }
    public void PullPlayer()
    {
        Debug.Log("pull Player");
        m_player.GetComponent<SpringJoint>().connectedAnchor = hookPos;
        m_player.GetComponent<SpringJoint>().spring = grapplePullSpeed;
    }
    public void Hooked()
    {
        if (!m_retract)
        {
            Debug.Log("Hooked");
            m_isHooked = true;
            speed = 0;
            GetComponent<Rigidbody>().isKinematic = true;
        }
        
    }
    public void Retract()
    {
        m_retract = true;
        transform.LookAt(m_playerPos);
    }
}
