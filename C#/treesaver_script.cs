using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;

public class treesaver_script : MonoBehaviour {
	[SerializeField]
	string path_to_savefile ;
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			gameObject.GetComponentInChildren<TextMesh>().text = "SAVED";
			Player player = other.GetComponent<Player>();
			Transform transform_p = other.GetComponent<Transform>();
            treeloader loader = GameObject.Find("TreeLoad").GetComponent<treeloader>();
			System.IO.FileStream fs = new System.IO.FileStream(path_to_savefile + "/Save_test", System.IO.FileMode.Create, System.IO.FileAccess.Write);
			System.IO.BinaryWriter writer = new System.IO.BinaryWriter(fs);
			float x = transform_p.position.x;
			float y = transform_p.position.y;
			float z =transform_p.position.z;
			float rx = transform_p.rotation.x;
			float ry = transform_p.rotation.y;
			float rz = transform_p.rotation.z;
			string save = player.Level + "n" + player.health + "n" + player.mana + "n"
				+ player.stamina + "n" + player.score + "n" + player.gold + "n" + player.experience + "n" + x + "n"
					+ y + "n" + z + "n" + rx + "n" + ry + "n" + rz + "n";
			writer.Write(save);
			fs.Close();
            loader.health = player.health;
            loader.mana = player.mana;
            loader.stamina = player.stamina;
            loader.gold = player.gold;
            loader.LEvel = player.Level;
            loader.experience = player.experience;
		}
	}
}