using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErectricalEffectManager : MonoBehaviour
{
    [SerializeField]
    private GameObject obj;
    // Start is called before the first frame update
    
    public void OnCollisionErecParticle()
    {
        obj.SetActive(false);
        Invoke("ResetParticleLifeTime", 0.5f);
    }

    private void ResetParticleLifeTime()
    {
        obj.SetActive(true);
    }
}
