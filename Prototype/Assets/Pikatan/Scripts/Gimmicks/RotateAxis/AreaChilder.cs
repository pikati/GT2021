using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaChilder : MonoBehaviour
{
    private GameObject parent;
    public bool IsActive { get; set; } = false;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.root.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (!IsActive) return;
        if(other.CompareTag("PanelArea"))
        {
            other.gameObject.transform.parent = parent.transform;
        }
    }
}
