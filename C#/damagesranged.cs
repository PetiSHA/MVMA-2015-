using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;


public class damagesranged : NetworkBehaviour
{
	[SerializeField]
	GameObject bulletPrefab;
	[SerializeField]
	GameObject arrowPrefab;

	private float counter = 0.0f;

    [SerializeField]
    private Animator animo;
    private Transform myTransform;
	private Transform targetTransform;
	private Transform target2Transform;
    // Use this for initialization
    private int damages;
    void Start()
    {
		

		damages = gameObject.GetComponentInParent<Move_destination>().damages_M;
    }
	[Command]
	void CmdTellMyArrowToTheServer()  //pas utilise pour l'instant
	{
		GameObject Arrow = Instantiate(arrowPrefab, target2Transform.position, Quaternion.identity) as GameObject;
		Arrow arrowScript = Arrow.GetComponent<Arrow>();
		
		arrowScript.syncYRotation2 = target2Transform.eulerAngles.y;
		arrowScript.idPlayer2 = GetComponent<NetworkIdentity>().netId;
		
		NetworkServer.Spawn(Arrow);
		Destroy(Arrow, 3.0f);
	}
	
	[Command]
	void CmdTellMyShootToTheServer()
	{
		GameObject bullet = Instantiate(bulletPrefab, targetTransform.position, Quaternion.identity) as GameObject;
		bullet bulletScript = bullet.GetComponent<bullet>();
		
		bulletScript.syncYRotation = targetTransform.eulerAngles.y;
		bulletScript.idPlayer = GetComponent<NetworkIdentity>().netId;
		
		NetworkServer.Spawn(bullet);
		Destroy(bullet, 1.0f);
	}

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        
        myTransform = GetComponent<Transform>();
        targetTransform = myTransform.FindChild("Target").GetComponent<Transform>();
        target2Transform = myTransform.FindChild("Target2").GetComponent<Transform>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player playerScript = other.GetComponent<Player>();
            gameObject.GetComponent<Transform>().LookAt(other.GetComponent<Transform>());
            if (counter>2.5f)
            {
                animo.Play("BASICATTACK");
                Invoke("CmdTellMyShootToTheServer", 1.1f);
				 

				counter = 0.0f;
                /*if (playerScript.health <= 0)
                    playerScript.died = true;*/

            }
            
            

        }
    }
}
