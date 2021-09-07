using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMove : MonoBehaviour
{
    Rigidbody2D rigid;
    public Animator anim;

    public int nextMove;
    public bool ismoving; // 바꿀 수 없는 것
    public bool isAct; // 바꿀 수 있는 것
    public bool isIdle; // 움직이지 않고 애니메이션하는 경우

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Think();
    }
    void FixedUpdate()
    {
        if (isIdle)
            return;
        else if (isAct)
            Move();
        else
        {
            stopping();
            CancelInvoke("Think");
            anim.SetInteger("WalkSpeed", 0);
        }
    }
    public void stopping()
    {
        rigid.velocity = new Vector2(rigid.velocity.x, nextMove * 0f);
    }
    void Move()
    {
        // Move
        rigid.velocity = new Vector2(rigid.velocity.x, nextMove * 0.5f);

        //Plaform Check
        if (nextMove == 1)
        {
            Debug.DrawRay(rigid.position, Vector3.up, new Color(0, 1, 0));
            RaycastHit2D rayHit1 = Physics2D.Raycast(rigid.position, Vector3.up, 1f, LayerMask.GetMask("AnimalArea"));
            Collider(rayHit1.collider);
        }
        else
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit1 = Physics2D.Raycast(rigid.position, Vector3.down, 1f, LayerMask.GetMask("AnimalArea"));
            Collider(rayHit1.collider);
        }
    }
    // 방향 바꾸기
    void Collider(Collider2D rayHit1)
    {
        if (rayHit1 != null)
        {
            nextMove *= -1;
            CancelInvoke();
            anim.SetInteger("WalkSpeed", nextMove);
            Invoke("Think", 3);
        }
    }
    // 혼자 생각해서 방향바꾸기
    void Think()
    {
        nextMove = Random.Range(-1, 2);

        float nextThinkTime = Random.Range(2f, 10f);
        Invoke("Think", nextThinkTime);

        anim.SetInteger("WalkSpeed", nextMove);
    }
}