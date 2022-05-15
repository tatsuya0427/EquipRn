using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour{

    [SerializeField]protected GameObject player;//playerを格納して、PlayerControllerを扱う。
    [SerializeField]protected string jumpKeyName = "Jump";//JumpKeyにて使用するキーの名称(Jumpならスペースキー)
    [SerializeField]protected List<ActionUI> actions = new List<ActionUI>();//装備していactionをlistで管理する
    [SerializeField]protected List<GameObject> actionIcons = new List<GameObject>();//actionごとに表示するアイコンを管理する

    
    //private List<ActinonUI> actionComp = new List<ActionUI>();

    //-----------------
    //表示を変更するUIボタンを格納しておく変数群
    [SerializeField]protected GameObject buttonUp;
    [SerializeField]protected GameObject buttonDown;
    [SerializeField]protected GameObject buttonRight;
    [SerializeField]protected GameObject buttonLeft;
    [SerializeField]protected GameObject buttonJump;

    //-----------------

    //-----------------
    //UIボタンのコンポーネントを保持しておく変数群
    private PushButtonActive buttonUpComp;
    private PushButtonActive buttonDownComp;
    private PushButtonActive buttonRightComp;
    private PushButtonActive buttonLeftComp;
    private PushButtonActive buttonJumpComp;
    //-----------------

    private PlayerController playerComp;//playerControllerのコンポーネントを格納しておく変数
    
    private ActionUI actionUIComp;//現在装備しているActionUIのコンポーネントを格納しておく

    private MoveActionIcon moveActionIconComp;//装備しているActionに対応したUIのコンポーネントを格納しておく。

    private int refActionNomber = 0;//list内から、現在装備しているActionUIの番号を格納する

    private float axisH = 0;//左右の入力値を保存しておく
    private float axisV = 0;//上下の入力値を保存しておく



    //-----------------
    //現在装備中のActionで、どのキーが使用できるのか管理する変数群
    private bool CanUseUpKey = false;
    private bool CanUseDownKey = false;
    private bool CanUseLeftKey = false;
    private bool CanUseRightKey = false;
    private bool CanUseJumpKey = false;
    //-----------------

    //現在入力を受け付けるかどうかの判定を行う変数
    private bool canPlayerControl = false;
    public bool EditCanPlayerControl
    {
        set
        {
            this.canPlayerControl = value;
        }
        get
        {
            return this.canPlayerControl;
        }
    }

    void Start(){//playerの初期装備に合わせてプレイヤーとボタン設定の設定を行う。
        this.refActionNomber = 0;//初期装備としてactionsの0番目を取得する。
        this.canPlayerControl = false;
        //-----------------
        //最初に装備しているActionUIのsetPlayerの実行と、このActionUIに設定されているCanUse(...)Keyの設定を取得する
        this.playerComp = player.GetComponent<PlayerController>();
        this.actionUIComp = this.actions[this.refActionNomber];

        this.moveActionIconComp = this.actionIcons[this.refActionNomber].GetComponent<MoveActionIcon>();
        this.moveActionIconComp.Set();//画面上に現在装備しているアイコンを表示する

        this.actionUIComp.PlayerSet(player);
        this.CanUseUpKey = this.actionUIComp.GetCanPush(ActionUI.ActionKey.UP);
        this.CanUseDownKey = this.actionUIComp.GetCanPush(ActionUI.ActionKey.DOWN);
        this.CanUseLeftKey = this.actionUIComp.GetCanPush(ActionUI.ActionKey.LEFT);
        this.CanUseRightKey = this.actionUIComp.GetCanPush(ActionUI.ActionKey.RIGHT);
        this.CanUseJumpKey = this.actionUIComp.GetCanPush(ActionUI.ActionKey.JUMP);
        //-----------------

        //playerに現在の装備クラスを代入する
        this.playerComp.SetUp(this.actionUIComp);

        //-----------------ボタンの表示変更
        this.buttonUpComp = buttonUp.GetComponent<PushButtonActive>();
        this.buttonUpComp.ChangeState(this.CanUseUpKey);

        this.buttonDownComp = buttonDown.GetComponent<PushButtonActive>();
        this.buttonDownComp.ChangeState(this.CanUseDownKey);

        this.buttonRightComp = buttonRight.GetComponent<PushButtonActive>();
        this.buttonRightComp.ChangeState(this.CanUseRightKey);

        this.buttonLeftComp = buttonLeft.GetComponent<PushButtonActive>();
        this.buttonLeftComp.ChangeState(this.CanUseLeftKey);

        this.buttonJumpComp = buttonJump.GetComponent<PushButtonActive>();
        this.buttonJumpComp.ChangeState(this.CanUseJumpKey);
        //-----------------
    }

    void SetAction(ActionUI nextSetAction){//playerが装備するものに合わせてプレイヤーとボタン設定の設定を行う。
        Debug.Log(nextSetAction.GetName());
        //this.actionUIComp = nextSetAction.GetComponent<ActionUI>();
        this.actionUIComp.Remove();//現在装備している装備を外す際の処理を実行する
        this.actionUIComp = nextSetAction;

        this.moveActionIconComp.Remove();//画面上に現在装備しているアイコンを外に移動する
        this.moveActionIconComp = this.actionIcons[this.refActionNomber].GetComponent<MoveActionIcon>();
        this.moveActionIconComp.Set();//画面上に現在装備しているアイコンを表示する

        //装備しているActionUIのsetPlayerの実行と、このActionUIに設定されているCanUse(...)Keyの設定を取得する
        this.actionUIComp.PlayerSet(player);
        this.CanUseUpKey = this.actionUIComp.GetCanPush(ActionUI.ActionKey.UP);
        this.CanUseDownKey = this.actionUIComp.GetCanPush(ActionUI.ActionKey.DOWN);
        this.CanUseLeftKey = this.actionUIComp.GetCanPush(ActionUI.ActionKey.LEFT);
        this.CanUseRightKey = this.actionUIComp.GetCanPush(ActionUI.ActionKey.RIGHT);
        this.CanUseJumpKey = this.actionUIComp.GetCanPush(ActionUI.ActionKey.JUMP);
        //-----------------

        //playerに現在の装備クラスを代入する
        this.playerComp.ChangeAction(this.actionUIComp);

        //-----------------ボタンの表示変更
        this.buttonUpComp.ChangeState(this.CanUseUpKey);
        this.buttonDownComp.ChangeState(this.CanUseDownKey);
        this.buttonRightComp.ChangeState(this.CanUseRightKey);
        this.buttonLeftComp.ChangeState(this.CanUseLeftKey);
        this.buttonJumpComp.ChangeState(this.CanUseJumpKey);
        //-----------------
    }

    void Update()
    {
        if(this.canPlayerControl)//各キーの入力処理を管理する。
        {
            if(this.CanUseRightKey || this.CanUseLeftKey)
            {
                this.axisH = Input.GetAxisRaw("Horizontal");

                //playerComp.EditAxisH = this.axisH;

                if(this.axisH > 0 && this.CanUseRightKey)
                {
                    this.playerComp.InputRight(true);//右キーを押した際の処理を起動
                    this.playerComp.InputLeft(false);//左キーの処理を中断

                    this.buttonRightComp.PushKey(true);//右キーを押した際のUIの変更
                    this.buttonLeftComp.PushKey(false);//左キーのUIをリセット

                }
                else if(this.axisH < 0 && this.CanUseLeftKey)
                {
                    this.playerComp.InputRight(false);
                    this.playerComp.InputLeft(true);

                    this.buttonRightComp.PushKey(false);
                    this.buttonLeftComp.PushKey(true);

                }
                else
                {
                    this.playerComp.InputRight(false);
                    this.playerComp.InputLeft(false);

                    this.buttonRightComp.PushKey(false);
                    this.buttonLeftComp.PushKey(false);

                    playerComp.EditAxisH = this.axisH;    
                }
            }

            if(this.CanUseUpKey || this.CanUseDownKey)
            {
                this.axisV = Input.GetAxisRaw("Vertical");
                //playerComp.EditAxisV = this.axisV;

                if(this.axisV > 0 && this.CanUseUpKey)
                {
                    this.playerComp.InputUp(true);//上キーを押した際の処理を起動
                    this.playerComp.InputDown(false);//下キーの処理を中断

                    this.buttonUpComp.PushKey(true);//上キーを押した際のUIの変更
                    this.buttonDownComp.PushKey(false);//下キーのUIをリセット

                }
                else if(this.axisV < 0 && this.CanUseDownKey)
                {
                    this.playerComp.InputUp(false);
                    this.playerComp.InputDown(true);

                    this.buttonUpComp.PushKey(false);
                    this.buttonDownComp.PushKey(true);

                }
                else
                {
                    this.playerComp.InputUp(false);
                    this.playerComp.InputDown(false);

                    this.buttonUpComp.PushKey(false);
                    this.buttonDownComp.PushKey(false);

                    playerComp.EditAxisV = this.axisV;
                }
            }

            if(this.CanUseJumpKey)
            {
                this.playerComp.InputJump(Input.GetButtonDown(jumpKeyName));//Jumpキーを押した時の処理を起動
                this.buttonJumpComp.PushKey(Input.GetButton(jumpKeyName));//Jumpキーを押した際のUIの変更
            }

            if(Input.GetKeyDown(KeyCode.C))//actionsに格納されているもの
            {
                if(this.refActionNomber + 1 < actions.Count)
                {
                    this.refActionNomber ++;
                    Debug.Log("next:" + this.refActionNomber);
                }
                else
                {
                    Debug.Log("return");
                    this.refActionNomber = 0;
                }

                SetAction(actions[this.refActionNomber]);
            }
        }
    }
}
