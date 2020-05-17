using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Grabbable: MonoBehaviour
{
    [SerializeField] float xOffset= 0;
    [SerializeField] float yOffset= 0.5f;
    public UnityEvent onGrab;
    public UnityEvent onRelease;
    Rigidbody2D m_rigidbody;
    Transform firstParent;
    RigidbodyConstraints2D grabbedConstraints = RigidbodyConstraints2D.FreezeRotation;
    RigidbodyConstraints2D freeConstraints = (RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation);
    bool isGrabbed;
    float gravityScale;
    // Use this for initialization

    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        firstParent = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
       if(isGrabbed && (transform.localPosition.x != xOffset || transform.localPosition.y != yOffset))
        {
            transform.localPosition = new Vector3(xOffset, yOffset);
        }
    }

    public void Grab (Transform grabber)
    {
        isGrabbed = true;
        transform.SetParent(grabber);
        transform.localPosition = new Vector3(xOffset, yOffset);
        gravityScale = m_rigidbody.gravityScale;
        m_rigidbody.gravityScale = 0;
        onGrab.Invoke();
    }

    public void Drop()
    {
        isGrabbed = false;
        transform.SetParent(firstParent);
        transform.position -= new Vector3(0f,0f,transform.position.z); //make sure the position is always 0
        m_rigidbody.gravityScale = gravityScale;
        onRelease.Invoke();
    }


}
