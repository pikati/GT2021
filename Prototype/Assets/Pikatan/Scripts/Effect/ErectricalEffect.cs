using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErectricalEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject eemObj;
    private ErectricalEffectManager eem;

    private void Start()
    {
        eem = eemObj.GetComponent<ErectricalEffectManager>();
    }

    private void OnParticleCollision(GameObject other)
    {
        eem.OnCollisionErecParticle();
    }
}
