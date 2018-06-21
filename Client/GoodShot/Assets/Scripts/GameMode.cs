using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMode : MonoBehaviour {

    [SerializeField]
    float sliderAniSpeed = 2.0f;
    [SerializeField]
    Slider slider;
    [SerializeField]
    Text shotCountText;
    [SerializeField]
    Button shotButton;

    [SerializeField]
    ProtoPlayer player;
    [SerializeField]
    ProtoBall ball;

    Coroutine sliderAnimation;
    int shotCount;

    private void Start()
    {
        GameStart();
    }

    public void GameStart()
    {
        shotCount = 0;
        ReadyToShoot();

        ball.onBallStop = () =>
        {
            Debug.Log("ball stopped.");

            ReadyToShoot();
        };
    }

    void ReadyToShoot()
    {
        StartSliderAnimation();
        shotButton.enabled = true;
    }

    void StartSliderAnimation()
    {
        sliderAnimation = StartCoroutine(SliderAnimation());
    }

    void StopSliderAnimation()
    {
        Debug.Log("Stop Slider Animation");
        StopCoroutine(sliderAnimation);
    }

    IEnumerator SliderAnimation()
    {
        while (true)
        {
            slider.value = Mathf.Abs(Mathf.Sin(Time.realtimeSinceStartup * sliderAniSpeed));
            yield return null;
        }
    }

    public void OnShotButtonClicked()
    {
        StopSliderAnimation();

        player.ShootBall(slider.value);
        shotCount++;

        shotButton.enabled = false;
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
        if(sliderAnimation != null)
        {
            StopSliderAnimation();
        }
    }

    private void Update()
    {
        UIUpdate();
    }

    private void UIUpdate()
    {
        if (shotCountText.text != shotCount.ToString())
        {
            shotCountText.text = shotCount.ToString();
        }
    }
}
