using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;


namespace Zetcil
{
    public class MobileCameraRotate : MonoBehaviour
    {
        [Space(10)]
        public bool isEnabled;

        [Header("Camera Settings")]
        public Camera MainCamera;

        [Header("Additional Settings")]
        public bool usingMouseSimulation;
        public float RotateSpeed;

        [Header("Input Status")]
        [ReadOnly] public Vector2 BeginTouch;

        Vector3 FirstPoint;
        Vector3 SecondPoint;
        float xAngle;
        float yAngle;
        float xAngleTemp;
        float yAngleTemp;

        // Use this for initialization
        void Start()
        {
            xAngle = 0;
            yAngle = 0;
            MainCamera.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0);
        }

        // Update is called once per frame
        void Update()
        {
            if (isEnabled)
            {
                IsCameraRotate();
            }
        }

        public bool IsCameraRotate()
        {
            bool result = false;

            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    FirstPoint = Input.GetTouch(0).position;
                    xAngleTemp = xAngle;
                    yAngleTemp = yAngle;
                }
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    SecondPoint = Input.GetTouch(0).position;
                    xAngle = xAngleTemp + (SecondPoint.x - FirstPoint.x) * 180 / Screen.width;
                    yAngle = yAngleTemp + (SecondPoint.y - FirstPoint.y) * 90 / Screen.height;
                    MainCamera.transform.rotation = Quaternion.Euler(yAngle * RotateSpeed, xAngle * RotateSpeed, 0.0f);
                    result = true;
                }
            }
            if (Input.GetKeyDown(KeyCode.Mouse0) && usingMouseSimulation) //-- Mouse Gesture
            {
                FirstPoint = Input.mousePosition;
                xAngleTemp = xAngle;
                yAngleTemp = yAngle;
            }
            else if (Input.GetKey(KeyCode.Mouse0) && usingMouseSimulation) //-- Mouse Gesture
            {
                SecondPoint = Input.mousePosition;
                xAngle = xAngleTemp + (SecondPoint.x - FirstPoint.x) * 180 / Screen.width;
                yAngle = yAngleTemp + (SecondPoint.y - FirstPoint.y) * 90 / Screen.height;
                MainCamera.transform.rotation = Quaternion.Euler(yAngle * RotateSpeed, xAngle * RotateSpeed, 0.0f);
                result = true;
            }

            return result;
        }
    }
}
