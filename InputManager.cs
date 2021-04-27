using UnityEngine;

namespace Controllers
{
    public class InputManager
    {
        private static Camera _camera;
        
        static InputManager()
        {
            _camera = Camera.main;
        }
        
        public static bool TryGetTouchPosition(out Vector2 touchPosition)
        {
#if UNITY_EDITOR
            if (Input.GetMouseButton(0))
            {
                var mousePosition = Input.mousePosition;
                touchPosition = new Vector2(mousePosition.x, mousePosition.y);
                return true;
            }
#else
            if (Input.touchCount > 0)
            {
                touchPosition = Input.GetTouch(0).position;
                return true;
            }
#endif

            touchPosition = default;
            return false;
        }


        public static bool IsHitByRayFromCamera(Vector2 touchPosition, out RaycastHit hit)
        {
            hit = default;
            if (_camera == null)
                return false;

            var inputEndWorldPositionNear =
                _camera.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, _camera.nearClipPlane));
            var inputEndWorldPositionFar =
                _camera.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, _camera.farClipPlane));
            Debug.DrawRay(inputEndWorldPositionNear, inputEndWorldPositionFar - inputEndWorldPositionNear);

            return Physics.Raycast(inputEndWorldPositionNear, inputEndWorldPositionFar - inputEndWorldPositionNear,
                out hit);
        }
    }
}