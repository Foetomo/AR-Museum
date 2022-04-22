using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;

namespace Zetcil
{
    public class MobileCameraPan : MonoBehaviour
    {
        [Space(10)]
        public bool isEnabled;

        [Header("Camera Settings")]
        public Camera MainCamera;

        [Header("Additional Settings")]
        public bool usingMouseSimulation;
        public float PanSpeed;

        [Header("Input Status")]
        [ReadOnly] public Vector3 BeginTouch;

        // Update is called once per frame
        void Update()
        {
            if (isEnabled)
            {
                IsCameraPan();
            }
        }

        public bool IsCameraPan()
        {
            bool result = false;
            if (Input.touchCount > 0) //-- Mobile Gesture
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch fingerTouch = Input.GetTouch(i);

                    if (fingerTouch.phase == TouchPhase.Began)
                    {
                        BeginTouch.x = fingerTouch.position.x;
                        BeginTouch.y = fingerTouch.position.y;
                        BeginTouch = GetWorldPosition();
                    }

                    if (fingerTouch.phase == TouchPhase.Moved)
                    {
                        Vector3 direction = BeginTouch - GetWorldPosition();
                        MainCamera.transform.position += direction * PanSpeed;
                        result = true;
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) && usingMouseSimulation) //-- Mouse Gesture
            {
                BeginTouch.x = Input.mousePosition.x;
                BeginTouch.y = Input.mousePosition.y;
                BeginTouch = GetWorldPosition();
            }
            else if (Input.GetKey(KeyCode.Mouse0) && usingMouseSimulation) //-- Mouse Gesture
            {
                Vector3 direction = BeginTouch - GetWorldPosition();
                MainCamera.transform.position += direction * PanSpeed;
                result = true;
            }
            return result;
        }

        private Vector3 GetWorldPosition()
        {
            Ray mousePos = MainCamera.ScreenPointToRay(Input.mousePosition);
            Plane ground = new Plane(Vector3.forward, new Vector3(0, 0, 0));
            float distance;
            ground.Raycast(mousePos, out distance);
            return mousePos.GetPoint(distance);
        }

    }
}