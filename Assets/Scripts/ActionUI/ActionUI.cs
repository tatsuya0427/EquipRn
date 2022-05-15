using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionUI : MonoBehaviour{
    private bool pushed;//外部からこのクラスで押せるボタンを参照するときに使用する変数
    protected string actionUIName;
    protected bool canPushUp, canPushDown, canPushLeft, canPushRight, canPushJump;

    public enum ActionKey{
        UP,DOWN,LEFT,RIGHT,JUMP
    }

    //このクラスで実装するaction名と、使用できるキーを設定する関数
    protected internal void SetState(string name, bool up, bool down, bool left, bool right, bool jump){
        this.actionUIName = name;
        this.canPushUp = up;
        this.canPushDown = down;
        this.canPushLeft = left;
        this.canPushRight = right;
        this.canPushJump = jump;
    }

    protected internal string GetName(){
        return this.actionUIName;
    }

    //このactionが各ボタンが使用可能かどうかを返却する関数
    public bool GetCanPush(ActionKey wantKey){
        
        switch(wantKey){
            case ActionKey.UP:
                pushed = this.canPushUp;
            break;

            case ActionKey.DOWN:
                pushed = this.canPushDown;
            break;

            case ActionKey.LEFT:
                pushed = this.canPushLeft;
            break;

            case ActionKey.RIGHT:
                pushed = this.canPushRight;
            break;

            case ActionKey.JUMP:
                pushed = this.canPushRight;
            break;
        }
        return pushed;
    }

    //このactionを装備した際に初期設定を行う処理
    abstract public void PlayerSet(GameObject player);

    //actionを変更した際に処理を行いたいものを記載する。
    abstract public void Remove();

    //このactionを装備している際に常に処理したい内容を記入する
    abstract public void ActionsUpdate();

    //上キーを押された際に行う処理を記載する
    abstract public void PushUp(bool inputGetButtonDown);

    //下キーを押された際に行う処理を記載する
    abstract public void PushDown(bool inputGetButtonDown);

    //左キーを押された際に行う処理を記載する
    abstract public void PushLeft(bool inputGetButtonDown);

    //右キーを押された際に行う処理を記載する
    abstract public void PushRight(bool inputGetButtonDown);

    //スペースキーを押された際に行う処理を記載する
    abstract public void PushJump(bool inputGetButtonDown);
}
