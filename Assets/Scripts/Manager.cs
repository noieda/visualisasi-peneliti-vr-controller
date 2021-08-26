using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.Android;

public class Manager : EventHandler
{

    [Header("Koneksi dari unity ke database - Manager")]
    public InputField ip;
    public InputField port;
    public string ipAddress = "";
    public string portServer = "";
    private string param;

    // Start is called before the first frame update
    void Start()
    {
        StartConnection();
    }
    public void StartConnection()
    {
        Config config = new Config();
        Debug.Log("Manager -> " +ipAddress +":"+ portServer);
        //config.SetUrl(ip.text);
        //config.SetPort(port.text);
        config.SetUrl(ipAddress);
        config.SetPort(portServer);
        ApplyURL(config);
        configPanel.SetActive(false);
    }

    /*
    // Update is called once per frame
    void Update()
    {
        
    }
    */

    public void DashboardToggle()
    {
        dashboardStatus = !dashboardStatus;
        DashboardBar.SetActive(dashboardStatus);
        //DashboardBar.transform.LookAt(GetComponent<EventHandler>().playerHead.transform);

        //listDashboard = GameObject.FindGameObjectsWithTag("Dashboard");
        //foreach(GameObject dashboard in listDashboard)
        //{
        //    dashboard.SetActive(dashboardStatus);
        //}

        Debug.Log("dashboard button pressed <- manager");
    }

    public void PenelitianToggle()
    {
        detPenelitiStatus = !detPenelitiStatus;
        DetailPenelitiBar.SetActive(detPenelitiStatus);

        Debug.Log("peneliti button pressed <- eventHandler");
    }
    public void OptionToggle()
    {
        detOptionStatus = !detOptionStatus;
        OptionBar.SetActive(detOptionStatus);

        Debug.Log("option button pressed <- eventHandler");

    }
    public void OpenResearcherDetail()
    {
        DetailPenelitiBar.SetActive(true);
    }

    public void CloseResearcherDetail()
    {
        //Debug.Log("akrif");
        DetailPenelitiBar.SetActive(false);
    }
}
