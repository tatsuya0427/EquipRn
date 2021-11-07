using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordAction : ActionUI{

    private PlayerController playerComp;
    private GameObject playerObject;

    private bool nowSet = false;//現在このActionが装備されているかどうか判定する変数
    private GameObject moveSlash;//方向キーを押した際にターゲットを移動させるObjectを格納
    protected SpriteRenderer slashRender;
    private Vector3 targetPos;//今のtargetがどこにいるか保持しておく変数
    private Vector3 arrowVec;//arrowが放たれる方角を保持しておく変数
    private bool canSlash = true;//現在slashが打てるかどうか
    private float nowDelayTime = 0f;//現在のdelay経過時間を保持しておく
    private bool nowDelay = false;//現在slashにDelayがかかっているかどうかを保持しておく


    //------------------------
    private int targetAxisH = 0;//targetがplayerからx軸方向にずれている値を保持する変数
    //------------------------

    [SerializeField]protected GameObject targetOrigin;//targetを生成する時元となるObjectを格納
    [SerializeField]protected GameObject slashOrigin;//targetを生成する時元となるObjectを格納
    [SerializeField]protected float slashDelay = 0f;//slashを打った後に再度打てるようになるまでの感覚
    [SerializeField]protected float slashJudgment = 0f;//slashの猶予期間


    // void Start(){
    //     SetState("sword", false, false, true, true, true);
    // }

    public override void PlayerSet(GameObject player){
        SetState("sword", false, false, true, true, true);
        this.playerComp = player.GetComponent<PlayerController>();
        this.playerObject = player;

        this.moveSlash = Instantiate(this.slashOrigin, this.playerObject.transform.position, Quaternion.identity);
        this.slashRender = this.moveSlash.GetComponent<SpriteRenderer>();
        this.slashRender.color = new Color(1f,1f,1f,0f);

        this.nowSet = true;
        if(this.nowSet){
            this.targetPos = this.playerObject.transform.position;
            if(this.playerObject.transform.localScale.x > 0){
                this.targetAxisH = -1;
            }else if(this.playerObject.transform.localScale.x < 0){
                this.targetAxisH = 1;
            }

            this.targetPos = new Vector3(this.targetPos.x + this.targetAxisH, this.targetPos.y, this.targetPos.z);
            this.moveSlash.transform.position = this.targetPos;
        }
    }

    public override void ActionsUpdate(){
        if(this.nowSet){

            if(!this.canSlash){
                if(this.nowDelayTime >= this.slashDelay){
                    this.canSlash = true;
                    this.nowDelay = false;
                    Debug.Log("can");
                }else if(this.nowDelayTime >= this.slashJudgment){
                    this.slashRender.color = new Color(1f,1f,1f,0f);
                    Debug.Log("stopJudge");
                    this.nowDelayTime += Time.deltaTime;
                }else{
                    this.nowDelayTime += Time.deltaTime;
                }
            }

            this.targetPos = this.playerObject.transform.position;
            if(this.playerObject.transform.localScale.x > 0){
                this.targetAxisH = -1;
            }else if(this.playerObject.transform.localScale.x < 0){
                this.targetAxisH = 1;
            }

            this.targetPos = new Vector3(this.targetPos.x + this.targetAxisH, this.targetPos.y, this.targetPos.z);
            this.moveSlash.transform.position = this.targetPos;
        }
    }

    public override void Remove(){
        Destroy(this.moveSlash);
        this.nowSet = false;
    }

    public override void PushUp(bool inputGetButtonDown){
        if(inputGetButtonDown){
            
        }
    }

    public override void PushDown(bool inputGetButtonDown){
        if(inputGetButtonDown){
            
        }
    }

    public override void PushLeft(bool inputGetButtonDown){
        if(inputGetButtonDown && !nowDelay){
            playerObject.transform.localScale = new Vector3(0.4f, 0.4f, 1.0f);
            this.playerComp.EditAxisH = -1;
        }
    }

    public override void PushRight(bool inputGetButtonDown){
        if(inputGetButtonDown && !nowDelay){
            playerObject.transform.localScale = new Vector3(-0.4f, 0.4f, 1.0f);
            this.playerComp.EditAxisH = 1;
        }
    }

    

    public override void PushJump(bool inputGetButtonDown){
        if(inputGetButtonDown){
            if(this.canSlash){
                this.slashRender.color = new Color(1f,1f,1f,1f);
                this.nowDelayTime = 0;
                this.nowDelay = true;
                this.canSlash = false;
            }
        }
    }
}
