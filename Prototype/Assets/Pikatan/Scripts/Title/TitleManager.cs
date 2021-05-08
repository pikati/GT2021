using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    private Fade fade;
    // Start is called before the first frame update
    void Start()
    {
        fade = Singleton<Fade>.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        if (fade.isFading) return;
        fade.FadeIn("LevelSelect");
    }

    public void Option()
    {
        if (fade.isFading) return;
    }

    public void ExitGame()
    {
        if (fade.isFading) return;
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
      UnityEngine.Application.Quit();
#endif
    }
}
