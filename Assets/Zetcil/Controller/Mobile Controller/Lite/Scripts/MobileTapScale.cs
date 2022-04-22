using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;

namespace Zetcil
{
    public class MobileTapScale : MonoBehaviour
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
        public float ScaleSpeed;

        [Header("Additional Settings")]
        public bool usingMouseSimulation;

        [Header("Input Status")]
        [ReadOnly] public Vector2 BeginTouch;

        Vector3 initialScale;
        float initialFingersDistance;

        // Use this for initialization
        void Start()
        {

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

        // Update is called once per frame
        void Update()
        {
            if (isEnabled)
            {
                IsTapScale();
            }

        }

        void IsTapScale()
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

                    if (Input.touches.Length == 2 && SelectedObject != null)
                    {
                        Touch t1 = Input.touches[0];
                        Touch t2 = Input.touches[1];

                        if (t1.phase == TouchPhase.Began || t2.phase == TouchPhase.Began)
                        {
                            if (SelectedObject.CurrentValue != null)
                            {
                                initialFingersDistance = Vector2.Distance(t1.position, t2.position);
                                initialScale = SelectedObject.CurrentValue.transform.localScale;
                            }
                        }
                        else if (t1.phase == TouchPhase.Moved || t2.phase == TouchPhase.Moved)
                        {
                            var currentFingersDistance = Vector2.Distance(t1.position, t2.position);
                            var scaleFactor = currentFingersDistance / initialFingersDistance;
                            if (SelectedObject.CurrentValue != null)
                            {
                                SelectedObject.CurrentValue.transform.localScale = initialScale * scaleFactor * ScaleSpeed;
                            }
                        }
                    }
                }
            }
        }
    }
}
