using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour {
	Transform player;
	Rigidbody2D tirorb;
	Animator anim;
	Renderer enemy;
	// Use this for initialization
	void Start () {
		player=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		tirorb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		anim.SetBool("explode",false);
		if(player.localScale.x>0)
			tirorb.velocity = new Vector2(10f,0f);
		else
			tirorb.velocity = new Vector2(-10f,0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D outro){
		if(outro.gameObject.tag.Equals("reset") || outro.gameObject.tag.Equals("enemy")){
			anim.SetBool("explode",true);
			tirorb.velocity= new Vector2(0f,0f);
			Destroy(gameObject,0.4f);
		}
	}
	void OnTriggerStay2D(Collider2D outro){
		if(outro.gameObject.tag.Equals("reset") || outro.gameObject.tag.Equals("enemy")){
			anim.SetBool("explode",true);
			tirorb.velocity= new Vector2(0f,0f);
			Destroy(gameObject,0.4f);
		}
	}
}
