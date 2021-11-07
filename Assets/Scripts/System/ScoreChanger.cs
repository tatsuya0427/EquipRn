using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreChanger : MonoBehaviour
{

    private Text scoreText = null;
    private int oldScore = 0;
    void Start()
    {
        scoreText = GetComponent<Text>();
        if(ScoreManager.instance != null){
            scoreText.text = "" + ScoreManager.instance.EditScore;
        }else{
            Debug.Log("ScoreManager is not found");
            Destroy(this);
        }
    }
    void Update()
    {
        if(oldScore != ScoreManager.instance.EditScore)
        {
            scoreText.text = "" + ScoreManager.instance.EditScore;
            oldScore = ScoreManager.instance.EditScore;
        }
    }
}
