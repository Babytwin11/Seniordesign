using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class Minimap :NetworkBehaviour  {

    public Transform player;
	


    void Update ()

	{
		Vector3 newPosition = player.position;
		newPosition.y = transform.position.y;
		transform.position = newPosition;
	

		float angle = Mathf.Atan((player.position.x - transform.position.x)/(player.position.z - transform.position.z));      //Possibly wrong sign, depending on your setup
		transform.rotation = Quaternion.LookRotation(player.position - transform.position, Vector3.up);








		
	
	}
}
