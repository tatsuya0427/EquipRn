using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerController : MonoBehaviour{

	[SerializeField]protected GameObject mainGameSceneManager;

	[SerializeField]protected int maxHP = 1;
	[SerializeField]protected float defaultSpeed = 0;
	
	
	[SerializeField]Transform[] groundCheckTransforms = null;//地面への設置判定を行うobjectを格納するlist

	[SerializeField]protected int defaultPower = 0;

	public bool isActive = false; 
	
	//--------------------
	//playerが現在装備しているActionについて格納しておく変数群
	private ActionUI nowSetAction;//現在装備しているactionの操作方法
	//--------------------

	//--------------------
	//ジャンプの処理に使用する変数群
	[SerializeField]protected float jumpPower = 10.0f;
	[SerializeField]protected float secondJumpPower = 10.0f;

	[SerializeField]int canJumpCount = 2;//地面についてからジャンプできる最大回数
	[SerializeField]bool isGround = false;//現在接地しているかどうか
	[SerializeField]int remJumpCount = 0;//残り何回ジャンプできるか
	//--------------------

	
	//-------------------
	private float axisV;
	//--------------------

	//--------------------
	private float axisH;
	//--------------------

	protected int hp = 1;
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

	protected Animator anim;
	protected SpriteRenderer srender;
	protected Rigidbody2D rbody;

	public int EditHp
	{
		set
		{
			hp = Mathf.Clamp(hp - value, 0, maxHP);
			if(hp <= 0)
			{
				Dead();//死亡判定
			}

		}
		get
		{
			return hp;
		}
	}

	public float EditSpeed
	{
		set
		{
			speed = value;
		}
		get
		{
			return speed;
		}
	}

	public int EditPower
	{
		set
		{
			power = Mathf.Max(value, 0);
		}
		get
		{
			return power;
		}
	}

	public float EditJumpPower
	{
		set
		{
			this.jumpPower = value;
		}
		get
		{
			return this.jumpPower;
		}
	}

	public float EditSecondJumpPower
	{
		set
		{
			this.secondJumpPower = value;
		}
		get
		{
			return this.secondJumpPower;
		}
	}

	public float EditAxisH
	{
		set
		{
			this.axisH = value;
		}
		get
		{
			return this.axisH;
		}
	}

	public float EditAxisV
	{
		set
		{
			this.axisV = value;
		}
		get
		{
			return this.axisV;
		}
	}

	public bool GetIsGround
	{
		get
		{
			return this.isGround;
		}
	}

	public int EditRemJumpCount
	{
		set
		{
			remJumpCount = value;
		}
		get
		{
			return this.remJumpCount;
		}
	}

	public int EditCanJumpCount
	{
		set
		{
			this.canJumpCount = value;
		}
		get
		{
			return this.canJumpCount;
		}
	}

	public Rigidbody2D GetRbody
	{
		get
		{
			return this.rbody;
		}
	}

	public ActionUI EditNowSetAction
	{
		set
		{
			this.nowSetAction = value;
		}
		get
		{
			return this.nowSetAction;
		}
	}

	public void SetUp(ActionUI firstSetAction)//初期設定
	{
		ChangeAction(firstSetAction);

		anim = GetComponent<Animator>();
		srender = GetComponent<SpriteRenderer>();
		Debug.Log(srender);
		rbody = GetComponent<Rigidbody2D>();

		InitCharacter();//Playerのステータスを初期設定に戻す機構
	}

	public void ChangeAction(ActionUI targetAction)//装備しているActionUIclassの変更
	{
		EditNowSetAction = targetAction;
		this.axisH = 0;
	}

    protected virtual void Update()
	{
		DamageTimer();//被弾時の無敵時間や入力受付制限の制御
    }

	protected virtual void FixedUpdate()
	{
		FixedUpdateCharacter();//playerの描画に関する関数
		DamageAnimation();//ダメージを受けた際の点滅処理
		if(this.nowSetAction != null)
		{
			this.nowSetAction.ActionsUpdate();
		}
	}

	protected virtual void FixedUpdateCharacter()//playerの描画に関する関数
	{
		Move();//playerの動きに関して
	}

	public void InputUp(bool inputGetButtonDown)
	{
		if(!isActive || notMoveFlag)
		{
			return;
		}
		this.nowSetAction.PushUp(inputGetButtonDown);
	}

	public void InputDown(bool inputGetButtonDown)
	{
		if(!isActive || notMoveFlag)
		{
			return;
		}
		this.nowSetAction.PushDown(inputGetButtonDown);
	}

	public void InputLeft(bool inputGetButtonDown)
	{
		if(!isActive || notMoveFlag)
		{
			return;
		}
		this.nowSetAction.PushLeft(inputGetButtonDown);
	}

	public void InputRight(bool inputGetButtonDown)
	{
		if(!isActive || notMoveFlag)
		{
			return;
		}
		this.nowSetAction.PushRight(inputGetButtonDown);
	}

	public void InputJump(bool inputGetButtonDown)
	{
		if(!isActive || notMoveFlag)
		{
			return;
		}
		this.nowSetAction.PushJump(inputGetButtonDown);
	}

	protected virtual void InitCharacter()//Playerのステータスを初期設定に戻す
	{
		this.hp = maxHP;
		EditSpeed = defaultSpeed;
		//Debug.Log("active");
		isActive = true;
	}

	protected virtual void Move()
	{
		if(!isActive)
		{
			return;
		}
		GroundCheck();//接地判定

		//実際の移動処理
		rbody.velocity = new Vector2(speed * axisH, rbody.velocity.y);
	}

	void GroundCheck()//playerの接地判定
	{
		Collider2D[] groundCheckCollider = new Collider2D[groundCheckTransforms.Length];
		
		//接地判定オブジェクトが何かに重なっているかどうかをチェック
		for (int i = 0; i < groundCheckTransforms.Length; i++)
		{
			groundCheckCollider[i] = Physics2D.OverlapPoint(groundCheckTransforms[i].position);

			//接地判定オブジェクトのうち、1つでも何かに重なっていたら接地しているものとして終了
			if (groundCheckCollider[i] != null)
			{
				isGround = true;
				remJumpCount = canJumpCount;
				return;
			}
		}
		//ここまできたということは何も重なっていないということなので、接地していないと判断する
		isGround = false;
  	}

	protected void OnCollisionEnter2D(Collision2D collision)//敵との当たり判定制御
	{
		if(collision.gameObject.CompareTag("enemy"))
		{
			Damage(collision.collider.transform.position);
			this.EditAxisH = 0;
		}
	}

	protected virtual void Damage(Vector3 damagePos)//ダメージを受けた際の数値処理やノックバック処理
	{
		if(this.damageFlag)
		{
			return;
		}

		EditHp = 1;
		damageFlag = true;
		notMoveFlag = true;

		Vector2 damageForce;
		Vector2 distination = (transform.position - damagePos).normalized;

		//playerとenemyの位置関係からどちらの方向へ吹っ飛ぶか判断する制御
		if(distination.x < 0)
		{
			damageForce = new Vector2(-3f, 1.5f);
			
		}
		else
		{
			damageForce = new Vector2(3f, 1.5f);
		}
		transform.DOMove(damageForce, 0.3f);
		//transform.position = Vector2.MoveTowards(transform.position, damageForce, 2.0f*Time.deltaTime);

		if(EditHp > 0)
		{
			this.nowDamageCoolTime = 0;
		}
	}

	protected virtual void DamageTimer()//被弾時の無敵時間や入力受付制限の制御
	{
		if(this.nowDamageCoolTime >= this.damageCoolTime)
		{
			this.damageFlag = false;
			this.notMoveFlag = false;
		}
		else if(this.nowDamageCoolTime >= this.damageCoolTime / 2)
		{
			this.notMoveFlag = false;
		}

		if(this.nowDamageCoolTime < this.damageCoolTime)
		{
			this.nowDamageCoolTime += Time.deltaTime;
		}
	}

	protected virtual void Dead()//playerが死亡した際の処理
	{
		Debug.Log("Dead!!!!!!");
		if(this.mainGameSceneManager != null)
		{
			this.notMoveFlag = false;
			this.mainGameSceneManager.GetComponent<MainGameSceneManager>().GameOver();
		}
		else
		{
			Debug.Log("not set MainGameSceneManager");
		}
	}

	void UpdateAnimation()
	{
		//anim.SetBool("Grounded", isGround);
	}

	protected void DamageAnimation()//ダメージを受けた際の点滅処理
	{
		if(this.damageFlag)
		{
			this.damageFlash = Mathf.Abs(Mathf.Sin(Time.time * 10));
			damageColor = new Color(1f, 1f, 1f, this.damageFlash);
			srender.color = damageColor;
		}
		else
		{
			srender.color = new Color(1f, 1f, 1f, 1f);
		}
	}
}
