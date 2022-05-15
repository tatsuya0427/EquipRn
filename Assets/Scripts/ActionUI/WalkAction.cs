using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAction : ActionUI{


    private PlayerController playerComp;
    private GameObject playerObject;
    
    private GameObject createIcon;
    private MoveActionIcon iconComp;

    private bool keepPushJumpKey = false;

    private int remJumpCount = 0;

    public override void PlayerSet(GameObject player){
        SetState("walk", false, false, true, true, true);
        this.playerComp = player.GetComponent<PlayerController>();
        this.playerObject = player;
    }

    public override void Remove(){
        
    }

    public override void ActionsUpdate(){
        
    }


    public override void PushUp(bool inputGetButtonDown){
        if(inputGetButtonDown){}
    }

    public override void PushDown(bool inputGetButtonDown){
        if(inputGetButtonDown){}
    }

    public override void PushLeft(bool inputGetButtonDown){
        if(inputGetButtonDown){
            playerObject.transform.localScale = new Vector3(0.4f, 0.4f, 1.0f);
            this.playerComp.EditAxisH = -1;
        }
    }

    public override void PushRight(bool inputGetButtonDown){
        if(inputGetButtonDown){
            playerObject.transform.localScale = new Vector3(-0.4f, 0.4f, 1.0f);
            this.playerComp.EditAxisH = 1;
        }
    }

    public override void PushJump(bool inputGetButtonDown){
        if(inputGetButtonDown){
			if(this.keepPushJumpKey == false){
				this.keepPushJumpKey = true;

                playerComp.EditRemJumpCount = playerComp.EditRemJumpCount - 1;
                this.remJumpCount = playerComp.EditRemJumpCount;

                if(playerComp.GetIsGround || this.remJumpCount > 0){
                    if(!(this.remJumpCount >= playerComp.EditCanJumpCount - 1)){
                        playerComp.GetRbody.velocity = Vector3.up * playerComp.EditSecondJumpPower;//2段ジャンプ量の作成
                    }else{
                        playerComp.GetRbody.velocity = Vector3.up * playerComp.EditJumpPower;//ジャンプ量の作成
                    }
                }
			}
		}else{
			this.keepPushJumpKey = false;
		}
    }
}
