using UnityEngine;
using System.Collections;

public class StateController : MonoBehaviour {

	public int currentState;
	public float side;
	public bool startingState;
	public int stateTimer;
	bool dashing,shotSound=false,shotSound2=false,cor=false;
	float dashSide,shotCount=0,colorCount=0;
	int dashTimer;
	public static bool canMove;

	Animator anim;
	Rigidbody2D rb2d;
	[SerializeField]
	GameObject tiro,tiro2;
	[SerializeField]
	AudioSource[] sons;
	[SerializeField] 
	Renderer personagem;
	bool loadingShot=false;

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

	void Awake(){
		canMove=true;
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
		//tiro = GameObject.FindGameObjectWithTag("tiro");

		currentState = (int)State.Stand;
		side = -1f;
		startingState = false;
		stateTimer = 0;

		dashing = false;
		dashSide = 0f;
		dashTimer = 0;
	}

	void Start () {

	}

	void Update () {
		/*if(loadingShot){
			Invoke("ChangeColor",0.2f);
			if(shotSound==false &&shotCount>=0.3f){
				sons[1].Play();
				shotSound=true;
			}
			else{
				if(shotSound2==false && !sons[1].isPlaying &&shotCount>=0.3f){
					sons[2].Play();
					shotSound2=true;
				}
			}
		}
		else if(cor){
			personagem.material.color = new Color(1f,1f,1f,1f);
			cor=false;
		}*/
		///STAND
		if (currentState == (int)State.Stand && canMove)

		{
			side = Input.GetAxisRaw ("Horizontal");

			stateTimer++;

			if (Input.GetAxisRaw ("Horizontal") == -1f || Input.GetAxisRaw ("Horizontal") == 1f)
			{
				side = Input.GetAxisRaw ("Horizontal");
				currentState = (int)State.Move;
				stateTimer = 0;
			}

			if (Input.GetKeyDown(KeyCode.X))
			{
				currentState = (int)State.Jump;
				stateTimer = 0;
			}

			if (Input.GetKeyDown(KeyCode.Z))
			{
				currentState = (int)State.StandShoot;
				sons[0].Play();
				Instantiate(tiro,new Vector3(transform.localPosition.x,transform.localPosition.y,transform.localPosition.z),Quaternion.identity);
				stateTimer = 0;
			}
			if(Input.GetKey(KeyCode.Z)){
				shotCount+=Time.deltaTime;
				loadingShot=true;
				Invoke("ChangeColor",0.2f);
				if(shotSound==false &&shotCount>=0.3f){
					sons[1].Play();
					shotSound=true;
				}
				else{
					if(shotSound2==false && !sons[1].isPlaying &&shotCount>=0.3f){
						sons[2].Play();
						shotSound2=true;
					}
				}
			}
			else if(Input.GetKeyUp(KeyCode.Z)){
				currentState = (int)State.StandShoot;
				sons[2].Stop();
				sons[0].Play();
				loadingShot=false;
				cor=false;
				colorCount=0;
				Invoke("ChangeColor",0.01f);
				shotSound=false;
				shotSound2=false;
				if(shotCount>0){
					Instantiate(tiro2,new Vector3(transform.localPosition.x,
						transform.localPosition.y,transform.localPosition.z),Quaternion.identity);
					shotCount=0;
				}
			}

			if (!VerificaChao.checkGround(transform.position))
			{
				currentState = (int)State.Fall;
				stateTimer = 0;
			}

			if (dashing)
			{
				currentState = (int)State.Dash;
				stateTimer = 0;
			}
		}


		///MOVE
		else if (currentState == (int)State.Move  && canMove)

		{
			stateTimer++;

			side = Input.GetAxisRaw ("Horizontal");

			if (Input.GetAxisRaw ("Horizontal") == 0f)
			{
				currentState = (int)State.Stand;
				stateTimer = 0;
			}

			if (Input.GetKeyDown(KeyCode.X))
			{
				currentState = (int)State.Jump;
				stateTimer = 0;
			}

			if (Input.GetKeyDown(KeyCode.Z))
			{
				Instantiate(tiro,new Vector3(transform.localPosition.x,transform.localPosition.y,transform.localPosition.z),Quaternion.identity);
				sons[0].Play();
				currentState = (int)State.MoveShoot;
				stateTimer = 0;
			}

			if (dashing)
			{
				currentState = (int)State.Dash;
				stateTimer = 0;
			}

			if (!VerificaChao.checkGround(transform.position))
			{
				currentState = (int)State.Fall;
				stateTimer = 0;
			}
		}



		///JUMP
		else if (currentState == (int)State.Jump  && canMove)

		{
			stateTimer++;

			side = Input.GetAxisRaw ("Horizontal");

			if (VerificaChao.checkGround(transform.position))
			{
				currentState = (int)State.Stand;
				stateTimer = 0;
			}

			if (dashing)
			{
				currentState = (int)State.Dash;
				stateTimer = 0;
			}

			if (Input.GetKeyDown(KeyCode.Z))
			{
				currentState = (int)State.JumpShoot;
				stateTimer = 0;
			}

			if ((WallCheck.leftWall (transform.position) && Input.GetKey (KeyCode.LeftArrow)) || (WallCheck.rightWall (transform.position) && Input.GetKey (KeyCode.RightArrow)))
			{
				currentState = (int)State.WallSliding;
			}

		}


		///FALL
		else if (currentState == (int)State.Fall  && canMove) 

		{
			stateTimer++;

			side = Input.GetAxisRaw ("Horizontal");

			if (VerificaChao.checkGround(transform.position))
			{
				currentState = (int)State.Stand;
				stateTimer = 0;
			}

			if (dashing)
			{
				currentState = (int)State.Dash;
				stateTimer = 0;
			}

			if (Input.GetKeyDown(KeyCode.Z))
			{
				currentState = (int)State.JumpShoot;
				stateTimer = 0;
			}

			if ((WallCheck.leftWall (transform.position) && Input.GetKey (KeyCode.LeftArrow)) || (WallCheck.rightWall (transform.position) && Input.GetKey (KeyCode.RightArrow)))
			{
				currentState = (int)State.WallSliding;
			}

		}

		///DASH
		else if (currentState == (int)State.Dash  && canMove)

		{

			dashing = false;

			stateTimer++;

			if (stateTimer > 20)
			{
				currentState = (int)State.Stand;
				stateTimer = 0;
			}
		}


		///STAND-SHOOT
		else if (currentState == (int)State.StandShoot  && canMove)

		{
			stateTimer++;

			if (stateTimer > 20)
			{
				currentState = (int)State.Stand;
				stateTimer = 0;
			}
		}

		///JUMP-SHOOT
		else if (currentState == (int)State.JumpShoot  && canMove)

		{
			side = Input.GetAxisRaw ("Horizontal");

			stateTimer++;

			if (stateTimer > 20)
			{
				currentState = (int)State.Fall;
				stateTimer = 0;
			}

			if (VerificaChao.checkGround(transform.position))
			{
				currentState = (int)State.Stand;
				stateTimer = 0;
			}
		}

		///MOVE-SHOOT
		else if (currentState == (int)State.MoveShoot  && canMove)

		{
			side = Input.GetAxisRaw ("Horizontal");

			stateTimer++;

			if (stateTimer > 20)
			{
				currentState = (int)State.Move;
				stateTimer = 0;
			}

			if (!VerificaChao.checkGround(transform.position))
			{
				currentState = (int)State.Fall;
				stateTimer = 0;
			}
		}

		///WALL-SLIDING
		else if (currentState == (int)State.WallSliding  && canMove)

		{
			stateTimer++;

			if (Input.GetKeyDown(KeyCode.X))
			{
				if (WallCheck.leftWall (transform.position))
				{
					rb2d.AddForce(new Vector2(600f,100f));
				}
				else if (WallCheck.rightWall (transform.position))
				{
					rb2d.AddForce(new Vector2(-600f,100f));
				}
				currentState = (int)State.Jump;
				stateTimer = 0;
			}

			if (WallCheck.leftWall (transform.position) && Input.GetKeyUp(KeyCode.LeftArrow))
			{
				currentState = (int)State.Fall;
				stateTimer = 0;
			}

			if (WallCheck.rightWall (transform.position) && Input.GetKeyUp(KeyCode.RightArrow))
			{
				currentState = (int)State.Fall;
				stateTimer = 0;
			}

			if (VerificaChao.checkGround(transform.position))
			{
				currentState = (int)State.Stand;
				stateTimer = 0;
			}

			if (!WallCheck.rightWall (transform.position) && !WallCheck.leftWall (transform.position))
			{
				currentState = (int)State.Fall;
				stateTimer = 0;
			}
		}

		if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow)) && dashTimer <= 0)
		{
			dashTimer = 40;
			dashSide = Input.GetAxisRaw ("Horizontal");
		}

		if (dashTimer > 0)
		{
			dashTimer--;

			if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
			{
				if (Input.GetAxisRaw ("Horizontal") == dashSide  && dashTimer <= 35)
				{
					dashing = true;
				}

			}
		}
		else
		{
			dashSide = 0f;
		}

		if (stateTimer == 0) 
		{
			startingState = true;
		}
		else
		{
			startingState = false;
		}

		anim.SetInteger ("state", currentState);

		if (side != 0f)
		{
			transform.localScale = new Vector2 (4f * side, transform.localScale.y);
		}
		//DEATH
		else if(currentState==(int)State.Death){
			
		}
	}
	void ChangeColor(){
		if(colorCount>=1f){
			personagem.material.color = new Color(0.29f,1f,1f,1f);
			cor=true;
		}
		else{
			personagem.material.color = new Color(1f,1f,1f,1f);
			cor=false;
		}
		if(cor){
			colorCount-=5f*Time.deltaTime;
		}
		else{
			colorCount+=5f*Time.deltaTime;
		}
	}
}
