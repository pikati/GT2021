using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalChild : MonoBehaviour
{
    private bool isGoal = false;
    public bool IsGoal { 
        get 
        { return isGoal; }
        private set
        { IsGoal = value; }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isGoal = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isGoal = false;
        }
    }
}
