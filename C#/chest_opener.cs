using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class chest_opener : MonoBehaviour
{
    [SerializeField]
    int gold = 50;
    [SyncVar]
    public NetworkInstanceId idPlayerbox;
    public Animator anima;
    private Transform myTransform;

    public bool crossed = false;
    // Use this for initialization
    void Start()
    {
        anima = gameObject.GetComponent<Animator>();
        myTransform = GetComponent<Transform>();
        gold = Random.Range(35, 75);
    }

    // Update is called once per frame
    void Update()
    {
        if (crossed)
        {
            Debug.Log("collision1");
            anima.SetBool("isOpened", true);
            //NetworkServer.objects[idPlayerbox].GetComponent<Player>().gold_Adder(gold);
            gold = 0;
            
            Invoke("destroyME", 2.0f);
            crossed = false;
        }
    }

    

    void destroyME()
    {
        Destroy(gameObject);
    }
}
