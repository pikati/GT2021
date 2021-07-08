using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectUIController : MonoBehaviour
{
    [SerializeField]
    private int maxPage;
    [SerializeField]
    private float pageChangeSpeed;
    //[SerializeField]
    //private List<GameObject> cursors;
    [SerializeField]
    private RectTransform cursor;
    [SerializeField]
    private Animator LBAnim;
    [SerializeField]
    private Animator RBAnim;
    [SerializeField]
    private int stageNum;
    private InputController ic;
    private bool isInput = false;
    private int index = 0;
    private int page = 0;
    private StageName stageName;
    private float diff = 0;
    private bool isChangePage = false;
    private RectTransform rt;
    private bool isSelected = false;
    private Vector3[] pos;
    public int StageNum => stageNum;
    public int Idx => index + page * 10;

    // Start is called before the first frame update
    void Start()
    {
        //for (int i = 1; i < 10; i++)
        //{
        //    DisableCursor(i);
        //}
        ic = Singleton<InputController>.Instance;
        stageName = new StageName();
        rt = GetComponent<RectTransform>();
        SetCursorPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelected) return;

        if (ic.MoveValue.x > 0.7f || ic.ArrowValue.x > 0)
        {
            if (isInput) return;
            ChangeCursor(1);
        }
        else if (ic.MoveValue.x < -0.7f || ic.ArrowValue.x < 0)
        {
            if (isInput) return;
            ChangeCursor(-1);
        }
        else if (ic.MoveValue.y > 0.7f || ic.ArrowValue.y > 0)
        {
            if (isInput) return;
            ChangeCursor(-5);
        }
        else if (ic.MoveValue.y < -0.7f || ic.ArrowValue.y < 0)
        {
            if (isInput) return;
            ChangeCursor(5);
        }
        else if (ic.RB)
        {
            if (isInput) return;
            RBAnim.SetTrigger("OnPlay");
            ChangePage(1);
        }
        else if (ic.LB)
        {
            if (isInput) return;
            LBAnim.SetTrigger("OnPlay");
            ChangePage(-1);
        }
        else
        {
            isInput = false;
        }
        if (ic.A)
        {
            Singleton<SoundManager>.Instance.PlaySeByName("decide");
            ChangeSecne(stageName.StageNames[index + page * 10]);
            isSelected = true;
        }
        MovePage();
    }

    private void ChangeCursor(int n)
    {
        isInput = true;
        int tmp = index;
        index += n;
        if (index < 0)
        {
            if(page > 0)
            {
                index = 9;
                ChangePage(-1);
            }
            else
            {
                index = tmp;
            }
        }
        else if (index >= 10)
        {
            if (page < maxPage)
            {
                index -= 10;
                ChangePage(1);
                if(index >= 10)
                {
                    ChangeCursor(0);
                }
            }
            else
            {
                index = tmp;
            }
        }
        else if(index + page * 10 >= stageNum)
        {
            index = tmp;
        }
        else
        {
            Singleton<SoundManager>.Instance.PlaySeByName("cursor");
        }
        //EnableCursor(index);
        MoveCursor(index);
    }

    private void ChangePage(int n)
    {
        isInput = true;
        int prePage = page;
        page += n;
        if(page < 0)
        {
            page = 0;
        }
        if(page > maxPage)
        {
            page = maxPage;
        }
        
        if (page > prePage)
        {
            diff += -2000.0f;
            isChangePage = true;
            Singleton<SoundManager>.Instance.PlaySeByName("cursor");
        }
        else if(page < prePage)
        {
            diff += 2000.0f;
            isChangePage = true;
            Singleton<SoundManager>.Instance.PlaySeByName("cursor");
        }
        if (index + page * 10 >= stageNum)
        {
            //DisableCursor(index);
            //index = 0;
            //EnableCursor(index);
            index = 0;
            MoveCursor(index);
        }
    }

    private void MovePage()
    {
        if (!isChangePage) return;
        Vector3 pos = rt.localPosition;
        pos.x = diff;
        rt.localPosition = Vector3.Lerp(rt.localPosition, pos, Time.deltaTime * pageChangeSpeed);
        if(Mathf.Abs(transform.position.x - pos.x) < 0.01f)
        {
            isChangePage = false;
        }
    }

    //private void EnableCursor(int index)
    //{
    //    cursors[index].GetComponent<Image>().enabled = true;
    //    cursors[index].GetComponent<CursorAnimation>().StartAniamtion();
    //}

    //private void DisableCursor(int index)
    //{
    //    cursors[index].GetComponent<Image>().enabled = false;
    //}

    private void MoveCursor(int index)
    {
        SetCursorPosition();
        cursor.localPosition = pos[index];
    }

    private void SetCursorPosition()
    {
        if(pos == null)
        {
            pos = new Vector3[10];
            pos[0] = new Vector3(-900, -700 + 540, 0);
            pos[1] = new Vector3(-550, -700 + 540, 0);
            pos[2] = new Vector3(-200, -700 + 540, 0);
            pos[3] = new Vector3(150, -700 + 540, 0);
            pos[4] = new Vector3(500, -700 + 540, 0);
            pos[5] = new Vector3(-900, -950 + 540, 0);
            pos[6] = new Vector3(-550, -950 + 540, 0);
            pos[7] = new Vector3(-200, -950 + 540, 0);
            pos[8] = new Vector3(150, -950 + 540, 0);
            pos[9] = new Vector3(500, -950 + 540, 0);
        }
    }

    public void ResetCursor()
    {
        index = 0;
        MoveCursor(index);
        //EnableCursor(0);
        //for (int i = 1; i < 10; i++)
        //{
        //    DisableCursor(i);
        //}
    }

    public void SetStageIndex(int idx)
    {
        ChangeCursor(idx);
    }

    private void ChangeSecne(string sceneName)
    {
        Singleton<SoundManager>.Instance.StopBgm();
        Singleton<SceneChanger>.Instance.SceneChange(sceneName);
    }
}
