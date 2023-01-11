using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Relay;
using Unity.Services.Relay.Http;
using Unity.Services.Relay.Models;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport;
using Unity.Networking.Transport.Relay;
using NetworkEvent = Unity.Networking.Transport.NetworkEvent;

public class ScreenManager : NetworkBehaviour
{
    public GameObject connect;

    // Start is called before the first frame update
    void Start()
    {
        connect.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {   
        //playersInGame.text = "Number of player: " + numofPlayer.Value;
    }

    public void startHost()
    {
        NetworkManager.Singleton.StartHost();
        connect.SetActive(false);
        //MyUI.gameObject.SetActive(true);
        //numofPlayer.Value++;
    }

    public void startClient()
    {
        NetworkManager.Singleton.StartClient();
        connect.SetActive(false);
        //MyUI.gameObject.SetActive(true);
        //numofPlayer.Value++;
    }
}
