using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    [Header("Move Info")]
    public float speed;
    public Rigidbody rb;

    [Header("DownCheck Ray Info")]
    public float downRayX;
    public float DownRaydis;
    public LayerMask downRayLayer;

    [Header("SideCheck Ray Info")]
    public float sideRayX;
    public float sideRayDis;
    public LayerMask sideRayLayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected virtual void Update()
    {
        Move();
        DownCheckRay();
        SideCheckRay();
    }

    void DownCheckRay()
    {
        Vector3 downVec = transform.position + new Vector3(downRayX, 0, 0);
        RaycastHit downHit;
        
        Physics.Raycast(downVec, Vector3.down, out downHit, DownRaydis, downRayLayer);
        if (downHit.collider == null) Rotate();
    }

    void SideCheckRay()
    {
        Vector3 sideVec = new Vector3(sideRayX, 0, 0);
        bool isSide = Physics.Raycast(transform.position, sideVec, sideRayDis, sideRayLayer);

        if (isSide) Rotate();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position + new Vector3(downRayX, 0, 0), Vector3.down * DownRaydis);

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, new Vector3(sideRayX, 0, 0) * sideRayDis);
    }

    protected virtual void Move() => transform.Translate(Vector3.left * speed * Time.deltaTime);

    void Rotate()
    {
        speed *= -1;
        downRayX *= -1;
        sideRayX *= -1;
    }

    public void Die() => Destroy(gameObject);
}
