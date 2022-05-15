using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapRange : MonoBehaviour
{
    [SerializeField]private GameObject EffectTarget = default;

    private enemy enemyComp;

    void Awake(){
        enemyComp = EffectTarget.GetComponent<enemy>();
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.CompareTag("Player")){
            enemyComp.BackDraw();
        }
    }

    void OnTriggerExit2D(Collider2D collider){
        if(collider.gameObject.CompareTag("Player")){
            enemyComp.ForwardDraw();
        }
    }
}
