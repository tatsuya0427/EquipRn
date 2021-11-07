﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    [SerializeField]protected GameObject inputManager;
    public static ScoreManager instance = null;


    //----------------score関係    
    public int score;
    public int highScore;
    //----------------
    
    public int EditScore{
        set{
            this.score += value;
        }get{
            return this.score;
        }
    }

    public int EditHighScore{
        set{
            if(this.highScore < value){
                this.highScore = value;
            }
        }get{
            return this.highScore;
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
