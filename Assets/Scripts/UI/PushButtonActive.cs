using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PushButtonActive : MonoBehaviour{

    [SerializeField]protected Image buttonImage;
    [SerializeField]protected Sprite pushSprite;
    [SerializeField]protected Sprite idleSprite;
    [SerializeField]protected Sprite notUseSprite;

    private bool canUse = false;

    public void PushKey(bool nowPush){
        if(this.canUse){
            if(nowPush){
                buttonImage.sprite = pushSprite;
            }else{
                buttonImage.sprite = idleSprite;
            }
        }
    }

    public void ChangeState(bool use){
        this.canUse = use;
        if(this.canUse){
            buttonImage.sprite = idleSprite;
        }else{
            buttonImage.sprite = notUseSprite;
        }
    }
}
