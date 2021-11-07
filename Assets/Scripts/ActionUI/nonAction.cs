using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nonAction : ActionUI{

    private PlayerController playerComp;
    private GameObject playerObject;

    void Start(){
        SetState("non", false, false, false, false, false);
    }

    public override void PlayerSet(GameObject player){
        this.playerComp = player.GetComponent<PlayerController>();
        this.playerObject = player;
    }

    public override void Remove(){
        
    }

    public override void ActionsUpdate()
    {
        
    }

    public override void PushUp(bool inputGetButtonDown){
        
    }

    public override void PushDown(bool inputGetButtonDown){
        
    }

    public override void PushRight(bool inputGetButtonDown){
        
    }

    public override void PushLeft(bool inputGetButtonDown){
        
    }

    public override void PushJump(bool inputGetButtonDown){
        
    }
}
