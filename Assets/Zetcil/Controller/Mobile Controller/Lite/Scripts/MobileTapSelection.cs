using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;

namespace Zetcil
{

    public class MobileTapSelection : MonoBehaviour
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
        
        [Header("Additional Settings")]
        public bool usingMouseSimulation;
        public bool ShowDebug;

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
                IsTapSelection();
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
        void IsTapSelection()
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
                }
            }
            if (usingMouseSimulation)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    BeginTouch = Input.mousePosition;

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
            }
        }

        void OnGUI()
        {
            if (ShowDebug)
            {
                GUILayout.BeginVertical();
                GUILayout.Label("FingerTouchCount: " + Input.touchCount.ToString());
                GUILayout.Label("BeginTouch: " + BeginTouch.ToString());
                if (SelectedObject.CurrentValue != null) GUILayout.Label("SelectedObject: " + SelectedObject.CurrentValue.name);
                GUILayout.EndVertical();
            }
        }
    }
}
