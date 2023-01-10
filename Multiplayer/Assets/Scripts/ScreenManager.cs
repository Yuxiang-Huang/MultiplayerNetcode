using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ScreenManager : MonoBehaviour
{
    public GameObject connect;
    public GameObject main;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startHost()
    {
        NetworkManager.Singleton.StartHost();
        connect.SetActive(false);
        main.SetActive(true);
    }

    public void startClient()
    {
        NetworkManager.Singleton.StartClient();
        connect.SetActive(false);
        main.SetActive(true);
    }
}
