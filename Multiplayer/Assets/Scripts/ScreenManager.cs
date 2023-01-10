using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;

public class ScreenManager : NetworkBehaviour
{
    public GameObject connect;
    public GameObject main;
    public TextMeshProUGUI playersInGame;

    private NetworkVariable<int> numofPlayer = new NetworkVariable<int>(0,
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    // Start is called before the first frame update
    void Start()
    {
        connect.SetActive(true);
        main.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        playersInGame.text = "Number of player: " + numofPlayer.Value;
    }

    public void startHost()
    {
        NetworkManager.Singleton.StartHost();
        connect.SetActive(false);
        main.SetActive(true);

        numofPlayer.Value++;
    }

    public void startClient()
    {
        NetworkManager.Singleton.StartClient();
        connect.SetActive(false);
        main.SetActive(true);

        numofPlayer.Value++;
    }
}
