using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EventCaller : MonoBehaviour
{
    // Start is called before the first frame update

    //public UnityEvent SpawnObjectListener;

    public TestEvent TestEvent;

    public event EventHandler SpawnObjectListener;
    void Start()
    {
       SpawnObjectListener += TestEvent.RunEvent;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SpawnObjectListener?.Invoke(this, EventArgs.Empty);
        }
    }
    
}
