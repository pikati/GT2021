using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalEmitter : MonoBehaviour
{
    [SerializeField]
    private float maxScale;
    private Vector3 scale;
    // Start is called before the first frame update
    void Start()
    {
        scale = transform.localScale * maxScale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, scale, Time.deltaTime * 2.5f);
        if(Mathf.Abs(transform.localScale.y - scale.y) < 0.05f)
        {
            scale *= 0.1f;
        }
    }
}
