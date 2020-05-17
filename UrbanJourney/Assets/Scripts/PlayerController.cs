using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : EntityController
{
    [SerializeField] float m_runSpeed = 5f;
    [SerializeField] float grabDistance = 0.316f;
    [SerializeField] KeyCode grab;
    [SerializeField]SpriteRenderer sprite;
    [SerializeField] Transform grabSlot;
    Grabbable grabbedObject;
    GrabCommand grabCommand;
    Mover mover;
    private float horizontalMove;
    bool jump;
    bool inputSuspended;

    // Start is called before the first frame update
    void Start()
    {
        mover =GetComponent<Mover>();
        grabCommand = new GrabCommand();
        
    }

    private void OnDrawGizmosSelected()
    {
        if(Input.GetKeyDown(grab))
        {
            Gizmos.color = Color.red;
            Vector3 direction = grabSlot.TransformDirection(Vector3.right) * grabDistance;
            Gizmos.DrawRay(transform.position, direction);
        }
    }

    public void SetInput(bool isSuspended)
    {
        inputSuspended = isSuspended;
    }
    public GameObject FindGrabbableObject()
    {
        RaycastHit2D hit = Physics2D.Raycast(grabSlot.position, grabSlot.right, grabDistance);
        if (hit.collider != null)
        {
            if (hit.transform.GetComponent<Grabbable>() != null)
            {
                return hit.transform.gameObject;
            }
        }
        return null;
    }

    public void Grab(GameObject obj)
    {
        if(obj == null) return;
        if(obj.GetComponent<Grabbable>() != null)
        {
            grabbedObject = obj.GetComponent<Grabbable>();
            grabbedObject.Grab(grabSlot);
        }
    }

    public void Drop ()
    {
        if(grabbedObject != null)
        {
            grabbedObject.Drop();
            grabbedObject = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inputSuspended) { return; }
        horizontalMove = Input.GetAxisRaw("Horizontal") * m_runSpeed;
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (Input.GetButtonUp("Jump"))
        {
            mover.StopJump();
        }

        if (Input.GetKeyDown(grab))
        {
            if(FindGrabbableObject() != null)
            {
                Debug.Log("found grabbable object");
                grabCommand.Execute(this, FindGrabbableObject());
            }
        }
        

        if(Input.GetKeyUp(grab))
        {
            grabCommand.UnExecute(this);
        }
    }

    private void FixedUpdate()
    {
        
        mover.Move(horizontalMove * Time.deltaTime, jump);
        jump = false;
        
    }
}
