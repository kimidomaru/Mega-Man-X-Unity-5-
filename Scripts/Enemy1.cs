using UnityEngine;
using System.Collections;

public class Enemy1 : MonoBehaviour {
	Rigidbody2D rb2d;
	int vida;
	Animator anim;
	// Use this for initialization
	void Start () {
		rb2d=GetComponent<Rigidbody2D>();
		anim=GetComponent<Animator>();
		vida=5;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D outro){
		if(outro.gameObject.tag.Equals("tiro")){
			vida--;
			anim.SetBool("atingido",true);
			Invoke("ChangeBool",0.15f);
			if(vida==0)
				Destroy(gameObject);
		}
		if(outro.gameObject.tag.Equals("tiro2")){
			vida-=2;
			anim.SetBool("atingido",true);
			Invoke("ChangeBool",0.15f);
			if(vida==0)
				Destroy(gameObject);
		}
	}
	void ChangeBool(){
		anim.SetBool("atingido",false);
	}
}
