using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("血量"), Range(100, 1000)]
    public float hp = 200;
    [Header("移動速度"), Range(0, 1000)]
    public float speed = 10;
    [Header("攻擊冷卻"), Range(0, 10)]
    public float cd = 3;

    private float timer;
    private bool startAttack;

    private Animator ani;
    private Rigidbody2D rig;

    private void Start()
    {
        ani = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        Attack();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "玩家") startAttack = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "玩家") startAttack = false;
    }

    public void Damage(float damage)
    {
        hp -= damage;
        ani.SetTrigger("受傷觸發");
        if (hp <= 0) Dead();
    }

    private void Dead()
    {
        ani.SetBool("死亡開關", true);
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<CapsuleCollider2D>().enabled = false;
    }

    private void Move()
    {
        if (startAttack) return;
        rig.AddForce(-transform.right * speed);
        ani.SetBool("跑步開關", true);
    }

    private void Attack()
    {
        if (startAttack)
        {
            if (timer < cd)
            {
                timer += Time.deltaTime;
                ani.SetBool("跑步開關", false);
            }
            else
            {
                timer = 0;
                ani.SetTrigger("攻擊觸發");
            }
        }
    }
}
