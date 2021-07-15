using UnityEngine;

/// <summary>
/// スクリーンショットをキャプチャするサンプル
/// </summary>
public class ScreenShotCapturer : MonoBehaviour
{
    private int id = -1;
    private void Awake()
    {
        id = GameObject.Find("StageNumController").GetComponent<StageNumController>().StageNum;
    }
    private void Update()
    {
        // スペースキーが押されたら
        if (Singleton<InputController>.Instance.Y)
        {

            // スクリーンショットを保存
            CaptureScreenShot("D:/Unity/GT2021/Git/Prototype/Assets/Master/Sprites/SS/" + id + ".png");
        }
    }

    // 画面全体のスクリーンショットを保存する
    private void CaptureScreenShot(string filePath)
    {
        ScreenCapture.CaptureScreenshot(filePath);
        Debug.Log("success" + id);
    }
}