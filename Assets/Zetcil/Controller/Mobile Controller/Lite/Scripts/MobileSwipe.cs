using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TechnomediaLabs;

namespace Zetcil
{

    public class MobileSwipe : MonoBehaviour
    {
        [Space(10)]
        public bool isEnabled;

        [Header("Variable Settings")]
        public VarString swipeStatus;

        [Header("Swipe Settings")]
        public string LeftSwipeFlag = "LEFT";
        public string RightSwipeFlag = "RIGHT";
        public string UpSwipeFlag = "UP";
        public string DownSwipeFlag = "DOWN";

        [Header("Input Status")]
        [ReadOnly] public Vector3 BeginTouch;

        [Header("Swipe Settings")]
        public bool usingSwipeLeft;
        public UnityEvent SwipeLeftEvent;
        public bool usingSwipeRight;
        public UnityEvent SwipeRightEvent;
        public bool usingSwipeUp;
        public UnityEvent SwipeUpEvent;
        public bool usingSwipeDown;
        public UnityEvent SwipeDownEvent;

        Vector3 firstTouch;
        Vector3 lastTouch;
        float FingerTouchX;
        float FingerTouchY;
        float dragDistance;

        // Use this for initialization
        void Start()
        {
            dragDistance = dragDistance = Screen.height * 15 / 100;
        }

        // Update is called once per frame
        void Update()
        {
            if (isEnabled)
            {
                IsSwipe();
            }
        }

        bool IsSwipe()
        {
            bool result = false;

            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch fingerTouch = Input.GetTouch(i);

                    if (fingerTouch.phase == TouchPhase.Began)
                    {
                        firstTouch = fingerTouch.position;
                        lastTouch = fingerTouch.position;

                        FingerTouchX = fingerTouch.position.x;
                        FingerTouchY = fingerTouch.position.y;

                    }
                    else if (fingerTouch.phase == TouchPhase.Moved)
                    {
                        lastTouch = fingerTouch.position;
                    }
                    else if (fingerTouch.phase == TouchPhase.Ended)
                    {
                        lastTouch = fingerTouch.position;

                        if (Mathf.Abs(lastTouch.x - firstTouch.x) > dragDistance || Mathf.Abs(lastTouch.y - firstTouch.y) > dragDistance)
                        {
                            //-- HORIZONTAL SWIPE
                            if (Mathf.Abs(lastTouch.x - firstTouch.x) > Mathf.Abs(lastTouch.y - firstTouch.y))
                            {
                                if ((lastTouch.x > firstTouch.x))
                                {
                                    swipeStatus.CurrentValue = RightSwipeFlag;
                                    if (usingSwipeRight)
                                    {
                                        SwipeRightEvent.Invoke();
                                    }

                                }
                                else
                                {
                                    swipeStatus.CurrentValue = LeftSwipeFlag;
                                    if (usingSwipeLeft)
                                    {
                                        SwipeLeftEvent.Invoke();
                                    }
                                }
                            }
                            //-- VERTICAL SWIPE
                            else
                            {
                                if (lastTouch.y > firstTouch.y)
                                {
                                    swipeStatus.CurrentValue = UpSwipeFlag;
                                    if (usingSwipeUp)
                                    {
                                        SwipeUpEvent.Invoke();
                                    }
                                }
                                else
                                {
                                    swipeStatus.CurrentValue = DownSwipeFlag;
                                    if (usingSwipeDown)
                                    {
                                        SwipeDownEvent.Invoke();
                                    }
                                }
                            }
                        }

                    }

                }
            }

            return result;
        }
    }
}
