using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerManager : NetworkBehaviour
{
    private NetworkVariable<int> randomInt = new NetworkVariable<int>(0,
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(OwnerClientId + ": " + randomInt.Value);

        if (Input.GetKeyDown(KeyCode.T))
        {
            randomInt.Value = Random.Range(0, 100);
        }
    }
}