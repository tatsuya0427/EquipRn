using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    protected void OnCollisionEnter2D(Collision2D collision){
		if(collision.gameObject.CompareTag("Player")){
			ScoreManager.instance.EditScore += 50;
            Destroy(this.gameObject);
		}
	}
}
