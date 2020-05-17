using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAI : EntityController
{
    public double terminusThreshold = 0.05;
    public Transform pointA;
    public Transform pointB;
    [SerializeField] float moveSpeed;
    [SerializeField] bool isVertical;
    Rigidbody2D m_rigidbody;
    public Transform currentDestination;
    Timescale timer;

    Transform oldPlayerParent;
    Vector3 offset;

    private void Awake()
    {
        timer = FindObjectOfType<Timescale>();
        m_rigidbody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        currentDestination = pointA;
        if(isVertical)
        {
            m_rigidbody.constraints = (RigidbodyConstraints2D.FreezePositionX|RigidbodyConstraints2D.FreezeRotation);
        }
        else
        {
            m_rigidbody.constraints = (RigidbodyConstraints2D.FreezePositionY|RigidbodyConstraints2D.FreezeRotation);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dist = Vector2.Distance(transform.position, currentDestination.position);
        if (Mathf.Abs(dist) <= terminusThreshold)
        {
            switchDirection();
        }
        Vector2 direction = Vector2.MoveTowards(transform.position, currentDestination.position, moveSpeed * (Time.deltaTime* timer.GetScale()));
        m_rigidbody.MovePosition(direction);           
        

    }   

    private void LateUpdate()
    {
        //if(target != null)
        //{
        //    target.transform.position = transform.position + offset;
        //}
    }
    private void switchDirection()
    {
        if (currentDestination == pointA)
        {
            currentDestination = pointB;
        }
        else
        {
            currentDestination = pointA;
        }
    }
}
