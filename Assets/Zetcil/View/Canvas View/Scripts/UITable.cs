using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechnomediaLabs;

namespace Zetcil {

    public class UITable : MonoBehaviour
    {
        public enum CTableDataLoad { None, ByDelay, ByInputKey, ByEvent }

        public bool isEnabled;

        [Header("Invoke Settings")]
        public GlobalVariable.CInvokeType InvokeType;

        [Header("Data Settings")]
        public VarJSON JSONData;

        [Header("Delay Settings")]
        public bool usingDelay;
        public int Delay;

        [Header("Interval Settings")]
        public bool usingInterval;
        public int Interval;

        [Header("Table Settings")]
        public GameObject TableViewport;
        public GameObject TableContent;
        public UITableRow TableRow;
        public float TableRowSize = 50;

        public int TotalColumns;

        public void ExecuteTableDataLoad()
        {
            TableContent.GetComponent<RectTransform>().sizeDelta = new Vector2(0,
                TableContent.GetComponent<RectTransform>().rect.height + (TableRowSize * JSONData.JSONRoot.Count));
            for (int i=0; i < JSONData.JSONRoot.Count; i++)
            {
                GameObject temp = GameObject.Instantiate(TableRow.gameObject, TableContent.transform);

                int JSONColumn = temp.GetComponent<UITableRow>().DataColumns.Length;
                for (int j=0; j<JSONColumn; j++)
                {
                    string aData = JSONData.JSONRoot[i].JSONData[j].Value;
                    temp.GetComponent<UITableRow>().SetDataRow(i+1, j, aData);
                }
            }
        }

        public void InvokeJSONTable()
        {
            ExecuteTableDataLoad();
        }

        void Awake()
        {
            if (isEnabled)
            {
                if (InvokeType == GlobalVariable.CInvokeType.OnAwake)
                {
                    InvokeJSONTable();
                }
            }
        }

        // Start is called before the first frame update

        void Start()
        {
            if (isEnabled)
            {
                if (InvokeType == GlobalVariable.CInvokeType.OnStart)
                {
                    InvokeJSONTable();
                }
                else if (InvokeType == GlobalVariable.CInvokeType.OnDelay)
                {
                    Invoke("InvokeJSONTable", Delay);
                }
                else if (InvokeType == GlobalVariable.CInvokeType.OnInterval)
                {
                    InvokeRepeating("InvokeJSONTable", 1, Interval);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
