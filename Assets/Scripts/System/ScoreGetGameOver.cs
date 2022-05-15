using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreGetGameOver : MonoBehaviour
{
    private Text scoreText = null;
    private int oldScore = 0;
    void Start()
    {
        scoreText = GetComponent<Text>();
        if(ScoreManager.instance != null){
            Debug.Log(ScoreManager.instance.EditScore);
            scoreText.text = "" + ScoreManager.score;
        }else{
            Debug.Log("ScoreManager is not found");
            Destroy(this);
        }
    }
}
