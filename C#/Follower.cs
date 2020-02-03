using UnityEngine;
using System.Collections;

public class Follower : MonoBehaviour
{
	//public Transform player;
	public float moveSpeed;
	// Use this for initialization
	void Start ()
	{
		moveSpeed = 7f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.Translate(moveSpeed*Input.GetAxis("Verticzl")*Time.deltaTime, 0f, moveSpeed*Input.GetAxis("Horizontal")*Time.deltaTime);
	}
/*	{
		Vector3 direction = player.position - this.transform.position;
		this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), 0.1f);
		direction.y = 0;
		this.transform.Translate (0, 0, 0.05f);
	}
}
	public Transform target; //the player
	public float moveSpeed = 2.0f;
	
	void Update(){
		//Look at the target
		transform.LookAt(target);
		
		//Move towards the target
		transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
	}*/
}
	