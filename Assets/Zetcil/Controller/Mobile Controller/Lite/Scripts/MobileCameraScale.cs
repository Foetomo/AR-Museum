using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;


namespace Zetcil
{
    public class MobileCameraScale : MonoBehaviour
    {
        [Space(10)]
        public bool isEnabled;

        [Header("Camera Settings")]
        public Camera MainCamera;

        [Header("Additional Settings")]
        public bool usingMouseSimulation;
        public float perspectiveZoomSpeed = 0.5f;
        public float orthoZoomSpeed = 0.5f;
        public float floatSpeed = 0.1f;

        [Header("Input Status")]
        [ReadOnly] public Vector2 BeginTouch;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (isEnabled)
            {
                IsCameraScale();
            }
        }

        public bool IsCameraScale()
        {
            bool result = false;

            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch fingerTouch = Input.GetTouch(i);

                    if (fingerTouch.phase == TouchPhase.Began)
                    {
                        BeginTouch.x = fingerTouch.position.x;
                        BeginTouch.y = fingerTouch.position.y;
                    }

                    // If there are two touches on the device...
                    if (Input.touchCount == 2)
                    {
                        // Store both touches.
                        Touch touchZero = Input.GetTouch(0);
                        Touch touchOne = Input.GetTouch(1);

                        // Find the position in the previous frame of each touch.
                        Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                        Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                        // Find the magnitude of the vector (the distance) between the touches in each frame.
                        float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                        float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                        // Find the difference in the distances between each frame.
                        float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                        // If the camera is orthographic...
                        if (MainCamera.orthographic)
                        {
                            // ... change the orthographic size based on the change in distance between the touches.
                            MainCamera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

                            // Make sure the orthographic size never drops below zero.
                            MainCamera.orthographicSize = Mathf.Max(MainCamera.orthographicSize, 0.1f);
                        }
                        else
                        {
                            // Otherwise change the field of view based on the change in distance between the touches.
                            MainCamera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

                            // Clamp the field of view to make sure it's between 0 and 180.
                            MainCamera.fieldOfView = Mathf.Clamp(MainCamera.fieldOfView, 0.1f, 179.9f);
                        }

                        result = true;
                    }
                }
            }

            if (usingMouseSimulation) //-- Mouse Gesture
            {
                Vector3 pos = MainCamera.transform.position;
                pos.y += Input.mouseScrollDelta.y * floatSpeed;
                pos.x += Input.mouseScrollDelta.x * floatSpeed;
                MainCamera.transform.position = pos;
            }

            return result;
        }

    }
}
