using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using System.Net;


public class DispMyIP : MonoBehaviour
{
    private InputField myInputField;
    // Use this for initialization
    void Start()
    {
        myInputField = GameObject.Find("yourIP").GetComponent<InputField>();
        StartCoroutine_Auto(recuptextweb());
    }

    private IEnumerator recuptextweb()
    {

        WWW www = new WWW("http://magicviolentmeowalert.alwaysdata.net/getIP.php");


        yield return www;
        myInputField.text = www.text;

    }
}
