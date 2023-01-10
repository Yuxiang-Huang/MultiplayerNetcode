using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;

public class PlayerManager : NetworkBehaviour
{
    public GameObject main;
    public TextMeshProUGUI display;

    private NetworkVariable<int> randomInt = new NetworkVariable<int>(0,
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(OwnerClientId + ": " + randomInt.Value);

        if (Input.GetKeyDown(KeyCode.T))
        {
            randomInt.Value = Random.Range(0, 100);
        }
    }

    public void generateNumber()
    {
        display.text = "" + Random.Range(0, 10);
    }
}
