using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWarper : MonoBehaviour
{
    [SerializeField] Transform resetPoint;
    [SerializeField] float resetSpeed;
    [SerializeField] float resetThreshold = 0.5f;
    GameObject objectToWarp;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            objectToWarp = collision.gameObject;
            TogglePlayerControl(false);
            objectToWarp.GetComponent<Mover>().Move(0, false);
        }
    }

    private void TogglePlayerControl(bool isActive)
    {
        objectToWarp.GetComponent<PlayerController>().SetInput(!isActive);
        Collider2D[] colliders = objectToWarp.GetComponents<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = isActive;
        }
        Rigidbody2D rigidbody = objectToWarp.GetComponent<Rigidbody2D>();
        rigidbody.isKinematic = !isActive;
        rigidbody.velocity = Vector2.zero;
    }

    private void FixedUpdate()
    {
        if(objectToWarp != null)
        {
            Vector2 pos = objectToWarp.transform.position;
            if(Vector2.Distance(pos, resetPoint.position) <= resetThreshold)
            {
                TogglePlayerControl(true);
                objectToWarp = null;
            }
            else
            {
                objectToWarp.transform.position = Vector2.MoveTowards(pos, resetPoint.position, resetSpeed* Time.deltaTime);
            }
        }
    }
}
