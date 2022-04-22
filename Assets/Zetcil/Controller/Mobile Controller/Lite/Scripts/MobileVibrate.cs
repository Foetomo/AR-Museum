using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Zetcil
{
    public class MobileVibrate : MonoBehaviour
    {
        public enum CVibrateTrigger { OnAwake, OnEvent }

        [Space(10)]
        public bool isEnabled;

        [Header("Vibrate Setting")]
        public CVibrateTrigger VibrateTrigger;

        [Header("Vibrate Event")]
        public bool usingVibrateEvent;
        public UnityEvent VibrateEvent;

        public void ExecuteVibrate()
        {
            if (isEnabled)
            {
                if (VibrateTrigger == CVibrateTrigger.OnEvent)
                {
                    Handheld.Vibrate();

                    if (usingVibrateEvent)
                    {
                        VibrateEvent.Invoke();
                    }
                }
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            if (isEnabled)
            {
                if (VibrateTrigger == CVibrateTrigger.OnAwake)
                {
                    Handheld.Vibrate();

                    if (usingVibrateEvent)
                    {
                        VibrateEvent.Invoke();
                    }
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
