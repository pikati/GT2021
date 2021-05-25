using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private bool isReach = false;
    private ClearCount clearCount;
    private GameObject emitter;
    private bool isGoal = false;

    private void Start()
    {
        emitter = transform.GetChild(1).gameObject;
        emitter.SetActive(false);
    }
    private void Update()
    {

        transform.Rotate(0, 120 * Time.deltaTime, 0);
        clearCount = GameObject.Find("GoalUI").GetComponent<ClearCount>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isReach) return;
        if (isGoal) return;
        if(other.CompareTag("Player"))
        {
            isReach = true;
            Singleton<ClearChecker>.Instance.ReachChechkPoint();
            clearCount.ChangeState(ClearCount.ImageState.Visible);
            transform.GetChild(0).gameObject.SetActive(false);
            emitter.SetActive(true);
            Singleton<SoundManager>.Instance.PlaySeByName("get");
            Destroy(gameObject, 5.0f);
            isGoal = true;
            Singleton<NavMeshBaker>.Instance.Bake();
        }
    }
}
