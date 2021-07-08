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

    private GameObject particle;

    //UIの場所に動かす
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private Vector3 startPoint;
    [SerializeField]
    private Vector3 endPoint;

    private void Start()
    {
        emitter = transform.GetChild(1).gameObject;
        emitter.SetActive(false);
        memory = transform.Find("memor_kai2").gameObject;
        particle = transform.Find("GoalParticle").gameObject;
        goalChild1 = transform.Find("Col1").GetComponent<GoalChild>();
        goalChild2 = transform.Find("Col2").GetComponent<GoalChild>();

        memoryState = MEMORY_STATE.WAIT;
        spinTimer = new GameTimer(timer);

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        //endPoint = new Vector3(0.0f, 1.0f, 5.0f);
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
                //memory.transform.position += new Vector3(0.0f, 10.0f * Time.deltaTime, 0.0f);

                Vector2 rate = MemoryEasing(spinTimer.TimeRate);
                Vector3 nowPos = new Vector3(Mathf.Lerp(startPoint.x, endPoint.x, rate.x),
                                             Mathf.Lerp(startPoint.y, endPoint.y, rate.y),
                                             Mathf.Lerp(startPoint.z, endPoint.z, rate.x));

                memory.transform.position = mainCamera.ViewportToWorldPoint(nowPos);
                memory.transform.Rotate(0.0f, 500000.0f * Time.deltaTime, 0.0f);

                particle.transform.position = memory.transform.position;
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
        transform.GetChild(0).gameObject.SetActive(false);
        goalChild1.gameObject.SetActive(false);
        goalChild2.gameObject.SetActive(false);
        particle.gameObject.SetActive(false);
        Singleton<ClearChecker>.Instance.ReachChechkPoint();    //ここでUI出してる
        //常時出し続ける仕様変更のため
        //clearCount.ChangeState(ClearCount.ImageState.Visible);
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
            startPoint = mainCamera.WorldToViewportPoint(memory.transform.position);
            endPoint = Singleton<ClearCount>.Instance.GetActiveIconPosition();
            particle.GetComponent<ParticleSystem>().Play();
        }
        else if (next == MEMORY_STATE.END)
        {
            memoryState = MEMORY_STATE.END;
            GoalFnac();
        }
    }

    private Vector2 MemoryEasing(float rate)
    {
        float x = rate;
        float y = rate;

        //x
        x = x * x;

        //y
        y = y * y * y * y;

        //if (y < 0.5)
        //{
        //    y = 4 * y * y * y;
        //}
        //else
        //{
        //    y = 1 - Mathf.Pow(-2.0f * y + 2.0f, 3.0f) / 2;
        //}

        return new Vector2(x, y);
    }
}
