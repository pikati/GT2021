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

    void Start()
    {
        elecObj = transform.Find("ElectArea").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        if(other.CompareTag("Obstacle"))
        {
            ChangeState(ElecState.InActive);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            ChangeState(ElecState.InActive);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            ChangeState(ElecState.Active);
        }
    }
}
