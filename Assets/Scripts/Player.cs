using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour, IResetable
{
    public float moveSpeed = 3;
    public float jumpPower = 5;
    public Rigidbody rigid { get; set; }
    public bool haveDoubleJump { get; set; } = false;

    public Vector3 dashDirection { get; set; } = Vector2.zero;
    Vector3 originPos;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        originPos = transform.position;
        LevelManager.AddResetable(this);
    }

    void Update()
    {
        if (dashDirection != Vector3.zero)
        {
            transform.position += dashDirection * moveSpeed * Time.deltaTime;
            rigid.velocity = Vector3.zero;
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                EndDash();
            }
        }
        else
        {
            GroundCheck();

            rigid.velocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, rigid.velocity.y, 0);

            if (Input.GetKeyDown(KeyCode.Space) && haveDoubleJump)
            {
                haveDoubleJump = false;
                rigid.velocity = new Vector3(rigid.velocity.x, Mathf.Max(rigid.velocity.y, jumpPower), 0);
            }
        }
    }

    void GroundCheck()
    {
        RaycastHit hit;
        if (rigid.SweepTest(Vector3.down, out hit, 0.1f))
        {
            rigid.velocity = new Vector3(rigid.velocity.x, Mathf.Max(rigid.velocity.y, jumpPower), 0);
        }


        hit.collider?.GetComponent<IUseable>()?.Use(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        EndDash();
        //other.GetComponent<IUseable>()?.Use(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        EndDash();
        //collision.transform.GetComponent<IUseable>()?.Use(this);
    }

    void EndDash()
    {
        rigid.velocity = dashDirection * 2;
        rigid.useGravity = true;
        dashDirection = Vector3.zero;
    }

    public void ResetObject()
    {
        transform.position = originPos;
        EndDash();
        rigid.velocity = Vector3.zero;
        haveDoubleJump = false;
    }
}

public interface IResetable
{
    void ResetObject();
}

public interface IUseable
{
    void Use(Player player);
}