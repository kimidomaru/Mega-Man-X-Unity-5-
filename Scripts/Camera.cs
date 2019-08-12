using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {
	Transform player;
	float x;
	// Use this for initialization
	void Start () {
		player=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		x=transform.position.x;
		if(x<0.7f && player.position.x>=-0.6f)
			x=0.7f;
		if(x>=-0.7f)
			transform.position = new Vector3(player.position.x,0,-10);
	}
}
