using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Personagem : MonoBehaviour {
	StateController state;
	string cena;
	GameObject player;
	enum State {
		Stand,
		StandShoot,
		Move,
		MoveShoot,
		Jump,
		JumpShoot,
		Dash,
		Fall,
		WallSliding,
		Death
	}; 
	int vida;
	[SerializeField]
	int velocidade;
	[SerializeField]
	Rigidbody2D rb2d;
	[SerializeField]
	Animator dust;
	[SerializeField]
	Renderer dustRend;
	[SerializeField]
	AudioSource dashSom,hitted,death;
	Animator anim;
	void Start () {
		cena = SceneManager.GetActiveScene().name;
		player=GetComponent<GameObject>();
		vida=10;
		anim=GetComponent<Animator>();
	}
	void Update () {
		Debug.Log(vida);
		state = GetComponent<StateController> ();


		if (state.currentState == (int)State.Jump && state.startingState)
		{
			rb2d.AddForce(new Vector2(0,400));
		}

		if (state.currentState == (int)State.Dash)
		{
			rb2d.velocity = new Vector2 (state.side*velocidade*1.7f, 0f);
			dustRend.enabled = true;
			dashSom.Play();
			dust.SetBool("dash",true);
			Invoke("DisableDust",0.4f);
			//transform.position += new Vector3 (state.side * velocidade * Time.deltaTime * 1.7f, 0, 0);
		}
		else if (state.currentState != (int)State.WallSliding)
		{
			rb2d.velocity = new Vector2 (state.side*velocidade, rb2d.velocity.y);
			//transform.position += new Vector3(state.side*velocidade*Time.deltaTime,0,0);
		}

		if (state.currentState == (int)State.WallSliding)
		{
			rb2d.velocity = new Vector2 (rb2d.velocity.x,Mathf.Max(-1f,rb2d.velocity.y));
		} 

	}
	void DisableDust(){
		dustRend.enabled = false;
		dust.SetBool("dash",false);
	}
	void OnTriggerEnter2D(Collider2D outro){ 
		if(outro.gameObject.tag.Equals("enemy")){
			vida-=5;
			if(vida<=0){
				death.Play();
				StateController.canMove=false;
				state.currentState = (int)State.Death;
				anim.SetBool("death",true);
				Invoke("ReloadScene",death.clip.length);
			}
			hitted.Play();
			anim.SetBool("atingido",true);
			Invoke("ChangeBool",0.2f);
		}
	}
	void ChangeBool(){
		anim.SetBool("atingido",false);
	}
	void ReloadScene(){
		SceneManager.LoadScene(cena);
	}
}
