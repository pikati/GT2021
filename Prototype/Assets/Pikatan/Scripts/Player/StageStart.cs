using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageStart : Singleton<StageStart>
{
    [SerializeField]
    private float speed;
    GameObject player;
    Vector3 startPosition;
    public bool IsEnd { get; private set; } = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        startPosition = player.transform.position;
        player.transform.position += new Vector3(0, 10, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsEnd) return;
        player.transform.position = Vector3.Lerp(player.transform.position, startPosition, Time.deltaTime * speed);
        if(Mathf.Abs(player.transform.position.y - startPosition.y) < 0.5f)
        {
            player.transform.position = startPosition;
            IsEnd = true;
            player.GetComponent<PlayerMove>().ActivateAgent();
            GameObject.FindGameObjectWithTag("AxisPointer").GetComponent<AxisPointer>().ActivatePointer();
        }
    }
}
