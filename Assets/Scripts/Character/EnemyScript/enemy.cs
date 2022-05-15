using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour{
    [SerializeField]protected float attackLimit;//敵の攻撃間隔
    protected GameObject targetPlayer;//対象となるplayerを格納しておく変数
    private float nowTime = 0f;//経過時間を保持しておく変数
    protected Rigidbody2D rbody;
    protected Vector2 jumpPower = new Vector2(5.0f, 5.0f);

    public bool IsActive => gameObject.activeSelf;

    protected SpriteRenderer srender;
    protected Color drawColor;

    public enum EnemyState{
        OffScene,//画面外にいる状態
        ActiveIdle,//画面内に描画されている時の待機状態
        Attack//攻撃状態にある状態
    }

    void Start(){
        this.rbody = GetComponent<Rigidbody2D>();
        srender = GetComponent<SpriteRenderer>();
        targetPlayer = GameObject.FindWithTag("Player");
    }

    void FixedUpdate(){
        if(nowTime > attackLimit){
            switch(Random.Range(0, 3)){
                case 0:
                    jumpPower.x = 0;
                break;
                case 1:
                    jumpPower.x = -1 * jumpPower.x;
                break;
                case 2:
                    jumpPower.x = 1 * jumpPower.x;
                break;
		    }
            this.rbody.velocity = jumpPower;
            nowTime = 0f;
        }else{
            nowTime += Time.deltaTime;
        }
    }

    //オブジェクトプール用の、呼び出された際にマイフレーム呼び出す処理
     public void onUpdate(){
    //     if(nowTime > attackLimit){
    //         switch(Random.Range(0, 3)){
    //             case 0:
    //                 jumpPower.x = 0;
    //             break;
    //             case 1:
    //                 jumpPower.x = -1 * jumpPower.x;
    //             break;
    //             case 2:
    //                 jumpPower.x = 1 * jumpPower.x;
    //             break;
	// 	    }
    //         this.rbody.velocity = jumpPower;
    //         nowTime = 0f;
    //     }else{
    //         nowTime += Time.deltaTime;
    //     }
     }

    protected void OnCollisionEnter2D(Collision2D collision){
		if(collision.gameObject.CompareTag("playerAttack")){
			Destroy(this.gameObject);
            ScoreManager.instance.EditScore = 10;
		}
	}

    public void ForwardDraw(){
        this.drawColor = new Color(1f, 1f, 1f, 1f);
        srender.color = this.drawColor;
    }

    public void BackDraw(){
        this.drawColor = new Color(0.3f, 0.3f, 0.3f, 1f);
        srender.color = this.drawColor;
    }
}
