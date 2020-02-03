using System.Collections;
using System.Net;
#if UNITY_EDITOR
using UnityEditor;
#endif 
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MenuMulti : MonoBehaviour
{

    [SerializeField]

    WebClient client = new WebClient();
    private InputField myInputField;
    private InputField hostIPInputField;
    private Text EnterIP;
    public NetworkManager manager;




    // Use this for initialization

    void Start()
    {

        hostIPInputField = GameObject.Find("Interface de connexion perso").GetComponent<Transform>().FindChild("Panel").FindChild("InputIP").GetComponent<InputField>();
        myInputField = GameObject.Find("Interface de connexion perso").GetComponent<Transform>().FindChild("Panel").FindChild("yourIP").GetComponent<InputField>();
        EnterIP = GameObject.Find("Interface de connexion perso").GetComponent<Transform>().FindChild("Panel").FindChild("PlzEnterYourIP").GetComponent<Text>();
        hostIPInputField.text = NetworkManager.singleton.networkAddress;
    }
    /*
    // Update is called once per frame
    void Update()
    {
     
    }

    public void StartLocalGame()
    {
        
        NetworkManager.singleton.StartHost(); // Créer le serveur  
        //Network.InitializeServer(10, Port);
    }*/
    public void JoinLocalGame()
    {
        if (hostIPInputField.text != "")
        {

            //NetworkManager.singleton.StartClient();// Créer un client  
            //Network.Connect(hostIPInputField.text, Port);
            EnterIP.text = "";

        }
        else
            EnterIP.text = "Please enter an IP adress";
    }
    /*
    public void Disconect()
    {
        
        NetworkManager.singleton.StopClient(); // Se déconnecter  
    }*/

    #region Web Connection


    private IEnumerator recuptextweb()
    {

        WWW www = new WWW("http://magicviolentmeowalert.alwaysdata.net/getIP.php");


        yield return www;
        myInputField.text = www.text;

    }

    public void DispMyIP()
    {
        StartCoroutine_Auto(recuptextweb());
    }

    public void PasteInClipBoard()
    {
#if UNITY_EDITOR
        EditorGUIUtility.systemCopyBuffer = myInputField.text;
#endif
    }

    public void PasteFromClipBoard()
    {
#if UNITY_EDITOR
        hostIPInputField.text = EditorGUIUtility.systemCopyBuffer;
#endif
    }

    #endregion
}

