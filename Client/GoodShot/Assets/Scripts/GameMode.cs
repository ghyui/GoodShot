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
    ProtoPlayer player;
    [SerializeField]
    ProtoBall ball;

    Coroutine sliderAnimation;

    private void Start()
    {
        GameStart();
    }

    public void GameStart()
    {
        sliderAnimation = StartCoroutine(SliderAnimation());
    }

    public void OnShotButtonClicked()
    {
        StopCoroutine(sliderAnimation);

        player.ShootBall(slider.value);
    }

    public void OnResetButtonClicked()
    {
        ball.Reset();

        GameStart();
    }

    IEnumerator SliderAnimation()
    {
        while (true)
        {
            slider.value = Mathf.Abs(Mathf.Sin(Time.realtimeSinceStartup * sliderAniSpeed));
            yield return null;
        }
    }
}
