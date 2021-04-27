using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PixoEvent;
using UnityEngine;

public class Billboard : MonoBehaviour, IEventHandler
{
    private Camera mainCam;
    public float lookSpeed = (float) 3;
    private bool isActive = false;
    
    void Start()
    {
        GetComponentsInChildren<Renderer>().ToList().ForEach(r => r.enabled = false);
        EventController.Subscribe(this);
        mainCam = Camera.main;
    }

    private void Update()
    {
        if(isActive == false) return;
        lookTowardsCamera();
    }

    private void lookTowardsCamera()
    {
        var thisBillboard = transform;
        var camDirection = mainCam.transform.forward;
        
        Quaternion newRotation = Quaternion.LookRotation(camDirection, thisBillboard.transform.up);

        transform.rotation = newRotation;
    }

    public void OnEvent(EventPayload p)
    {
        Debug.Log($"{this.name} received {p.name} for {p.target}");
        
        if (p.target == this)
        {
            GetComponentsInChildren<Renderer>().ToList().ForEach(r => r.enabled = true);
            isActive = true;
        }
        
        if (p.target != this)
        {
            GetComponentsInChildren<Renderer>().ToList().ForEach(r => r.enabled = false);
            isActive = false;
        }
    }
}