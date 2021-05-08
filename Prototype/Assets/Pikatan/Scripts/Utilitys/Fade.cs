using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fade : Singleton<Fade>
{
    [SerializeField]
    private float fadeTime;
    private string nextSceneName = null;

    private float fTime;

    private Canvas canvas;
    private Image image;

    private bool isFadeIn;
    private bool isFadeOut;

    private float alpha;
    private float speed = 1.0f;
    public bool isFading { get; private set; } = false;

    public delegate void EndFade();
    public event EndFade OnEndFade;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        image = transform.Find("FadeImage").GetComponent<Image>();
        isFadeIn = false;
        isFadeOut = false;
        alpha = 0.0f;
        fTime = fadeTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFadeIn) UpdateFadeIn();
        else if (isFadeOut) UpdateFadeOut();
        isFading = isFadeIn || isFadeOut;
    }

    private void UpdateFadeIn()
    {
        alpha += Time.deltaTime / fTime * speed;
        if (nextSceneName != null && alpha >= 1.0f)
        {
            SceneManager.LoadScene(nextSceneName);
        }
        if (alpha >= 1.2f)
        {
            isFadeIn = false;
            canvas.enabled = false;
            OnEndFade?.Invoke();//nullチェックして実行
            FadeOut();
        }

        image.color = new Color(0.0f, 0.0f, 0.0f, alpha);
    }

    private void UpdateFadeOut()
    {
        alpha -= Time.deltaTime / fTime * speed;

        if (alpha <= 0.0f)
        {
            isFadeOut = false;
            alpha = 0.0f;
        }

        image.color = new Color(0.0f, 0.0f, 0.0f, alpha);
    }

    public void FadeIn()
    {
        image.color = Color.clear;
        isFadeIn = true;
        alpha = 0.0f;
        speed = 1.0f;
    }

    public void FadeIn(string sceneName)
    {
        image.color = Color.clear;
        isFadeIn = true;
        alpha = 0.0f;
        nextSceneName = sceneName;
        speed = 1.0f;
    }

    public void FadeIn(float spd)
    {
        image.color = Color.clear;
        isFadeIn = true;
        alpha = 0.0f;
        nextSceneName = null;
        speed = spd;
    }

    public void FadeIn(string sceneName, float spd)
    {
        image.color = Color.clear;
        isFadeIn = true;
        alpha = 0.0f;
        nextSceneName = sceneName;
        speed = spd;
    }

    public void FadeOut()
    {
        image.color = Color.black;
        canvas.enabled = true;
        isFadeOut = true;
        alpha = 1.0f;
    }

    public void SetFade()
    {
        image.color = Color.black;
        canvas.enabled = true;
        alpha = 1.0f;
    }
}
