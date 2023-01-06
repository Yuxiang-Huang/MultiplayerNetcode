using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Menu : MonoBehaviour
{
    public GameObject menu;

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
        menu.SetActive(false);
    }

    public void startClient()
    {
        NetworkManager.Singleton.StartClient();
        menu.SetActive(false);
    }
}
