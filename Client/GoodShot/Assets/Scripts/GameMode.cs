using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMode : MonoBehaviour {
    
    public static GameMode Instance
    {
        get { return instance; }
    }
    static GameMode instance;

    [SerializeField]
    ProtoPlayer player;
    [SerializeField]
    ProtoBall ball;

    public delegate void OnGameEvent();
    public OnGameEvent OnGameStart;
    public OnGameEvent OnShot;
    public OnGameEvent OnBallStop;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        GameStart();
    }

    public void GameStart()
    {
        if(OnGameStart != null)
        {
            OnGameStart();
        }

        ball.onBallStop = () =>
        {
            Debug.Log("ball stopped.");

            if(OnBallStop != null)
            {
                OnBallStop();
            }
        };
    }

    
    public void OnShotButtonClicked(float value)
    {
        player.ShootBall(value);
        if(OnShot != null)
        {
            OnShot();
        }
    }

    public void OnResetButtonClicked()
    {
        Reset();
        ball.Reset();

        GameStart();
    }

    private void Reset()
    {
        Debug.Log("Game Reset");
    }
}
