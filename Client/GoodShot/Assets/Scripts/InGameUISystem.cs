using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InGameUISystem : MonoBehaviour {

    public static InGameUISystem Instance
    {
        get { return instance; }
    }
    static InGameUISystem instance;

    [SerializeField]
    float sliderAniSpeed = 2.0f;
    [SerializeField]
    Slider slider;
    [SerializeField]
    Text shotCountText;
    [SerializeField]
    Button shotButton;
    [SerializeField]
    Button rotateRightButton;
    [SerializeField]
    Button rotateLeftButton;
    [SerializeField]
    GameObject controlObject;

    Coroutine sliderAnimation;

    int shotCount;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {
        shotCount = 0;

        GameMode.Instance.OnGameStart += () =>
            {
                ReadyToShoot();
            };

        GameMode.Instance.OnBallStop += () =>
            {
                ReadyToShoot();
            };

        shotButton.onClick.AddListener(() => OnShotButtonClicked());
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

        if(!GameMode.Instance.Ball.IsShooting ^ controlObject.activeInHierarchy)
        {
            controlObject.SetActive(GameMode.Instance.Ball.IsShooting);
        }

    }

    void ReadyToShoot()
    {
        StartSliderAnimation();
        controlObject.SetActive(true);
    }

    void OnShotButtonClicked()
    {
        StopSliderAnimation();

        shotCount++;

        controlObject.SetActive(false);

        GameMode.Instance.OnShotButtonClicked(slider.value);
    }

    void OnResetButtonClicked()
    {
        Reset();

        GameMode.Instance.OnResetButtonClicked();
    }

    void Reset()
    {
        shotCount = 0;

        if (sliderAnimation != null)
        {
            StopSliderAnimation();
        }
    }

    void StartSliderAnimation()
    {
        sliderAnimation = StartCoroutine(SliderAnimation());
    }

    IEnumerator SliderAnimation()
    {
        while (true)
        {
            slider.value = Mathf.Abs(Mathf.Sin(Time.realtimeSinceStartup * sliderAniSpeed));
            yield return null;
        }
    }

    void StopSliderAnimation()
    {
        Debug.Log("Stop Slider Animation");
        StopCoroutine(sliderAnimation);
    }
}
