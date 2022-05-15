using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCountDown : MonoBehaviour {
	[SerializeField]protected Text timerText;

	[SerializeField]protected float totalTime;

    [SerializeField]protected GameObject mainGameSceneManager;
    private float nowTime;
	private int seconds;
    private bool countStart = false;

    public void setUp(){
        nowTime = totalTime;
        countStart = true;
        timerText.text= ("ready...");
    }

	void Update () {
        if(countStart)
        {
            nowTime -= Time.deltaTime;
            seconds = (int)nowTime;
            if(seconds <= 0){
                timerText.text= ("start!!");
                countStart = false;
                this.mainGameSceneManager.GetComponent<MainGameSceneManager>().CountZero();
                Invoke("DelayErase", 1.0f);
                
            }else{
                timerText.text= seconds.ToString();
            }
        }
	}

    void DelayErase(){
        timerText.text = "";
    }
}