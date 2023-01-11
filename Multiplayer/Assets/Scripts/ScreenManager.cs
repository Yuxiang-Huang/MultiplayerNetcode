using System.Threading.Tasks;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;

public class ScreenManager : NetworkBehaviour
{
    public TextMeshProUGUI joinCodeText;
    public TMP_InputField joinInput;
    public GameObject buttons;

    private UnityTransport transport;
    private const int MaxPlayer = 4;

    public GameObject connect;

    private async void Awake()
    {
        transport = FindObjectOfType<UnityTransport>();

        buttons.SetActive(false);

        await Authenticate();

        buttons.SetActive(true);
    }

    public static async Task Authenticate()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

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
