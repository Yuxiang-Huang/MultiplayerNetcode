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
    public TMP_InputField numOfPlayer;
    public TMP_InputField playerName;
    public GameObject buttons;

    private UnityTransport transport;
    public int MaxPlayer = 5;

    public GameObject connect;

    private async void Awake()
    {
        transport = FindObjectOfType<UnityTransport>();

        buttons.SetActive(false);

        await Authenticate();

        buttons.SetActive(true);
    }

    void Start()
    {
        connect.SetActive(true);
    }

    public static async Task Authenticate()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    public async void createGame()
    {
        buttons.SetActive(false);

        //MaxPlayer = (int) numOfPlayer.text[0];

        Allocation a = await RelayService.Instance.CreateAllocationAsync(MaxPlayer);

        joinCodeText.text = "Join Code: " + await RelayService.Instance.GetJoinCodeAsync(a.AllocationId);

        transport.SetRelayServerData(a.RelayServer.IpV4, (ushort)a.RelayServer.Port, a.AllocationIdBytes, a.Key, a.ConnectionData);

        NetworkManager.Singleton.StartHost();
    }

    public async void joinGame()
    {
        buttons.SetActive(false);

        JoinAllocation a = await RelayService.Instance.JoinAllocationAsync(joinInput.text);

        transport.SetClientRelayData(a.RelayServer.IpV4, (ushort)a.RelayServer.Port, a.AllocationIdBytes, a.Key, a.ConnectionData,
        a.HostConnectionData);

        NetworkManager.Singleton.StartClient();
    }
}
