using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalController : MonoBehaviour
{
    private enum ElecState
    {
        Active,
        InActive
    };

    private GameObject elecObj;
    private ElecState elecState;
    private AxisStateController asc;
    private bool isActive = false;

    void Start()
    {
        elecObj = transform.Find("ElecArea").gameObject;
        asc = GameObject.Find("AxisStateController").GetComponent<AxisStateController>();
    }

    private void Update()
    {
        if (!isActive) return;
        if (asc.IsRotate) return;
        ChangeState(ElecState.Active);
        isActive = false;
    }
    private void ChangeState(ElecState state)
    {
        if(state != elecState)
        {
            elecState = state;
            if (elecState == ElecState.Active)
            {
                elecObj.SetActive(true);
                Singleton<NavMeshBaker>.Instance.Bake();
            }
            else
            {
                elecObj.SetActive(false);
                Singleton<NavMeshBaker>.Instance.Bake();
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (asc.IsRotate) return;
        if(other.CompareTag("Obstacle"))
        {
            ChangeState(ElecState.InActive);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (asc.IsRotate) return;
        if (other.CompareTag("Obstacle"))
        {
            ChangeState(ElecState.InActive);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            isActive = true;
        }
    }
}
