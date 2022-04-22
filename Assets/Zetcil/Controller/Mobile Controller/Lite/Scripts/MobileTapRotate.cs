using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;

namespace Zetcil
{

    public class MobileTapRotate : MonoBehaviour
    {
        public enum CRotationType { Horizontal, Vertical, AllDirection }
        public enum CObjectSelectionType { Everything, ByName, ByTag }

        [Space(10)] 
        public bool isEnabled;

        [Header("Raycast Settings")]
        public Camera MainCamera;
        public VarObject SelectedObject;

        [Header("Rotation Settings")]
        public CRotationType RotationType;

        [Header("Object Settings")]
        public CObjectSelectionType ObjectSelectionType;
        [Tag] public string ObjectTag;
        public string ObjectName;
        public float RotationSpeed = 3.0f;

        [Header("Additional Settings")]
        public bool usingMouseSimulation;

        [Header("Input Status Settings")]
        [ReadOnly] public Vector2 BeginTouch;

        int ObjectType = 0;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            IsTapRotate();
        }

        bool isValidSelection(GameObject targetSelection)
        {
            bool result = false;
            if (targetSelection != null)
            {
                if (ObjectSelectionType == CObjectSelectionType.ByName)
                {
                    if (targetSelection.name == ObjectName)
                    {
                        result = true;
                    }
                }
                else if (ObjectSelectionType == CObjectSelectionType.ByTag)
                {
                    if (targetSelection.tag == ObjectTag)
                    {
                        result = true;
                    }
                }
                else if (ObjectSelectionType == CObjectSelectionType.Everything)
                {
                    result = true;
                }
            }

            return result;
        }

        void IsTapRotate()
        {
            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch fingerTouch = Input.GetTouch(i);

                    if (fingerTouch.phase == TouchPhase.Began)
                    {
                        BeginTouch.x = fingerTouch.position.x;
                        BeginTouch.y = fingerTouch.position.y;

                        Ray ray = MainCamera.ScreenPointToRay(fingerTouch.position);
                        RaycastHit2D raycastHit2D = Physics2D.GetRayIntersection(ray);
                        if (raycastHit2D.collider != null)
                        {
                            if (isValidSelection(raycastHit2D.collider.gameObject))
                            {
                                SelectedObject.CurrentValue = raycastHit2D.collider.gameObject;
                                ObjectType = 2;
                            }
                            else
                            {
                                SelectedObject.CurrentValue = null;
                            }
                        }
                        else
                        {
                            ray = MainCamera.ScreenPointToRay(fingerTouch.position);
                            RaycastHit raycastHit3D;
                            if (Physics.Raycast(ray, out raycastHit3D))
                            {
                                if (isValidSelection(raycastHit3D.collider.gameObject))
                                {
                                    SelectedObject.CurrentValue = raycastHit3D.collider.gameObject;
                                    ObjectType = 3;
                                }
                                else
                                {
                                    SelectedObject.CurrentValue = null;
                                }
                            }
                            else
                            {
                                SelectedObject.CurrentValue = null;
                            }
                        }
                    }
                    if (fingerTouch.phase == TouchPhase.Moved)
                    {
                        if (SelectedObject != null && ObjectType == 2)
                        {
                            if (SelectedObject.CurrentValue != null)
                            {
                                SelectedObject.CurrentValue.transform.Rotate(0, 0, -fingerTouch.deltaPosition.x * RotationSpeed, Space.World);
                            }
                            else
                            {
                                SelectedObject.CurrentValue = null;
                            }
                        }
                        if (SelectedObject != null && ObjectType == 3)
                        {
                            if (SelectedObject.CurrentValue != null)
                            {
                                //SelectedObject.CurrentValue.transform.Rotate(fingerTouch.deltaPosition.y * RotationSpeed, -fingerTouch.deltaPosition.x * RotationSpeed, 0, Space.World);

                                if (RotationType == CRotationType.Horizontal)
                                {
                                    SelectedObject.CurrentValue.transform.Rotate(0, -fingerTouch.deltaPosition.x * RotationSpeed, 0, Space.World);
                                }
                                if (RotationType == CRotationType.Vertical)
                                {
                                    SelectedObject.CurrentValue.transform.Rotate(fingerTouch.deltaPosition.y * RotationSpeed, 0, 0, Space.World);
                                }
                                if (RotationType == CRotationType.AllDirection)
                                {
                                    SelectedObject.CurrentValue.transform.Rotate(fingerTouch.deltaPosition.y * RotationSpeed, -fingerTouch.deltaPosition.x * RotationSpeed, 0, Space.World);
                                }
                            }
                            else
                            {
                                SelectedObject.CurrentValue = null;
                            }
                        }
                    }

                }
            }
            if (usingMouseSimulation)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    BeginTouch.x = Input.mousePosition.x;
                    BeginTouch.y = Input.mousePosition.y;

                    Ray ray = MainCamera.ScreenPointToRay(BeginTouch);
                    RaycastHit2D raycastHit2D = Physics2D.GetRayIntersection(ray);
                    if (raycastHit2D.collider != null)
                    {
                        if (isValidSelection(raycastHit2D.collider.gameObject))
                        {
                            SelectedObject.CurrentValue = raycastHit2D.collider.gameObject;
                            ObjectType = 2;
                        }
                        else
                        {
                            SelectedObject.CurrentValue = null;
                        }
                    }
                    else
                    {
                        ray = MainCamera.ScreenPointToRay(BeginTouch);
                        RaycastHit raycastHit3D;
                        if (Physics.Raycast(ray, out raycastHit3D))
                        {
                            if (isValidSelection(raycastHit3D.collider.gameObject))
                            {
                                SelectedObject.CurrentValue = raycastHit3D.collider.gameObject;
                                ObjectType = 3;
                            }
                            else
                            {
                                SelectedObject.CurrentValue = null;
                            }
                        }
                        else
                        {
                            SelectedObject.CurrentValue = null;
                        }
                    }
                }
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    Vector3 deltaPosition = Vector3.zero;
                    deltaPosition.x = Input.mousePosition.x - BeginTouch.x;
                    deltaPosition.y = Input.mousePosition.y - BeginTouch.y;

                    if (SelectedObject.CurrentValue != null && ObjectType == 2)
                    {
                        if (SelectedObject.CurrentValue != null)
                        {
                            SelectedObject.CurrentValue.transform.Rotate(0, 0, -deltaPosition.x * RotationSpeed, Space.World);
                        }
                    }
                    if (SelectedObject.CurrentValue != null && ObjectType == 3)
                    {
                        if (SelectedObject.CurrentValue != null)
                        {
                            if (RotationType == CRotationType.Horizontal)
                            {
                                SelectedObject.CurrentValue.transform.Rotate(0, deltaPosition.y * RotationSpeed, 0, Space.World);
                            }
                            if (RotationType == CRotationType.Vertical)
                            {
                                SelectedObject.CurrentValue.transform.Rotate(-deltaPosition.x * RotationSpeed, 0, 0, Space.World);
                            }
                            if (RotationType == CRotationType.AllDirection)
                            {
                                SelectedObject.CurrentValue.transform.Rotate(deltaPosition.y * RotationSpeed, -deltaPosition.x * RotationSpeed, 0, Space.World);
                            }
                        }
                    } 
                }
            }
        }
    }
}
