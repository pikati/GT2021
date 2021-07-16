using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalBakeController : MonoBehaviour
{
    private int elecNum = -1;
    private int updateElecNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        var elec = GameObject.FindGameObjectsWithTag("Electrical");
        elecNum = elec.Length;
    }

    public void Bake()
    {
        if(++updateElecNum == elecNum)
        {
            Singleton<NavMeshBaker>.Instance.Bake();
            updateElecNum = 0;
        }
    }
}
