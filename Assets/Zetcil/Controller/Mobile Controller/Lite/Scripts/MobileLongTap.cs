using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TechnomediaLabs;

namespace Zetcil
{
    public class MobileLongTap : MonoBehaviour
    {
        [Space(10)]
        public bool isEnabled;

        [Header("Variable Settings")]
        public VarBoolean LongTapStatus;

        [Header("Additional Status")]
        public float StartTime = 0;
        public float MaxLongTapTime = 1;
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
                if (IsLongTap())
                {
                    LongTapStatus.CurrentValue = true;
                    if (usingEventSettings)
                    {
                        EventSettings.Invoke();
                    }
                }
                else
                {
                    LongTapStatus.CurrentValue = false;
                }
            }
        }

        bool IsLongTap()
        {
            bool result = false;

            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {

                    if (Input.GetTouch(i).phase == TouchPhase.Began)
                    {
                        StartTime = Time.time;
                    }

                    if (Input.touchCount > 0 && Time.time - StartTime > MaxLongTapTime)
                    {
                        result = true;
                    }

                    if (Input.GetTouch(i).phase == TouchPhase.Ended && result)
                    {
                        result = false;
                    }
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
