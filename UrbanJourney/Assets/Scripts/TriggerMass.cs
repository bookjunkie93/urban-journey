using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerMass : MonoBehaviour
{
    public UnityEvent onTrigger;
    public UnityEvent onTriggerExit;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            onTrigger.Invoke();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        onTriggerExit.Invoke();   
    }
}
