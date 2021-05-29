using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageNumController : MonoBehaviour
{
    [SerializeField]
    private int id;
    [SerializeField]
    private TextMeshProUGUI text;
    public int StageNum => id;

    private void Start()
    {
        text.text = "Stage" + id;
    }
}
