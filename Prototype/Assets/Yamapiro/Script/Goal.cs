using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    //演出状態
    enum MEMORY_STATE
    {
        WAIT = 0,
        SPIN,
        END,
    };

    private MEMORY_STATE memoryState;

    private bool isReach = false;
    private ClearCount clearCount;
    [SerializeField]
    private GameObject emitter;
    private GameObject memory;
    private bool isGoal = false;
    private GoalChild goalChild1;
    private GoalChild goalChild2;

    private GameTimer spinTimer;
    [SerializeField]
    private float timer;

    private void Start()
    {
        emitter = transform.GetChild(1).gameObject;
        emitter.SetActive(false);
        memory = transform.Find("memori-kai").gameObject;
        goalChild1 = transform.Find("Col1").GetComponent<GoalChild>();
        goalChild2 = transform.Find("Col2").GetComponent<GoalChild>();

        memoryState = MEMORY_STATE.WAIT;
        spinTimer = new GameTimer(timer);
    }
    private void Update()
    {
        //待機
        if (memoryState == MEMORY_STATE.WAIT)
        {
            memory.transform.Rotate(60 * Time.deltaTime, 120 * Time.deltaTime, 30 * Time.deltaTime);
        }
        //回転
        else if(memoryState == MEMORY_STATE.SPIN)
        {
            if(!spinTimer.IsTimeUp)
            {
                memory.transform.Rotate(0.0f, 500000.0f * Time.deltaTime, 0.0f);
                memory.transform.position += new Vector3(0.0f, 0.5f, 0.0f);
            }
            else
            {
                ChangeState(MEMORY_STATE.END);
            }

            spinTimer.UpdateTimer();
        }

        clearCount = GameObject.Find("GoalUI").GetComponent<ClearCount>();
        if(goalChild1.IsGoal && goalChild2.IsGoal && memoryState == MEMORY_STATE.WAIT)
        {
            ChangeState(MEMORY_STATE.SPIN);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isReach) return;
        if (isGoal) return;

        if(other.CompareTag("Player") && memoryState == MEMORY_STATE.WAIT)
        {
            ChangeState(MEMORY_STATE.SPIN);
        }
    }
    private void GoalFnac()
    {
        isReach = true;
        Singleton<ClearChecker>.Instance.ReachChechkPoint();    //ここでUI出してる
        //常時出し続ける仕様変更のため
        //clearCount.ChangeState(ClearCount.ImageState.Visible);
        transform.GetChild(0).gameObject.SetActive(false);
        goalChild1.gameObject.SetActive(false);
        goalChild2.gameObject.SetActive(false);
        Destroy(gameObject);
        Invoke("Singleton<NavMeshBaker>.Instance.Bake()", 0.1f);
    }

    private void ChangeState(MEMORY_STATE next)
    {
        if(next == MEMORY_STATE.WAIT)
        {
            Debug.Log("ここにきちゃだめー！(幼女)");
        }
        else if (next == MEMORY_STATE.SPIN)
        {
            memoryState = MEMORY_STATE.SPIN;
            emitter.SetActive(true);
            Singleton<SoundManager>.Instance.PlaySeByName("get");
            isGoal = true;
            memory.transform.Rotate(0.0f, 0.0f, 0.0f);
        }
        else if (next == MEMORY_STATE.END)
        {
            memoryState = MEMORY_STATE.END;
            GoalFnac();
        }
    }
}
