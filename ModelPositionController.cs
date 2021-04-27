
using UnityEngine;
using UnityEngine.XR.ARFoundation;


[RequireComponent(typeof(ARTrackedImageManager))]
public class ModelPositionController : MonoBehaviour
{
    public Vector3 desiredDozerPositionRelativeToAnchor = new Vector3(0, 0, 2);
    public GameObject dozer;
    
    ARTrackedImageManager _trackedImageManager;
    
    private ARTrackedImage _worldAnchor;

    void Awake()
    {
        _trackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        _trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        _trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }
    
    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        if (eventArgs.added.Count > 0) Debug.Log($"Tracker added {eventArgs.added.Count} images");
        if (eventArgs.removed.Count > 0) Debug.Log($"Tracker removed {eventArgs.removed.Count} images");
        if (eventArgs.updated.Count > 0) Debug.Log($"Tracker updated {eventArgs.updated.Count} images");
        
        foreach (var trackedImage in eventArgs.added)
        {
                RepositionDozer(trackedImage);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
                RepositionDozer(trackedImage);
        }
    }

    private void RepositionDozer(ARTrackedImage trackedImage)
    {
        var origPosition = dozer.transform.position;
        var imageTransform = trackedImage.transform;
        dozer.transform.position = imageTransform.position + desiredDozerPositionRelativeToAnchor;
        dozer.transform.rotation = imageTransform.rotation;
        Debug.Log($"Moved dozer from {origPosition} to {dozer.transform.position} relative to {trackedImage.name} at {trackedImage.transform.position} with rotation {imageTransform.rotation}");
    }
}

