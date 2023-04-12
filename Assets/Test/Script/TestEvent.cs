using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvent : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        EventCaller eventCaller = FindObjectOfType<EventCaller>();
        eventCaller.SpawnObjectListener += RunEvent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void RunEvent(object sender,EventArgs e)
    {
        Debug.Log("Nghe");
    }
}
