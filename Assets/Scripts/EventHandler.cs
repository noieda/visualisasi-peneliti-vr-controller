using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.Android;
using System.IO;
using VRTK;
using TMPro;


public class EventHandler : MonoBehaviour
{
    // template
    public GameObject titleBar;
    public GameObject backgroundBar;
    public GameObject playerHead;
    
    [Header("Koneksi dari unity ke database - Event Handler")]
    // koneksi dari unity ke database
    public GameObject configPanel;
    public GameObject retry;
    public Text retryMessage;
    public GameObject connectionMessagePanel;
    public string URL;

    [Header("Dashboard")]
    // total publikasi - dashboard
    public GameObject DashboardBar;
    public Text publikasiJurnal;
    public Text publikasiKonferensi;
    public Text publikasiBuku;
    public Text publikasiTesis;
    public Text publikasiPaten;
    public Text publikasiPenelitian;
    public bool dashboardStatus = false;
    bool dashboardRefreshed = false;

    [Header("Peneliti Abjad-Fakultas")]
    // peneliti ( secara umum )
    public GameObject parentPenelitiScatter;
    public GameObject NodePeneliti;
    public GameObject peekPeneliti;
    public float sizeCoef = 0.005f;
    bool penelitiAbjadRefreshed = false;
    bool penelitiInisialRefreshed = false;
    bool penelitiFakultasRefreshed = false;
    bool penelitiDepartemenRefreshed = false;
    GameObject[] listPeneliti;

    [Header("Material")]
    public Material AbjadMaterial;
    public Material InisialMaterial;
    public Material FakultasMaterial;
    public Material DepartemenMaterial;


    [Header("Detail")]
    // detail peneliti
    
    public GameObject DetailPenelitiBar;
    public Text namaPeneliti;
    public Text fakultasPeneliti;
    public Text departemenPeneliti;
    public bool detPenelitiStatus = false;

    [Header("Tombol Navigasi")]
    public VRTK_InteractableObject tombolDashboard;
    public GameObject TableButton;

    RequestHandler requestPeneliti = new RequestHandler();

    // Start is called before the first frame update
    
    void Start()
    {
        //buttonPressed(name);
        TableButton = GameObject.Find("NavigationButton");
    }

    public void ApplyURL(Config config)
    {
        if (config.GetPort() == "")
        {
            URL = config.GetUrl();
        }
        else
        {
            URL = config.GetWebAPI();
        }
        //Dashboard();
        //getDetailPenelitiITS(4987.ToString());
        //getPenelitiAbjadITS();
        //getPenelitiFakultasITS();
        //Debug.Log(URL);
    }

    public void Dashboard()
    {
        if(dashboardRefreshed == false)
        {
            //dashboardRefreshed = true;
            //RequestHandler requestHandler = new RequestHandler();
            requestPeneliti.URL = URL;
            StartCoroutine(requestPeneliti.RequestData((result) =>
            {
                // mengambil jumlah jurnal, conference, books, thesis, paten dan research yang ada
                hasilPublikasiITS(result);
               
            }, (error) => {
                if (error != "")
                {
                    retryMessage.text = error;
                    retry.SetActive(true);
                    connectionMessagePanel.SetActive(false);
                }
            }));
        }
    }

    // Update is called once per frame
    void Update()
    {
        //buttonPressed(name);
    }

    void testingData(RawData rawdata)
    {
        Debug.Log(rawdata);
    }

    // hasilPublikasiITS adalah data pertama yang ditampilkan di dashboard
    void hasilPublikasiITS(RawData rawdata)
    {
 
        publikasiJurnal.text = rawdata.data[0].dashboard_data[0].hasil_publikasi[0].journals.ToString();
        publikasiKonferensi.text = rawdata.data[0].dashboard_data[0].hasil_publikasi[1].conferences.ToString();
        publikasiBuku.text = rawdata.data[0].dashboard_data[0].hasil_publikasi[2].books.ToString();

        //publikasiTesis.text = rawdata.data[0].dashboard_data[0].hasil_publikasi[3].thesis.ToString();
        //publikasiPaten.text = rawdata.data[0].dashboard_data[0].hasil_publikasi[4].paten.ToString();

        publikasiPenelitian.text = rawdata.data[0].dashboard_data[0].hasil_publikasi[5].research.ToString();

    }

    public void getPenelitiAbjadITS()
    {
        if(penelitiAbjadRefreshed == false)
        {
            //penelitiAbjadRefreshed = true;
            flushNode();

            requestPeneliti.URL = URL + "/peneliti?abjad=none";
            StartCoroutine(requestPeneliti.RequestData((result) => {
                foreach (var data in result.data[0].inisial_peneliti)
                {

                    GameObject NodeAbjadPeneliti = (GameObject)Instantiate(NodePeneliti);
                    NodeAbjadPeneliti.GetComponent<VRTK_PressedObject>().Start();
                    NodeAbjadPeneliti.name = data.inisial;
                    NodeAbjadPeneliti.tag = "ListPenelitiAbjad";
                    int jumlah = data.total;

                    //int jumlah = data.jumlah;

                    float size = jumlah * sizeCoef;

                    //var namaTest = NodeAbjadPeneliti.transform;

                    //var dababy = namaTest.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
                    //if (namaTest != null)
                    //{
                    //    dababy.text = NodeAbjadPeneliti.name;
                    //}

                    var orientation = NodeAbjadPeneliti.GetComponent<FloatingSphere>().orientation;
                    int test = Random.Range(0, 2);
                    
                    if (test < 1) { test = -1; }
                    orientation = orientation * test;

                    NodeAbjadPeneliti.transform.SetParent(parentPenelitiScatter.transform);
                    NodeAbjadPeneliti.transform.localPosition = new Vector3(Random.Range(-3.0f, 3.0f), 0, Random.Range(-3.0f, 3.0f));
                    NodeAbjadPeneliti.transform.localScale = new Vector3(size, size, size);
                    NodeAbjadPeneliti.GetComponent<Renderer>().material = AbjadMaterial;

                    NamaPeneliti tambahan = NodeAbjadPeneliti.AddComponent<NamaPeneliti>();
                    tambahan.nama = data.inisial;
                    tambahan.jumlah = jumlah;
                    tambahan.ukuran = size;

                    //transform.SetParent(ParentTransform, false);
                    //NodeAbjadPeneliti.transform.SetParent(parentPenelitiScatter.transform, false);
                }
                listPeneliti = GameObject.FindGameObjectsWithTag("ListPenelitiAbjad");
            }, error => {
                if (error != "")
                {
                    retryMessage.text = error;
                    retry.SetActive(true);
                    connectionMessagePanel.SetActive(false);
                }
            }
            ));
        }
        else
        {

        }

        

    }

    public void peekPenelitiAbjadITS(GameObject NodePeneliti)
    {
        GameObject peekNodePeneliti = (GameObject)Instantiate(peekPeneliti);
        
        var peekNode = peekNodePeneliti.transform;
        //float nodeDistance = NodePeneliti.transform.localScale.y;
        peekNode.name = "peekPeneliti";
        peekNode.SetParent(NodePeneliti.transform);

        var peek = peekNode.GetChild(0);

        peekNodePeneliti.transform.localPosition = new Vector3(0, -0f, 0);
        peekNodePeneliti.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        peekNodePeneliti.transform.LookAt(playerHead.transform);


        var peekNodeNama = peek.GetChild(1).GetComponent<TMP_Text>();
        var peekNodeJumlah = peek.GetChild(3).GetComponent<TMP_Text>();
        var namaPeneliti = NodePeneliti.GetComponent<NamaPeneliti>().nama;
        var jumlahPublikasi = NodePeneliti.GetComponent<NamaPeneliti>().jumlah;

        peekNodeNama.text = namaPeneliti;
        peekNodeJumlah.text = jumlahPublikasi.ToString();
    }

    public void getPenelitiInisialITS(string inisial)
    {
        if(penelitiInisialRefreshed == false)
        {
            //penelitiInisialRefreshed = true;

            flushNode();

            requestPeneliti.URL = URL + "/peneliti?abjad=" + inisial;
            StartCoroutine(requestPeneliti.RequestData((result) => {
                foreach (var data in result.data[0].nama_peneliti)
                {
                    //Debug.Log("getPenelitiAbjadIts ->" + data.inisial);
                    //Debug.Log("getPenelitiAbjadIts ->" + data.total);
                    GameObject NodeAbjadPeneliti = (GameObject)Instantiate(NodePeneliti);
                    Debug.Log(data.nama + " " + data.kode_dosen + " " + data.jumlah);
                    NodeAbjadPeneliti.name = data.kode_dosen;
                    int jumlah = data.jumlah;
                    //float sizeCoef = 0.05f;
                    float size = jumlah * sizeCoef;
                    NodeAbjadPeneliti.tag = "ListPenelitiInisial";
                    NodeAbjadPeneliti.transform.SetParent(parentPenelitiScatter.transform);
                    NodeAbjadPeneliti.transform.localPosition = new Vector3(Random.Range(-3.0f, 3.0f), 0, Random.Range(-3.0f, 3.0f));
                    NodeAbjadPeneliti.transform.localScale = new Vector3(size, size, size);
                    NodeAbjadPeneliti.GetComponent<Renderer>().material = InisialMaterial;
                    //NodeAbjadPeneliti.AddComponent<NamaPeneliti>().nama = data.nama;
                    NamaPeneliti tambahan = NodeAbjadPeneliti.AddComponent<NamaPeneliti>();
                    tambahan.kode_peneliti = data.kode_dosen;
                    tambahan.nama = data.nama;
                    tambahan.jumlah = jumlah;
                    tambahan.ukuran = size;
                }
                listPeneliti = GameObject.FindGameObjectsWithTag("ListPenelitiInisial");

            }, error => {
                if (error != "")
                {
                    retryMessage.text = error;
                    retry.SetActive(true);
                    connectionMessagePanel.SetActive(false);
                }
            }
            ));
        }
    }

    public void getPenelitiFakultasITS()
    {
        if(penelitiFakultasRefreshed == false)
        {
            //penelitiFakultasRefreshed = true;

            flushNode();

            requestPeneliti.URL = URL + "/peneliti?fakultas=none";
            StartCoroutine(requestPeneliti.RequestData((result) => {
                foreach (var data in result.data[0].fakultas_peneliti)
                {

                    GameObject NodeAbjadPeneliti = (GameObject)Instantiate(NodePeneliti);
                    NodeAbjadPeneliti.name = data.nama_fakultas;
                    //Debug.Log(data.kode_fakultas);
                    NodeAbjadPeneliti.tag = "ListPenelitiFakultas";
                    //int jumlah = data.total;

                    int jumlah = data.jumlah;
                    //float sizeCoef = 0.005f;
                    float size = jumlah * sizeCoef;

                    NodeAbjadPeneliti.transform.SetParent(parentPenelitiScatter.transform);
                    NodeAbjadPeneliti.transform.localPosition = new Vector3(Random.Range(-3.0f, 3.0f), 0, Random.Range(-3.0f, 3.0f));
                    NodeAbjadPeneliti.transform.localScale = new Vector3(size, size, size);
                    NodeAbjadPeneliti.GetComponent<Renderer>().material = FakultasMaterial;

                    NamaPeneliti tambahan = NodeAbjadPeneliti.AddComponent<NamaPeneliti>();
                    tambahan.kode_peneliti = data.kode_fakultas.ToString();
                    tambahan.jumlah = jumlah;
                    tambahan.ukuran = size;

                    //transform.SetParent(ParentTransform, false);
                    //NodeAbjadPeneliti.transform.SetParent(parentPenelitiScatter.transform, false);
                }
                listPeneliti = GameObject.FindGameObjectsWithTag("ListPenelitiFakultas");
            }, error => {
                if (error != "")
                {
                    retryMessage.text = error;
                    retry.SetActive(true);
                    connectionMessagePanel.SetActive(false);
                }
            }
            ));
        }
        
    }

    public void getPenelitiDepartemenITS(string kode_fakultas)
    {
        if(penelitiDepartemenRefreshed == false)
        {
            //penelitiDepartemenRefreshed = true;

            flushNode();

            requestPeneliti.URL = URL + "/peneliti?fakultas=" + kode_fakultas.ToString();
            StartCoroutine(requestPeneliti.RequestData((result) => {
                foreach (var data in result.data[0].departemen_peneliti)
                {

                    GameObject NodeAbjadPeneliti = (GameObject)Instantiate(NodePeneliti);
                    NodeAbjadPeneliti.name = data.nama_departemen;
                    NodeAbjadPeneliti.tag = "ListPenelitiDepartemen";
                    //int jumlah = data.total;

                    int jumlah = data.jumlah;
                    //float sizeCoef = 0.005f;
                    float size = jumlah * sizeCoef;

                    //var namaTest = NodeAbjadPeneliti.transform;
                    //var dababy = namaTest.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
                    //if (namaTest != null)
                    //{
                    //    dababy.text = NodeAbjadPeneliti.name;
                    //}

                    NodeAbjadPeneliti.transform.SetParent(parentPenelitiScatter.transform);
                    NodeAbjadPeneliti.transform.localPosition = new Vector3(Random.Range(-3.0f, 3.0f), 0, Random.Range(-3.0f, 3.0f));
                    NodeAbjadPeneliti.transform.localScale = new Vector3(size, size, size);
                    NodeAbjadPeneliti.GetComponent<Renderer>().material = DepartemenMaterial;

                    NamaPeneliti tambahan = NodeAbjadPeneliti.AddComponent<NamaPeneliti>();
                    //tambahan.nama = data.nama_fakultas;
                    tambahan.jumlah = jumlah;
                    tambahan.ukuran = size;

                    //transform.SetParent(ParentTransform, false);
                    //NodeAbjadPeneliti.transform.SetParent(parentPenelitiScatter.transform, false);
                }
                listPeneliti = GameObject.FindGameObjectsWithTag("ListPenelitiDepartemen");
            }, error => {
                if (error != "")
                {
                    retryMessage.text = error;
                    retry.SetActive(true);
                    connectionMessagePanel.SetActive(false);
                }
            }
            ));
        }
        
    }

    public void resizeNode(float zoom)
    {
        if(listPeneliti != null)
        {
            foreach (GameObject node in listPeneliti)
            {
                Debug.Log("resized");
                node.transform.localScale = node.transform.localScale + new Vector3(zoom, zoom, zoom);
            }
        }
    }    
    public void flushNode()
    {
        //Debug.Log(listPeneliti.Length);
        if(listPeneliti != null)
        {
            foreach (GameObject node in listPeneliti)
            {
                Destroy(node);
            }
        }
        
    }

    // detailPenelitiITS adalah data yang ditampilkan ketika melihat salah satu peneliti ITS
    public void getDetailPenelitiITS(string id_peneliti)
    {
        //foreach (GameObject node in listPeneliti)
        //{
        //    Destroy(node);
        //}
        //RequestHandler requestHandler = new RequestHandler();
        requestPeneliti.URL = URL + "/detailpeneliti?id_peneliti=" + id_peneliti;
        StartCoroutine(requestPeneliti.RequestData((result) =>
        {
            // mengambil jumlah jurnal, conference, books, thesis, paten dan research yang ada
            detailPenelitiITS(result);
        }, (error) => {
            if (error != "")
            {
                retryMessage.text = error;
                retry.SetActive(true);
                connectionMessagePanel.SetActive(false);
            }
        }));
    }

    void detailPenelitiITS(RawData rawdata)
    {
        //namaPeneliti.text = rawdata.data[0].dashboard_data[0].hasil_publikasi[0].journals.ToString();
        namaPeneliti.text = rawdata.data[0].detail_peneliti[0].nama;
        fakultasPeneliti.text = rawdata.data[0].detail_peneliti[0].fakultas;
        departemenPeneliti.text = rawdata.data[0].detail_peneliti[0].departemen;
    }

    public void buttonPressed(string identifier, string name = null)
    {
        //VRTK_InteractableObject test = tombolDashboard.GetComponent(VRTK_InteractableObject);
        Debug.Log("button pressed <- eventHandler");
        if(identifier == "ListPenelitiAbjad")
        {
            Debug.Log(identifier + "<- eventhandler");
            getPenelitiInisialITS(name);
        }
        else if(identifier == "ListInisialPeneliti")
        {
            Debug.Log(identifier + "<- eventhandler");
            getDetailPenelitiITS(name);
        }
        else if(identifier == "DashboardButton")
        {
            dashboardStatus = !dashboardStatus;
            DashboardBar.SetActive(dashboardStatus);

            Debug.Log("dashboard button pressed <- eventHandler");
        }
        else if(identifier == "PenelitiButton")
        {
            detPenelitiStatus = !detPenelitiStatus;
            DetailPenelitiBar.SetActive(detPenelitiStatus);

            Debug.Log("peneliti button pressed <- eventHandler");
        }

    }


}   