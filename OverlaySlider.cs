using System;
using UnityEngine;
using UnityEngine.UI;

namespace OverlaySlider
{
    public class OverlaySlider : MonoBehaviour
    {
        public GameObject rayFocus;
        public GameObject overlay;
        public Slider slider;
        public float maxSlideValue, minSlideValue;
        private Camera mainCam;
        
        private void Start()
        {
            mainCam = Camera.main;
        }

        void Update()
        {
            var detectionPlane = new Plane(Vector3.back, overlay.transform.position.z);
            var rayOrigin = mainCam.transform.position;
            var rayDirection = Vector3.Normalize(rayFocus.transform.position - rayOrigin);
            
            var ray = new Ray(rayOrigin, rayDirection);
            Debug.DrawRay(rayOrigin, rayDirection * 1000.0f, Color.magenta);
            
            if (detectionPlane.Raycast(ray, out var enter))
            {
                overlay.transform.rotation = mainCam.transform.rotation;
             
                var hitPoint = ray.GetPoint(enter);
                var currentPosition = overlay.transform.position;
                var newPosition = new Vector3(hitPoint.x, mainCam.transform.position.y, mainCam.transform.position.z + 2);
                overlay.transform.position = newPosition;
                overlay.transform.LookAt(rayOrigin);
            }
        }

        public void checkValue()
        {
            if (slider.value > maxSlideValue)
            {
                slider.value = maxSlideValue;
            } 
            else if (slider.value < minSlideValue)
            {
                slider.value = minSlideValue;
            }
        }
    }
}