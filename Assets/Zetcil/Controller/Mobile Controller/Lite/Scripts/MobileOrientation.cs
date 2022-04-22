using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zetcil
{
    public class MobileOrientation : MonoBehaviour
    {
        public bool isEnabled;

        public enum COrientation { Potrait, Landscape }

        [Header("Invoke Settings")]
        public GlobalVariable.CInvokeType InvokeType;

        [Header("Orientation Settings")]
        public COrientation Orientation;

        // Start is called before the first frame update
        void Start()
        {
            if (InvokeType == GlobalVariable.CInvokeType.OnAwake ||
                InvokeType == GlobalVariable.CInvokeType.OnStart)
            {
                InvokeOrientation();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void InvokeOrientation()
        {
            if (Orientation == COrientation.Potrait)
            {
                InvokePortrait();
            }
            if (Orientation == COrientation.Landscape)
            {
                InvokeLandscape();
            }
        }

        public void InvokePortrait()
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }

        public void InvokeLandscape()
        {
            Screen.orientation = ScreenOrientation.Landscape;
        }

    }
}
