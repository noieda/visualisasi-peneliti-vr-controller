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
        SetURL();
    }
    public void SetURL()
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DashboardToggle()
    {
        dashboardStatus = !dashboardStatus;
        DashboardBar.SetActive(dashboardStatus);

        Debug.Log("dashboard button pressed <- eventHandler");
    }

    public void PenelitianToggle()
    {
        detPenelitiStatus = !detPenelitiStatus;
        DetailPenelitiBar.SetActive(detPenelitiStatus);

        Debug.Log("peneliti button pressed <- eventHandler");
    } 
}
