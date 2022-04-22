using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sistem : MonoBehaviour
{
    public static Sistem instance;
    public int ID;
    public GameObject TempatSpawn;
    public GameObject[] KoleksiBenda;
    public GameObject[] BtnSuara;
    public GameObject[] BtnDeskripsi;
    public GameObject[] PnlDeskripsi;

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        ID = 0;
    }

    public void SpawnObject()
    {
        GameObject BendaSebelumnya = GameObject.FindGameObjectWithTag("Benda");
        if (BendaSebelumnya != null) Destroy(BendaSebelumnya);

        GameObject Benda =  Instantiate(KoleksiBenda[ID]);
        Benda.transform.SetParent(TempatSpawn.transform, false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GantiBenda(true);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GantiBenda(false);
        }

    }

    public void GantiBenda(bool Kanan)
    {
        for (int i = 0; i < PnlDeskripsi.Length - 1; i++)
        {
            PnlDeskripsi[i].gameObject.SetActive(false);
            BtnSuara[i].gameObject.SetActive(true);
        }

        if (Kanan)
        {
            if (ID >= KoleksiBenda.Length - 1)
                ID = 0;
            else
                ID += 1;
        }
        else // kalo ke kiri
        {
            if (ID <= 0)
                ID = KoleksiBenda.Length - 1;
            else
                ID -= 1;
        }

        if (ID == 0)
        {
            BtnDeskripsi[0].gameObject.SetActive(true);
            BtnDeskripsi[1].gameObject.SetActive(false);
            BtnDeskripsi[2].gameObject.SetActive(false);
            BtnDeskripsi[3].gameObject.SetActive(false);
            BtnDeskripsi[4].gameObject.SetActive(false);
            BtnSuara[0].gameObject.SetActive(true);
            BtnSuara[1].gameObject.SetActive(false);
            BtnSuara[2].gameObject.SetActive(false);
            BtnSuara[3].gameObject.SetActive(false);
            BtnSuara[4].gameObject.SetActive(false);
        }
        else if (ID == 1)
        {
            BtnDeskripsi[0].gameObject.SetActive(false);
            BtnDeskripsi[1].gameObject.SetActive(true);
            BtnDeskripsi[2].gameObject.SetActive(false);
            BtnDeskripsi[3].gameObject.SetActive(false);
            BtnDeskripsi[4].gameObject.SetActive(false);
            BtnSuara[0].gameObject.SetActive(false);
            BtnSuara[1].gameObject.SetActive(true);
            BtnSuara[2].gameObject.SetActive(false);
            BtnSuara[3].gameObject.SetActive(false);
            BtnSuara[4].gameObject.SetActive(false);
        }
        else if (ID == 2) 
        {
            BtnDeskripsi[0].gameObject.SetActive(false);
            BtnDeskripsi[1].gameObject.SetActive(false);
            BtnDeskripsi[2].gameObject.SetActive(true);
            BtnDeskripsi[3].gameObject.SetActive(false);
            BtnDeskripsi[4].gameObject.SetActive(false);
            BtnSuara[0].gameObject.SetActive(false);
            BtnSuara[1].gameObject.SetActive(false);
            BtnSuara[2].gameObject.SetActive(true);
            BtnSuara[3].gameObject.SetActive(false);
            BtnSuara[4].gameObject.SetActive(false);
        }
        else if (ID == 3) 
        {
            BtnDeskripsi[0].gameObject.SetActive(false);
            BtnDeskripsi[1].gameObject.SetActive(false);
            BtnDeskripsi[2].gameObject.SetActive(false);
            BtnDeskripsi[3].gameObject.SetActive(true);
            BtnDeskripsi[4].gameObject.SetActive(false);
            BtnSuara[0].gameObject.SetActive(false);
            BtnSuara[1].gameObject.SetActive(false);
            BtnSuara[2].gameObject.SetActive(false);
            BtnSuara[3].gameObject.SetActive(true);
            BtnSuara[4].gameObject.SetActive(false);
        }
        else if (ID == 4) 
        {
            BtnDeskripsi[0].gameObject.SetActive(false);
            BtnDeskripsi[1].gameObject.SetActive(false);
            BtnDeskripsi[2].gameObject.SetActive(false);
            BtnDeskripsi[3].gameObject.SetActive(false);
            BtnDeskripsi[4].gameObject.SetActive(true);
            BtnSuara[0].gameObject.SetActive(false);
            BtnSuara[1].gameObject.SetActive(false);
            BtnSuara[2].gameObject.SetActive(false);
            BtnSuara[3].gameObject.SetActive(false);
            BtnSuara[4].gameObject.SetActive(true);
        }
            

        SpawnObject();

    }

}
