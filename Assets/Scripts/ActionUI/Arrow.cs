using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    protected void OnCollisionEnter2D(Collision2D collision){
		if(collision.gameObject.CompareTag("enemy") || collision.gameObject.CompareTag("outer")){
			Destroy(this.gameObject);

		}
	}
}
