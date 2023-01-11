using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;

public class PlayerManager : NetworkBehaviour
{
    public RectTransform canvas;
    public GameObject playerSet;
    public TextMeshProUGUI display;

    private NetworkVariable<int> randomInt = new NetworkVariable<int>(0,
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public override void OnNetworkSpawn() { 
        Debug.Log(OwnerClientId);
        float width = canvas.rect.width;
        playerSet.transform.position = new Vector3((OwnerClientId + 1) * width / 5,
            playerSet.transform.position.y, playerSet.transform.position.z);
    }


    // Update is called once per frame
    void Update()
    {
        //Debug.Log(OwnerClientId + ": " + randomInt.Value);
    }

    [ServerRpc]
    public void generateNumberServerRpc()
    {
        display.text = "" + Random.Range(0, 10);
        randomInt.Value = Random.Range(0, 100);
        Debug.Log(randomInt.Value);
        displayNumberClientRpc();
    }

    [ClientRpc]
    public void displayNumberClientRpc()
    {
        Debug.Log(OwnerClientId + ": " + randomInt.Value);
        display.text = "" + randomInt.Value;
    }
}
