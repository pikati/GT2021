using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerSuitsuki : MonoBehaviour
{
    private GameObject parentObj;
    // Start is called before the first frame update
    void Start()
    {
        parentObj = transform.root.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("RotatePoint"))
        {
            Vector3 pos;
            pos.x = Mathf.Lerp(parentObj.transform.position.x, other.transform.position.x, 0.1f);
            pos.y = Mathf.Lerp(parentObj.transform.position.y, other.transform.position.y, 0.1f);
            pos.z = Mathf.Lerp(parentObj.transform.position.z, other.transform.position.z, 0.1f);
            parentObj.transform.position = pos;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("RotatePoint"))
        {
            var ic = Singleton<InputController>.Instance;
            if (Mathf.Abs(ic.RightStickValue.x) < 0.5 && Mathf.Abs(ic.RightStickValue.y) < 0.5)
            {
                Vector3 pos;
                pos.x = Mathf.Lerp(parentObj.transform.position.x, other.transform.position.x, 0.1f);
                pos.y = Mathf.Lerp(parentObj.transform.position.y, other.transform.position.y, 0.1f);
                pos.z = Mathf.Lerp(parentObj.transform.position.z, other.transform.position.z, 0.1f);
                parentObj.transform.position = pos;
            }
        }
    }
}
