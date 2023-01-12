using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : NetworkBehaviour
{
    public RectTransform canvas;
    public RectTransform playerSetRect;
    public GameObject playerSet;
    public TextMeshProUGUI displayCard;
    public TextMeshProUGUI displayWord;

    public TextMeshProUGUI playerName;

    public Button revealBtn;
    public Button winCard;
    public Button loseCard;

    public bool revealed = false;

    List<string> allWords = new List<string>();

    public int corNum;

    //initialize data
    private NetworkVariable<CustomData> data = new NetworkVariable<CustomData>(
        new CustomData
        {
            cards = 5,
            word = "",
            playerName = "",
        },
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    //my data
    public struct CustomData : INetworkSerializable
    {
        public int cards;
        public FixedString128Bytes word;
        public FixedString128Bytes playerName;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref cards);
            serializer.SerializeValue(ref word);
            serializer.SerializeValue(ref playerName);
        }
    }

    //code body
    public override void OnNetworkSpawn() {
        ScreenManager screenManager = GameObject.Find("Screen Manager").GetComponent<ScreenManager>();

        //change name
        if (IsOwner)
        {
            data.Value = new CustomData
            {
                cards = 5,
                word = "",
                playerName = new FixedString128Bytes(screenManager.playerName.text)
            };
        }

        corNum = (int)OwnerClientId;

        createList();

        generateWord();
    }

    void createList()
    {
        allWords.Add("Say 'Unbelievable'");
        allWords.Add("Say 'Apple'");
        allWords.Add("Say 'Dog'");
        allWords.Add("Laugh");
        allWords.Add("Touch hands");
        allWords.Add("Reject to answer");
        allWords.Add("Guess the phrase");
    }


    // Update is called once per frame
    void Update()
    {
        //update text
        playerName.text = "" + data.Value.playerName;
        displayCard.text = "Cards Left: " + data.Value.cards;

        //display cards that are not mine
        if (!IsOwner || revealed)
        {
            displayWord.text = "" + data.Value.word;
            revealBtn.gameObject.SetActive(false);
        }
        else
        { 
            displayWord.text = "";
            revealBtn.gameObject.SetActive(true);
        }

        if (revealed)
        {
            winCard.gameObject.SetActive(true);
            loseCard.gameObject.SetActive(true);
        }
        else
        {
            winCard.gameObject.SetActive(false);
            loseCard.gameObject.SetActive(false);
        }

        //position
        float width = canvas.rect.width;
        playerSet.transform.position = new Vector3((corNum * 2 + 1) * width / 9 - width / 2,
            playerSet.transform.position.y, playerSet.transform.position.z);

        //middle left
        if (corNum < 5 / 2.0)
        {
            playerSetRect.anchorMin = new Vector2(0, 0.5f);
            playerSetRect.anchorMax = new Vector2(0, 0.5f);
            playerSetRect.pivot = new Vector2(0, 0.5f);
            playerSet.transform.position = new Vector3((corNum * 2 + 1) * width / 9,
    playerSet.transform.position.y, playerSet.transform.position.z);
        }

        //middle right
        if (corNum > 5 / 2.0)
        {
            playerSetRect.anchorMin = new Vector2(1, 0.5f);
            playerSetRect.anchorMax = new Vector2(1, 0.5f);
            playerSetRect.pivot = new Vector2(1, 0.5f);
            playerSet.transform.position = new Vector3((corNum * 2 + 1) * width / 9 + width / 2,
            playerSet.transform.position.y, playerSet.transform.position.z);
        }
    }

    public void reveal() {
        if (!IsOwner) return;

        revealed = true;
    }

    public void win()
    {
        if (!IsOwner) return;

        data.Value = new CustomData
        {
            cards = data.Value.cards + 1,
            word = allWords[Random.Range(0, allWords.Count)],
            playerName = data.Value.playerName,
        };

        revealed = false;
    }

    public void lose()
    {
        if (!IsOwner) return;

        data.Value = new CustomData
        {
            cards = data.Value.cards - 1,
            word = allWords[Random.Range(0, allWords.Count)],
            playerName = data.Value.playerName,
        };

        revealed = false;
    }


    public void generateWord()
    {
        if (!IsOwner) return;

        data.Value = new CustomData
        {
            cards = data.Value.cards,
            word = allWords[Random.Range(0, allWords.Count)],
            playerName = data.Value.playerName,
        };
    }
}
