using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerSuitsuki : MonoBehaviour
{
    [SerializeField]
    private GameObject sprite;
    [SerializeField]
    private GameObject crossSprite;
    private GameObject parentObj;
    private PointerAnimationController pac;
    private RotatePoint rp;
    
    void Start()
    {
        parentObj = transform.root.gameObject;
        pac = transform.root.GetComponent<PointerAnimationController>();
        sprite.SetActive(false);
        crossSprite.SetActive(false);
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
            pac.FocusPointer();
            sprite.SetActive(true);
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
                if (rp.OnPlayer)
                {
                    crossSprite.SetActive(true);
                }
                else
                {
                    crossSprite.SetActive(false);
                }
                if(!sprite.activeSelf)
                {
                    sprite.SetActive(true);
                }
                pac.FocusPointer();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("RotatePoint"))
        {
            sprite.SetActive(false);
            crossSprite.SetActive(false);
            pac.ExitPointer();
        }
    }

    public void SetSelectObject(RotatePoint point)
    {
        rp = point;
    }
}
