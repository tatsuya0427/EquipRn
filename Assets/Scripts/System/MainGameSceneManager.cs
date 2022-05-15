using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameSceneManager : MonoBehaviour
{

    [SerializeField]protected GameObject player;//プレイヤーオブジェクトの代入
    [SerializeField]protected GameObject inputManager;//キー入力を管理するオブジェクトの代入
    [SerializeField]protected GameObject countDown;//開始直後のカウントダウンオブジェクトの代入
    [SerializeField]protected GameObject createEnemy;//敵を生成するオブジェクトの代入

    private InputManager inputManagerComp;//inputManagerのコンポーネントを確保
    private ShowCountDown countDownComp;//countDownコンポーネントを確保

    private bool playerLive = true;
    private bool startGameFlow = false;

    private float nowTime = 0f;

    //プレイヤーが死亡した場合(playerLive = false)操作を無効にする
    public bool EditPlayerLive
    {
        set
        {
            this.playerLive = value;
            if(this.inputManagerComp != null && !this.playerLive){
                this.inputManagerComp.EditCanPlayerControl = false;
            }
        }
        get
        {
            return this.playerLive;
        }
    }

    //このクラスのUpdate関数を動かすかどうかを制御する
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

    private void Awake()//このゲームの初期設定を行う。
    {
        this.playerLive = true;
        this.startGameFlow = false;
        this.inputManagerComp = this.inputManager.GetComponent<InputManager>();
        this.countDownComp = this.countDown.GetComponent<ShowCountDown>();
        this.countDownComp.setUp();
        Time.timeScale = 1.0f;
        ScoreManager.score = 0;
    }

    void Update()
    {
        if(this.startGameFlow){//敵を一定時間ごとに生成する機構
            if(this.nowTime > 3f)
            {
                Vector2 pos = new Vector2(Random.Range(-3.6f, 12.7f), 6f);
                int lange = Random.Range(1, 3);
                for(int i = 0; i < lange; i++)
                {
                    if(this.createEnemy != null)
                    {
                        Instantiate(this.createEnemy, pos, Quaternion.identity);
                    }
                }
                this.nowTime = 0;
            }
            else
            {
                this.nowTime += Time.deltaTime;
            }
        }
    }

    public void CountZero()//カウントダウン処理が終わった際に処理を行う。
    {
        this.inputManagerComp.EditCanPlayerControl = true;
        this.startGameFlow = true;

    }

    public void GameOver()//ゲームオーバー時に処理を行う。
    {
        this.playerLive = false;
        this.startGameFlow = false;
        Time.timeScale = 0.1f;
        Invoke("EndSceneLoad", 0.3f);//スロー演出のために0.3秒後にGameOverSceneを読み込む
    }

    public void EndSceneLoad()//ゲームオーバー時にシーンの変更を行う。
    {
        SceneManager.LoadScene("GameOverScene");
    }
}
