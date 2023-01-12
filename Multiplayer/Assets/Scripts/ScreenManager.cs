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

        Allocation a = await RelayService.Instance.CreateAllocationAsync(MaxPlayer);

        joinCodeText.text = await RelayService.Instance.GetJoinCodeAsync(a.AllocationId);

        transport.SetRelayServerData(a.RelayServer.IpV4, (ushort)a.RelayServer.Port, a.AllocationIdBytes, a.Key, a.ConnectionData);

        startHost();
    }

    public async void joinGame()
    {
        buttons.SetActive(false);

        JoinAllocation a = await RelayService.Instance.JoinAllocationAsync(joinInput.text);

        transport.SetClientRelayData(a.RelayServer.IpV4, (ushort)a.RelayServer.Port, a.AllocationIdBytes, a.Key, a.ConnectionData,
        a.HostConnectionData);

        startClient();
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
