using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using Unity.Netcode;
using TMPro;

public class PlayerManager : NetworkBehaviour
{
    public RectTransform canvas;
    public GameObject playerSet;
    public TextMeshProUGUI displayCard;
    public TextMeshProUGUI displayWord;

    List<string> allWords = new List<string>();

    private NetworkVariable<CustomData> data = new NetworkVariable<CustomData>(
    new CustomData
    {
        cards = 10,
        word = ""
    },
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public struct CustomData : INetworkSerializable
    {
        public int cards;
        public FixedString128Bytes word;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref cards);
            serializer.SerializeValue(ref word);
        }
    }

    public override void OnNetworkSpawn() { 
        Debug.Log(OwnerClientId);
        float width = canvas.rect.width;
        playerSet.transform.position = new Vector3((OwnerClientId + 1) * width / 5,
            playerSet.transform.position.y, playerSet.transform.position.z);

        allWords.Add("Say 'Cat'");
        allWords.Add("Say 'Apple'");
        allWords.Add("Say 'Dog'");
        allWords.Add("Laugh");
        allWords.Add("Touch hands");
        allWords.Add("Reject to answer");

        generateWord();
    }


    // Update is called once per frame
    void Update()
    {
        displayCard.text = "Cards Left: " + data.Value.cards;
        displayWord.text = "" + data.Value.word;
    }

    public void generateWord()
    {
        if (!IsOwner) return;


        data.Value = new CustomData
        {
            cards = data.Value.cards,
            word = allWords[Random.Range(0, allWords.Count)]
        };
    }
}
