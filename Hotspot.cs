using System;
using Controllers;
using Gaze;
using PixoEvent;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hotspot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IGazeable
{
    public Billboard billboard;
    //public Component outline;
    public float gazeClickDelay;

    private float _gazeElapsed = 0;
    private bool _activated = false;
    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        LookTowardsCamera();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Activate();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _gazeElapsed = 0;
        SetOutlineColor(new Color(1, 0, 0));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetOutlineColor(new Color(1, 1, 1));
    }

    public void OnGazeUpdate()
    {
        _gazeElapsed += Time.deltaTime;
        if (_gazeElapsed > gazeClickDelay)
            Activate();
    }
    
    private void Activate()
    {
        if (_activated == false)
        {
            EventController.Publish(new EventPayload
            {
                name = "HotspotActivated", target = billboard
            });
            
            _activated = true;
        }
    }
    
    private void LookTowardsCamera()
    {
        var thisBillboard = transform;
        var camDirection = mainCam.transform.forward;
        Quaternion newRotation = Quaternion.LookRotation(camDirection, thisBillboard.transform.up);

        transform.rotation = newRotation;
    }

    public void OnGazeEnter()
    {
        SetOutlineColor(new Color(1, 0, 0));
    }

    public void OnGazeExit()
    {
        _gazeElapsed = 0;
        _activated = false;
        SetOutlineColor(new Color(1, 1, 1));
    }

    private void SetOutlineColor(Color color)
    {
        //var meshRenderer = outline.GetComponent<MeshRenderer>();
        //meshRenderer.material.color = color;
    }
}
