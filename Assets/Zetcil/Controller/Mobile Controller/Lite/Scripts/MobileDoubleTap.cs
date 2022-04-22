using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TechnomediaLabs;

namespace Zetcil
{
    public class MobileDoubleTap : MonoBehaviour
    {
        [Space(10)]
        public bool isEnabled;

        [Header("Variable Settings")]
        public VarBoolean DoubleTapStatus;

        [Header("Additional Settings")]
        public float MaxDoubleTapTime = 1;
        public float VariancePosition = 1;
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
                if (IsDoubleTap())
                {
                    DoubleTapStatus.CurrentValue = true;
                    if (usingEventSettings)
                    {
                        EventSettings.Invoke();
                    }
                    Invoke("SetDoubleTapFalse", 1);
                }
            }
        }

        void SetDoubleTapFalse()
        {
            DoubleTapStatus.CurrentValue = false;
        }

        bool IsDoubleTap()
        {
            bool result = false;

            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                float DeltaTime = Input.GetTouch(0).deltaTime;
                float DeltaPositionLength = Input.GetTouch(0).deltaPosition.magnitude;
                Debug.Log(Input.GetTouch(0).deltaPosition.magnitude.ToString());

                if (DeltaTime > 0 && DeltaTime < MaxDoubleTapTime && DeltaPositionLength < VariancePosition)
                    result = true;
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

                GUILayout.Label("DoubleTapStatus: " + DoubleTapStatus.ToString());
                GUILayout.EndVertical();
            }
        }
    }
}
