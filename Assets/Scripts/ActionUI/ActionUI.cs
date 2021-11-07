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

    abstract public void PlayerSet(GameObject player);

    abstract public void Remove();

    abstract public void ActionsUpdate();

    abstract public void PushUp(bool inputGetButtonDown);

    abstract public void PushDown(bool inputGetButtonDown);

    abstract public void PushLeft(bool inputGetButtonDown);

    abstract public void PushRight(bool inputGetButtonDown);

    abstract public void PushJump(bool inputGetButtonDown);
}
