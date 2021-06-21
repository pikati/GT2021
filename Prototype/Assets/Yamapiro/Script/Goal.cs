using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private bool isReach = false;
    private ClearCount clearCount;
    [SerializeField]
    private GameObject emitter;
    private GameObject memory;
    private bool isGoal = false;
    private GoalChild goalChild1;
    private GoalChild goalChild2;

    private void Start()
    {
        emitter = transform.GetChild(1).gameObject;
        emitter.SetActive(false);
        memory = transform.Find("memori-kai").gameObject;
        goalChild1 = transform.Find("Col1").GetComponent<GoalChild>();
        goalChild2 = transform.Find("Col2").GetComponent<GoalChild>();
    }
    private void Update()
    {

        memory.transform.Rotate(60 * Time.deltaTime, 120 * Time.deltaTime, 30 * Time.deltaTime);
        clearCount = GameObject.Find("GoalUI").GetComponent<ClearCount>();
        if(goalChild1.IsGoal && goalChild2.IsGoal)
        {
            GoalFnac();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isReach) return;
        if (isGoal) return;
        if(other.CompareTag("Player"))
        {
            GoalFnac();
        }
    }
    private void GoalFnac()
    {
        isReach = true;
        Singleton<ClearChecker>.Instance.ReachChechkPoint();
        //常時出し続ける仕様変更のため
        //clearCount.ChangeState(ClearCount.ImageState.Visible);
        transform.GetChild(0).gameObject.SetActive(false);
        emitter.SetActive(true);
        goalChild1.gameObject.SetActive(false);
        goalChild2.gameObject.SetActive(false);
        Singleton<SoundManager>.Instance.PlaySeByName("get");
        Destroy(gameObject, 1.5f);
        isGoal = true;
        Singleton<NavMeshBaker>.Instance.Bake();
    }
}
