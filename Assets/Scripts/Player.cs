using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour, IResetable
{
    public float moveSpeed = 3;
    public float jumpPower = 5;
    public float dashPower = 5;
    public Transform col;

    public Rigidbody rigid { get; set; }
    public bool haveDoubleJump { get; set; } = false;
    public bool haveDash { get; set; } = false;

    public float stunTimer { get; set; } = 0f;

    Vector3 originPos;
    bool powerOverWhelming = false;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        originPos = transform.position;
        LevelManager.AddResetable(this);
    }

    void Update()
    {
        col.rotation = Quaternion.identity;
        stunTimer -= Time.deltaTime;
        if (stunTimer > 0)
        {
            rigid.velocity = Vector3.zero;
            return;
        }
        else
        {
            GroundCheck();

        }

        if (Mathf.Abs(rigid.velocity.x) <= moveSpeed)
        {
            rigid.velocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, rigid.velocity.y, 0);
        }
        else
        {
            rigid.velocity = new Vector3(Mathf.Lerp(rigid.velocity.x, 0, Time.deltaTime / 2), rigid.velocity.y, rigid.velocity.z);
        }

        if (Input.GetKeyDown(KeyCode.Space) && haveDoubleJump)
        {
            haveDoubleJump = false;
            rigid.velocity = new Vector3(rigid.velocity.x, Mathf.Max(rigid.velocity.y, jumpPower), 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && Input.GetAxisRaw("Horizontal") != 0 && haveDash)
        {
            haveDash = false;
            StartDash();
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LevelManager.instance.ResetLevel();
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            powerOverWhelming = !powerOverWhelming;
        }
    }

    void GroundCheck()
    {
        RaycastHit hit;
        if (rigid.SweepTest(Vector3.down, out hit, 0.1f))
        {
            if (hit.collider.GetComponent<Monster>()) hit.collider.GetComponent<Monster>().Die();

            rigid.velocity = new Vector3(rigid.velocity.x, Mathf.Max(rigid.velocity.y, jumpPower), 0);
        }


        hit.collider?.GetComponent<IUseable>()?.Use(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (stunTimer <= 0)
        {
            collision?.transform?.GetComponent<IUseable>()?.Use(this);
        }
    }

    void StartDash()
    {
        //rigid.useGravity = false;
        rigid.AddForce(new Vector3(Input.GetAxisRaw("Horizontal") * dashPower, 0, 0));
    }

    public void ResetObject()
    {
        transform.position = originPos;
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