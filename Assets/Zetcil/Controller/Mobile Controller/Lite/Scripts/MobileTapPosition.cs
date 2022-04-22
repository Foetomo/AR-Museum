using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;

namespace Zetcil
{

    public class MobileTapPosition : MonoBehaviour
    {
        public enum CObjectSelectionType { Everything, ByName, ByTag }

        [Space(10)]
        public bool isEnabled;

        [Header("Raycast Settings")]
        public Camera MainCamera;
        public VarObject SelectedObject;

        [Header("Object Settings")]
        public CObjectSelectionType ObjectSelectionType;
        [Tag] public string ObjectTag;
        public string ObjectName;
        public float PositionSpeed;

        [Header("Additional Settings")]
        public bool usingMouseSimulation;
        public bool ShowDebug;

        [Header("Input Status")]
        [ReadOnly] public Vector3 BeginTouch;

        void Update()
        {
            if (isEnabled)
            {
                IsTapPosition();
            }
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
                    Debug.Log(targetSelection);
                    result = true;
                }
            }

            return result;
        }

        void IsTapPosition()
        {
            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch fingerTouch = Input.GetTouch(i); // get first touch since touch count is greater than zero

                    if (fingerTouch.phase == TouchPhase.Began)
                    {
                        BeginTouch.x = fingerTouch.position.x;
                        BeginTouch.y = fingerTouch.position.y;

                        //-- cek tabrakan dengan objeck 2d
                        Ray ray = MainCamera.ScreenPointToRay(fingerTouch.position);
                        RaycastHit2D raycastHit2D = Physics2D.GetRayIntersection(ray);

                        if (raycastHit2D.collider != null)
                        {
                            if (isValidSelection(raycastHit2D.collider.gameObject))
                            {
                                SelectedObject.CurrentValue = raycastHit2D.collider.gameObject;
                            }
                            else
                            {
                                SelectedObject.CurrentValue = null;
                            }
                        }
                        else
                        {
                            //-- cek tabrakan dengan objeck 3d
                            ray = MainCamera.ScreenPointToRay(fingerTouch.position);
                            RaycastHit raycastHit3D;

                            if (Physics.Raycast(ray, out raycastHit3D))
                            {
                                if (isValidSelection(raycastHit3D.collider.gameObject))
                                {
                                    SelectedObject.CurrentValue = raycastHit3D.collider.gameObject;
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

                    if (fingerTouch.phase == TouchPhase.Stationary || fingerTouch.phase == TouchPhase.Moved)
                    {
                        // get the touch position from the screen touch to world point
                        Vector3 touchedPos = MainCamera.ScreenToWorldPoint(new Vector3(fingerTouch.position.x, fingerTouch.position.y, 10));

                        //SelectedObject.transform.position = Vector3.Lerp(SelectedObject.transform.position, touchedPos, Time.deltaTime);
                        if (SelectedObject.CurrentValue != null)
                        {
                            SelectedObject.CurrentValue.transform.position = touchedPos * PositionSpeed;
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

                    //-- cek tabrakan dengan objeck 2d
                    Ray ray = MainCamera.ScreenPointToRay(BeginTouch);
                    RaycastHit2D raycastHit2D = Physics2D.GetRayIntersection(ray);

                    if (raycastHit2D.collider != null)
                    {
                        if (isValidSelection(raycastHit2D.collider.gameObject))
                        {
                            SelectedObject.CurrentValue = raycastHit2D.collider.gameObject;
                        }
                        else
                        {
                            SelectedObject.CurrentValue = null;
                        }
                    }
                    else
                    {
                        //-- cek tabrakan dengan objeck 3d
                        ray = MainCamera.ScreenPointToRay(BeginTouch);
                        RaycastHit raycastHit3D;

                        if (Physics.Raycast(ray, out raycastHit3D))
                        {
                            if (isValidSelection(raycastHit3D.collider.gameObject))
                            {
                                SelectedObject.CurrentValue = raycastHit3D.collider.gameObject;
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
                    // get the touch position from the screen touch to world point
                    Vector3 touchedPos = MainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));

                    //SelectedObject.transform.position = Vector3.Lerp(SelectedObject.transform.position, touchedPos, Time.deltaTime);
                    if (SelectedObject.CurrentValue != null)
                    {
                        SelectedObject.CurrentValue.transform.position = touchedPos * PositionSpeed;
                    }
                }
            }
        }

        void OnGUI()
        {
            if (ShowDebug)
            {
                GUILayout.BeginVertical();
                GUILayout.Label("FingerTouchCount: " + Input.touchCount.ToString());
                GUILayout.Label("BeginTouch: " + BeginTouch.ToString());
                GUILayout.EndVertical();
            }
        }

    }
}