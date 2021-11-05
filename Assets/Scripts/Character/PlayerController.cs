using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour{

	[SerializeField]protected int maxHP = 1;
	[SerializeField]protected float defaultSpeed = 0;
	
	
	[SerializeField]Transform[] groundCheckTransforms = null;

	[SerializeField]protected int defaultPower = 0;

	public bool isActive = false; 
	
	//--------------------
	//playerが現在装備しているActionについて格納しておく変数群
	private ActionUI nowSetAction;//現在装備している操作方法
	//--------------------

	//--------------------
	//ジャンプの処理に使用する変数群
	//[SerializeField]protected string jumpKeyName = "Jump";
	[SerializeField]protected float jumpPower = 10.0f;
	[SerializeField]protected float secondJumpPower = 10.0f;
	//--------------------

	
	//-------------------
	private float axisV;
	//--------------------

	//--------------------
	private float axisH;
	//--------------------


	[SerializeField]int canJumpCount = 2;//地面についてからジャンプできる最大回数
	[SerializeField]bool isGround = false;
	[SerializeField]int remJumpCount = 0;//残り何回ジャンプできるか

	protected int hp = 0;
	protected float speed = 0;
	protected int power = 0;

	//--------------------
	[SerializeField]float damageCoolTime = 10;//ダメージを受けた際の無敵時間を設定する
	protected bool damageFlag = false;//ダメージを受けてから無敵時間が解除されるまでtrueになるflag
	protected bool notMoveFlag = false;//ダメージを受けてから次に操作を受け付けるようになるまでを管理するflag
	protected float nowDamageCoolTime = 0;//現在ダメージを受けてからどれぐらい経過したか格納しておく

	protected Color damageColor;//被弾時の点滅処理で使用するcolorClass
	protected float damageFlash;//被弾時の点滅処理で使用するalpha値
	//--------------------


	// protected GameObject gameManagerObj;
	// protected GameManager gameManager;
	protected Animator anim;
	protected SpriteRenderer srender;
	protected Rigidbody2D rbody;

	public int EditHp{
		set{
			hp = Mathf.Clamp(hp - value, 0, maxHP);

			if(hp <= 0){
				Dead();
			}

		}get{
			return hp;
		}
	}

	public float EditSpeed{
		set{
			speed = value;
		}get{
			return speed;
		}
	}

	public int EditPower{
		set{
			power = Mathf.Max(value, 0);
		}get{
			return power;
		}
	}

	public float EditJumpPower{
		set{
			this.jumpPower = value;
		}get{
			return this.jumpPower;
		}
	}

	public float EditSecondJumpPower{
		set{
			this.secondJumpPower = value;
		}get{
			return this.secondJumpPower;
		}
	}

	public float EditAxisH{
		set{
			this.axisH = value;
		}get{
			return this.axisH;
		}
	}

	public float EditAxisV{
		set{
			this.axisV = value;
		}get{
			return this.axisV;
		}
	}

	public bool GetIsGround{
		get{
			return this.isGround;
		}
	}

	public int EditRemJumpCount{
		set{
			remJumpCount = value;
		}get{
			return this.remJumpCount;
		}
	}

	public int EditCanJumpCount{
		set{
			this.canJumpCount = value;
		}get{
			return this.canJumpCount;
		}
	}

	public Rigidbody2D GetRbody{
		get{
			return this.rbody;
		}
	}

	public ActionUI EditNowSetAction{
		set{
			this.nowSetAction = value;
		}get{
			return this.nowSetAction;
		}
	}

    protected virtual void Start(){
		// srender = GetComponent<SpriteRenderer>();
		// Debug.Log(srender);
		// if(srender != null){
		// 	Damage();
		// }


		//
		//gameManagerObj = GameObject.FindGameObjectWithTag("GameController");
		//gameManager = gameManagerObj.GetComponent<GameManager>();

		// anim = GetComponent<Animator>();
		// srender = GetComponent<SpriteRenderer>();
		// rbody = GetComponent<Rigidbody2D>();

		// InitCharacter();
    }

	public void SetUp(ActionUI firstSetAction){
		ChangeAction(firstSetAction);

		anim = GetComponent<Animator>();
		srender = GetComponent<SpriteRenderer>();
		Debug.Log(srender);
		rbody = GetComponent<Rigidbody2D>();
		Debug.Log("hogehoge");

		//Damage();

		InitCharacter();
	}

	public void ChangeAction(ActionUI targetAction){
		EditNowSetAction = targetAction;
	}

    protected virtual void Update(){
        GetInput();
		DamageTimer();
		DamageAnimation();
		//UpdateAnimation();
    }

	protected virtual void FixedUpdate(){
		FixedUpdateCharacter();
	}

	protected virtual void FixedUpdateCharacter(){
		Move();
	}

	void GetInput(){
		if(!isActive){
			return;
		}

		// this.nowPushJumpKey = Input.GetButtonDown(GetJumpKeyName);
		// this.axisH = Input.GetAxisRaw("Horizontal");
		// this.axisV = Input.GetAxisRaw("Vertical");

		// if(this.nowPushJumpKey){
			
		// }


		// this.nowPushUpKey = GetInput.GetButtonDown();

		// if()

		//-------------
		// nowPushJumpKey = Input.GetButtonDown(jumpKeyName);
		// if(nowPushJumpKey){
		// 	if(keepPushJumpKey == false){
		// 		keepPushJumpKey = true;
		// 		remJumpCount --;
		// 		//Debug.Log("hoge");
		// 	}
		// }else{
		// 	keepPushJumpKey = false;
		// }
		//-------------
	
		// nowPushSpace = Input.GetButtonDown(jumpButtonName);
		// if(nowPushSpace){
		// 	if(pushSpaceKeyFlag == false){
		// 		pushSpaceKeyFlag = true;
		// 	}
		// }else{
		// 	pushSpaceKeyFlag = false;
		// }

		// axisH = Input.GetAxisRaw("Horizontal");
        // if(axisH > 0){
        //     transform.localScale = new Vector3(-0.4f, 0.4f, 1.0f);
        // }else if(axisH < 0){
        //     transform.localScale = new Vector3(0.4f, 0.4f, 1.0f);
        // }


	}

	public void InputUp(bool inputGetButtonDown){
		if(!isActive || notMoveFlag){
			return;
		}
		this.nowSetAction.PushUp(inputGetButtonDown);
	}

	public void InputDown(bool inputGetButtonDown){
		if(!isActive || notMoveFlag){
			return;
		}
		this.nowSetAction.PushDown(inputGetButtonDown);
	}

	public void InputLeft(bool inputGetButtonDown){
		if(!isActive || notMoveFlag){
			return;
		}
		this.nowSetAction.PushLeft(inputGetButtonDown);
	}

	public void InputRight(bool inputGetButtonDown){
		if(!isActive || notMoveFlag){
			return;
		}
		this.nowSetAction.PushRight(inputGetButtonDown);
	}

	public void InputJump(bool inputGetButtonDown){
		if(!isActive || notMoveFlag){
			return;
		}
		this.nowSetAction.PushJump(inputGetButtonDown);
	}

	protected virtual void InitCharacter(){
		EditHp = maxHP;
		EditSpeed = defaultSpeed;

		isActive = true;
	}

	protected virtual void Move(){
		if(!isActive){
			return;
		}
		GroundCheck();//接地判定

		//実際の移動処理
		rbody.velocity = new Vector2(speed * axisH, rbody.velocity.y);
	}

	void GroundCheck(){
		Collider2D[] groundCheckCollider = new Collider2D[groundCheckTransforms.Length];
		
		//接地判定オブジェクトが何かに重なっているかどうかをチェック
		for (int i = 0; i < groundCheckTransforms.Length; i++){
			groundCheckCollider[i] = Physics2D.OverlapPoint(groundCheckTransforms[i].position);

			//接地判定オブジェクトのうち、1つでも何かに重なっていたら接地しているものとして終了
			if (groundCheckCollider[i] != null){
				isGround = true;
				remJumpCount = canJumpCount;
				//jump = true;
				//canSecondJump = true;
				return;
			}
		}
		//ここまできたということは何も重なっていないということなので、接地していないと判断する
		isGround = false;
  	}

	protected void OnCollisionEnter2D(Collision2D collision){
		if(collision.gameObject.CompareTag("enemy")){
			Damage(collision.collider.transform.position.x);
		}
	}

	protected virtual void Damage(float positionX){
		if(this.damageFlag){
			return;
		}

		EditHp = (EditHp - 1);
		damageFlag = true;
		notMoveFlag = true;


		Vector2 forced;
        if(transform.position.x < positionX){//敵がplayerからみて前方、後方どちらから攻撃してきたか判別し、playerが攻撃された方とは逆向きに吹っ飛ばす処理
            //forced = Vector2.left;
			
			this.rbody.AddForce(transform.up * 400.0f);
			this.rbody.AddForce(transform.right * -400.0f);

        }else{
            //forced = Vector2.right;
			this.rbody.AddForce(transform.right * 400.0f);
			this.rbody.AddForce(transform.up * 400.0f);
        }
        //this.rbody.velocity = forced * 300;
		//this.rbody.velocity = Vector2.up * 50;

		if(EditHp > 0){
			this.nowDamageCoolTime = 0;
		}

		//playerComp.GetRbody.velocity = Vector3.up * playerComp.EditJumpPower;//ジャンプ量の作成
	}

	protected virtual void DamageTimer(){
		if(this.nowDamageCoolTime >= this.damageCoolTime){
			this.damageFlag = false;
			this.notMoveFlag = false;
		}else if(this.nowDamageCoolTime >= this.damageCoolTime / 2){
			this.notMoveFlag = false;
		}

		if(this.nowDamageCoolTime < this.damageCoolTime){
			this.nowDamageCoolTime += Time.deltaTime;
		}
	}

	protected virtual void Dead(){
		Debug.Log("Dead!!!!!!");
	}

	void UpdateAnimation(){
		//anim.SetBool("Grounded", isGround);
	}

	protected void DamageAnimation(){
		if(this.damageFlag){
			this.damageFlash = Mathf.Abs(Mathf.Sin(Time.time * 10));
			damageColor = new Color(1f, 1f, 1f, this.damageFlash);
			srender.color = damageColor;
		}else{
			srender.color = new Color(1f, 1f, 1f, 1f);
		}
	}
}
