using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameSceneManager : MonoBehaviour
{

    [SerializeField]protected GameObject player;
    [SerializeField]protected GameObject inputManager;
    [SerializeField]protected GameObject countDown;

    private InputManager inputManagerComp;
    private ShowCountDown countDownComp;

    private bool playerLive = true;
    private bool startGameFlow = false;

    public bool EditPlayerLive
    {
        set 
        {
            this.playerLive = value;
            if(this.inputManagerComp != null && !this.playerLive)
            {
                this.inputManagerComp.EditCanPlayerControl = false;
            }
        }
        get
        {
            return this.playerLive;
        }
    }

    public bool EditStartGameFlow
    {
        set
        {
            this.startGameFlow = value;
        }
        get
        {
            return this.startGameFlow;
        }
    }

    private void Awake()
    {
        this.playerLive = true;
        this.startGameFlow = false;
        this.inputManagerComp = this.inputManager.GetComponent<InputManager>();
        this.countDownComp = this.countDown.GetComponent<ShowCountDown>();
        this.countDownComp.setUp();
    }

    void Update()
    {
        if(this.startGameFlow)
        {

        }
    }

    public void CountZero()
    {
        this.inputManagerComp.EditCanPlayerControl = true;

    }
}
