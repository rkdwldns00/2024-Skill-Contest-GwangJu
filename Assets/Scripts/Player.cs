using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public float moveSpeed = 3;
    public float jumpPower = 5;
    public Rigidbody rigid;

    public Vector3 dashDirection = Vector2.zero;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();

    }

    void Update()
    {
        if (dashDirection != Vector3.zero)
        {
            transform.position += dashDirection * moveSpeed * Time.deltaTime;
            if (Input.GetAxisRaw("Horizontal") == 0)
            {
                dashDirection = Vector3.zero;
                rigid.useGravity = true;
            }
        }
        else
        {
            RaycastHit hit;
            if (rigid.SweepTest(Vector3.down, out hit, 0.1f))
            {
                rigid.velocity = new Vector3(rigid.velocity.x, jumpPower, 0);
            }
            rigid.velocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, rigid.velocity.y, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<IItem>()?.Use(this);
    }
}
