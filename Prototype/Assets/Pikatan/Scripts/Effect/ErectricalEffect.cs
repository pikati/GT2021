using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErectricalEffect : MonoBehaviour
{
    ErectricalEffectManager eem;

    private void Start()
    {
        eem = transform.root.gameObject.transform.GetChild(0).GetComponent<ErectricalEffectManager>();
    }

    private void OnParticleCollision(GameObject other)
    {
        eem.OnCollisionErecParticle();
    }
}
