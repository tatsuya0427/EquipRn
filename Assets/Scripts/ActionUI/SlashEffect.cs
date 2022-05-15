using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashEffect : MonoBehaviour{
    protected SpriteRenderer slashRender;

    void Start(){
        this.slashRender = this.GetComponent<SpriteRenderer>();
        this.slashRender.color = new Color(1f,1f,1f,1f);
    }
    void Update(){
        this.slashRender.color -= new Color(1f,1f,1f,Time.deltaTime*10);
        if(this.slashRender.color.a <= 0)
        {
            Invoke("Delete", 0.3f);
        }
    }

    void Delete(){
        Destroy(this.gameObject);
    }
}
