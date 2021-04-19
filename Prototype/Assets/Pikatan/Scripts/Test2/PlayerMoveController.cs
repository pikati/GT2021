using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    #region SerializeField
    [SerializeField]
    private float speed = 1.0f;
    #endregion

    #region Field
    private List<Vector3> movePoints;
    private Vector3 targetPoint = Vector3.zero;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Panel");
        movePoints = new List<Vector3>();
        foreach(GameObject obj in objs)
        {
            movePoints.Add(obj.GetComponent<PointSetter>().MovePoint.Point);
        }
        SearchMovePoint();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if(MoveComplete())
        {
            SearchMovePoint();
        }
    }

    #region PrivateMethod
    private void Move()
    {
        transform.position = Vec3Lerp(targetPoint, transform.position, 0.0001f * speed);
    }

    private void SearchMovePoint()
    {
        Vector3 newPoint = new Vector3(1000f, 1000f, 1000f);
        foreach(Vector3 point in movePoints)
        {
            if(targetPoint != point)
            {
                if (Vector3.Distance(targetPoint, point) < Vector3.Distance(targetPoint, newPoint))
                {
                    newPoint = point;
                }
            }
        }
        targetPoint = newPoint;
    }

    private bool MoveComplete()
    {
        foreach(Vector3 point in movePoints)
        {
            if(Vec3Abs(transform.position, point) < 0.01f)
            {
                return true;
            }
        }
        return false;
    }

    private float Vec3Abs(Vector3 a, Vector3 b)
    {
        return (Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y) + Mathf.Abs(a.z - b.z)) / 3.0f;
    }

    private Vector3 Vec3Lerp(Vector3 a, Vector3 b, float t)
    {
        return new Vector3(Mathf.Lerp(a.x, b.x, t), Mathf.Lerp(a.y, b.y, t), Mathf.Lerp(a.z, b.z, t));
    }
    #endregion
}
