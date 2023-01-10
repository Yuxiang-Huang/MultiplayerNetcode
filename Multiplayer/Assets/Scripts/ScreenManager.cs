using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;

public class ScreenManager : NetworkBehaviour
{
    public GameObject connect;
    //public TextMeshProUGUI playersInGame;

    //private NetworkVariable<int> numofPlayer = new NetworkVariable<int>(0,
    //    NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        connect.SetActive(true);

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(OwnerClientId + ": " + gameManager.display.text);
        
        //playersInGame.text = "Number of player: " + numofPlayer.Value;
    }

    public void startHost()
    {
        NetworkManager.Singleton.StartHost();
        connect.SetActive(false);

        //numofPlayer.Value++;
    }

    public void startClient()
    {
        NetworkManager.Singleton.StartClient();
        connect.SetActive(false);

        //numofPlayer.Value++;
    }
}
