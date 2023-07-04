using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour, IResetable
{
    public float moveSpeed = 3;
    public float jumpPower = 5;
    public float dashPower = 300;
    public Transform col;

    public Rigidbody rigid { get; set; }
    public Skill haveSkill = Skill.None;
    public enum Skill { None, Dash, DoubleJump }

    public float stunTimer { get; set; } = 0f;

    Vector3 originPos;
    bool powerOverWhelming = false;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        originPos = transform.localPosition;
        LevelManager.AddResetable(this);
    }

    void Update()
    {
        col.rotation = Quaternion.identity;
        stunTimer -= Time.deltaTime;
        if (stunTimer > 0)
        {
            rigid.useGravity = false;
            rigid.velocity = Vector3.zero;
            col.gameObject.SetActive(false);
            return;
        }
        else
        {
            col.gameObject.SetActive(true);
            rigid.useGravity = true;
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

        if (Input.GetKeyDown(KeyCode.Space) && haveSkill == Skill.DoubleJump)
        {
            haveSkill = Skill.None;
            rigid.velocity = new Vector3(rigid.velocity.x, Mathf.Max(rigid.velocity.y, jumpPower), 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && Input.GetAxisRaw("Horizontal") != 0 && haveSkill == Skill.Dash)
        {
            haveSkill = Skill.None;
            StartDash();
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LevelManager.instance.ResetLevel();
        }

        if (Mathf.Abs(transform.position.y) > 12)
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
        if (rigid.SweepTest(Vector3.down, out hit, 0.02f))
        {
            if (hit.collider.GetComponent<Monster>())
            {
                Collider[] cols = Physics.OverlapBox(transform.position - Vector3.down * 0.5f, new Vector3(0.1f, 1, 500), Quaternion.identity, 1 << LayerMask.NameToLayer("Monster"));
                bool c = true;
                foreach (Collider col in cols)
                {
                    if (hit.collider == col)
                    {
                        hit.collider.GetComponent<Monster>().Die();
                        c = false;
                        break;
                    }
                }
                if (c)
                {
                    gameObject.SetActive(false);
                    LevelManager.instance.ResetLevel(0.5f);
                }
            }

            if (hit.collider?.GetComponent<IUseable>() == null)
            {
                rigid.velocity = new Vector3(rigid.velocity.x, Mathf.Max(rigid.velocity.y, jumpPower), 0);
            }
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

    private void OnTriggerEnter(Collider other)
    {
        if (stunTimer <= 0)
        {
            other?.transform?.GetComponent<IUseable>()?.Use(this);
        }
    }

    void StartDash()
    {
        //rigid.useGravity = false;
        rigid.AddForce(new Vector3(Input.GetAxisRaw("Horizontal") * dashPower, 0, 0));
    }

    public void ResetObject()
    {
        transform.localPosition = originPos;
        rigid.velocity = Vector3.zero;
        haveSkill = Skill.None;
        gameObject.SetActive(true);
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