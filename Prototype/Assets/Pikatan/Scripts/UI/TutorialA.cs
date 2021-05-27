using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialA : MonoBehaviour
{
    private GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        obj = transform.GetChild(0).gameObject;
        obj.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            obj.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            obj.SetActive(false);
        }
    }
}
