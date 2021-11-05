using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowAction : ActionUI{

    private PlayerController playerComp;
    private GameObject playerObject;

    private bool nowSet = false;//現在このActionが装備されているかどうか判定する変数
    private GameObject moveTarget;//方向キーを押した際にターゲットを移動させるObjectを格納
    private Vector3 targetPos;//今のtargetがどこにいるか保持しておく変数
    private Vector3 arrowVec;//arrowが放たれる方角を保持しておく変数
    private bool canShot = true;//現在arrowが打てるかどうか
    private float nowDelayTime = 0f;//現在のdelay経過時間を保持しておく


    //------------------------
    private bool upFlag = false;//trueの間、targetが斜め上を向く
    private bool downFlag = false;//trueの間、targetが斜め下を向く
    private int targetAxisH = 0;//targetがplayerからx軸方向にずれている値を保持する変数
    private int targetAxisV = 0;//targetがplayerからy軸方向にずれている値を保持する変数
    //------------------------

    [SerializeField]protected GameObject targetOrigin;//targetを生成する時元となるObjectを格納
    [SerializeField]protected GameObject arrow;//jumpボタンを押した際に飛ばすObjectを格納
    [SerializeField]protected float arrowSpeed　= 5.0f;//arrowの速度を保持する変数
    [SerializeField]protected float shotDelay = 0f;//arrowを打った後に再度打てるようになるまでの感覚


    void Start(){
        SetState("arrow", true, true, true, true, true);
    }

    public override void PlayerSet(GameObject player){
        //SetState("arrow", true, true, true, true, true);
        this.playerComp = player.GetComponent<PlayerController>();
        this.playerObject = player;

        this.moveTarget = Instantiate(this.targetOrigin, this.playerObject.transform.position, Quaternion.identity);

        this.nowSet = true;
        if(this.nowSet){
            this.targetPos = this.playerObject.transform.position;
            if(this.playerObject.transform.localScale.x > 0){
                this.targetAxisH = -3;
            }else if(this.playerObject.transform.localScale.x < 0){
                this.targetAxisH = 3;
            }

            if(!((this.upFlag && this.downFlag) || (!this.upFlag && !this.downFlag))){
                if(this.upFlag){
                    this.targetAxisV = 3;
                }else if(this.downFlag){
                    this.targetAxisV = -3;
                }
            }

            this.targetPos = new Vector3(this.targetPos.x + this.targetAxisH, this.targetPos.y + targetAxisV, this.targetPos.z);
            this.moveTarget.transform.position = Vector3.MoveTowards(this.moveTarget.transform.position, this.targetPos, 3.0f);
        }
    }

    void Update(){
        if(this.nowSet){

            if(!this.canShot){
                if(this.nowDelayTime >= this.shotDelay){
                    this.canShot = true;
                }else{
                    this.nowDelayTime += Time.deltaTime;
                }
            }

            this.targetPos = this.playerObject.transform.position;
            if(this.playerObject.transform.localScale.x > 0){
                this.targetAxisH = -3;
            }else if(this.playerObject.transform.localScale.x < 0){
                this.targetAxisH = 3;
            }

            if(!((this.upFlag && this.downFlag) || (!this.upFlag && !this.downFlag))){
                if(this.upFlag){
                    this.targetAxisV = 2;
                }else if(this.downFlag){
                    this.targetAxisV = -2;
                }
            }else{
                this.targetAxisV = 0;
            }

            this.targetPos = new Vector3(this.targetPos.x + this.targetAxisH, this.targetPos.y + targetAxisV, this.targetPos.z);
            this.moveTarget.transform.position = Vector3.MoveTowards(this.moveTarget.transform.position, this.targetPos, 3.0f);
        }
    }

    public override void Remove(){
        Destroy(this.moveTarget);
        this.nowSet = false;
    }

    public override void PushUp(bool inputGetButtonDown){
        if(inputGetButtonDown){
            upFlag = true;
        }else{
            upFlag = false;
        }
    }

    public override void PushDown(bool inputGetButtonDown){
        if(inputGetButtonDown){
            downFlag = true;
        }else{
            downFlag = false;
        }
    }

    public override void PushLeft(bool inputGetButtonDown){
        if(inputGetButtonDown){
            playerObject.transform.localScale = new Vector3(0.4f, 0.4f, 1.0f);
        }
    }

    public override void PushRight(bool inputGetButtonDown){
        if(inputGetButtonDown){
            playerObject.transform.localScale = new Vector3(-0.4f, 0.4f, 1.0f);
        }
    }

    

    public override void PushJump(bool inputGetButtonDown){
        if(inputGetButtonDown){
            if(this.canShot){
                this.arrowVec = Vector3.Scale((this.moveTarget.transform.position - this.playerObject.transform.position), new Vector3(1, 1, 0)).normalized;
                Instantiate(this.arrow, this.playerObject.transform.position, Quaternion.identity).GetComponent<Rigidbody2D>().velocity = this.arrowVec * this.arrowSpeed;
                this.canShot = false;
                this.nowDelayTime = 0;
            }
            //GameObject cloneArrow = Instantiate(this.arrow, this.playerObject.transform.position, Quaternion.identity);
            //GameObject cloneArrow = Instantiate(this.arrow);
            //cloneArrow.transform.LookAt(this.moveTarget.transform);
            
            //cloneArrow.GetComponent<Rigidbody2D>().velocity = this.arrowVec * this.arrowSpeed;
        }
    //Instantiate(this.arrow, this.playerObject.transform.position, this.playerObject.rotation);
    }
}
