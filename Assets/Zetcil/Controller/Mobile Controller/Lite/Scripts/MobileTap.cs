using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TechnomediaLabs;

namespace Zetcil
{

    public class MobileTap : MonoBehaviour
    {
        [Space(10)]
        public bool isEnabled;

        [Header("Additional Settings")]
        public bool usingMouseSimulation;
        public bool ShowDebug;

        [Header("Input Status")]
        [ReadOnly] public Vector2 BeginTouch;
        [ReadOnly] public Vector2 MovedTouch;
        [ReadOnly] public Vector2 EndedTouch;

        [Header("Event Settings")]
        public bool usingEventSettings;
        public UnityEvent EventSettings;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (isEnabled)
            {
                if (IsTap())
                {
                    if (usingEventSettings)
                    {
                        EventSettings.Invoke();
                    }
                }
            }
        }

        bool IsTap()
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
                    if (fingerTouch.phase == TouchPhase.Moved)
                    {
                        MovedTouch.x = fingerTouch.position.x;
                        MovedTouch.y = fingerTouch.position.y;
                    }
                    if (fingerTouch.phase == TouchPhase.Ended)
                    {
                        EndedTouch.x = fingerTouch.position.x;
                        EndedTouch.y = fingerTouch.position.y;
                    }

                    result = true;
                }
            }
            if (usingMouseSimulation)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    BeginTouch = Input.mousePosition;
                }
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    MovedTouch = Input.mousePosition;
                }
                if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    EndedTouch = Input.mousePosition;
                }
            }

            return result;
        }

        void OnGUI()
        {
            if (ShowDebug)
            {
                GUILayout.BeginVertical();
                GUILayout.Label("FingerTouchCount: " + Input.touchCount.ToString());
                GUILayout.Label("BeginTouch: " + BeginTouch.ToString());
                GUILayout.Label("MovedTouch: " + MovedTouch.ToString());
                GUILayout.Label("EndedTouch: " + EndedTouch.ToString());
                GUILayout.EndVertical();
            }
        }
    }
}
