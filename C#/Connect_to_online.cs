using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Connect_to_online : MonoBehaviour
{
    public NetworkManager manager;
    [SerializeField]
    public bool showGUI = true;
    [SerializeField]
    public int offsetX;
    [SerializeField]
    public int offsetY;
    [SerializeField]
    Button createHost;
    [SerializeField]
    Button joinGame;
    private InputField hostIPInputField;
    public InputField inputName;
    // Runtime variable
    bool showServer = false;

    void Awake()
    {
        manager = GetComponent<NetworkManager>();
    }
    void Start()
    {

        hostIPInputField = GameObject.Find("Interface de connexion perso").GetComponent<Transform>().FindChild("Panel").FindChild("InputIP").GetComponent<InputField>();
        inputName = GameObject.Find("Interface de connexion perso").GetComponent<Transform>().FindChild("Panel").FindChild("PlayerName").GetComponent<InputField>();


    }
    void Update()
    {

        /*
        if (!NetworkClient.active && !NetworkServer.active && manager.matchMaker == null)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                manager.StartServer();
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                manager.StartHost();
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                manager.StartClient();
            }
        }*/
        if (NetworkServer.active && NetworkClient.active)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                manager.StopHost();
            }
        }
    }

    public void startHosting()
    {
        if (inputName.text != "")
        {
            InfoStorage.namee = inputName.text;
            manager.StartHost();

        }

    }
    public void startClient()
    {
        if (hostIPInputField != null)
        {
            if (inputName.text != "")
            {
                InfoStorage.namee = inputName.text;
                manager.networkAddress = hostIPInputField.text;
                manager.StartClient();
            }
        }

    }
    public void stopPlaying()
    {
        manager.StopHost();
    }

}

