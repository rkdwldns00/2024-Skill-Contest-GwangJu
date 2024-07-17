using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMonster : Monster
{
    [Header("Dash Info")]
    public float dashForce;
    public float playerCheckRange;
    public bool isDashing;

    protected override void Update()
    {
        base.Update();

        CheckPlayer();
    }

    protected override void Move()
    {
        if (!isDashing) base.Move();
    }

    void CheckPlayer()
    {
        int playerLayer = (1 << LayerMask.NameToLayer("Player"));
        Collider[] playerCheck = Physics.OverlapSphere(transform.position, playerCheckRange, playerLayer);

        if (playerCheck.Length > 0 && !isDashing)
        {
            Debug.Log(playerCheck[0].name);
            StartCoroutine(Dash(playerCheck[0].transform.position));
            isDashing = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.1f);
        Gizmos.DrawSphere(transform.position, playerCheckRange);
    }

    IEnumerator Dash(Vector3 playerPos)
    {
        Vector3 dashDir = playerPos - transform.position;
        rb.AddForce(dashDir * dashForce, ForceMode.Impulse);

        yield return new WaitForSeconds(0.5f);

        while (rb.velocity != new Vector3(0,0,0)) yield return new WaitForSeconds(0.5f);

        isDashing = false;
        yield break;
    }
}