using UnityEngine;

public class Player : MonoBehaviour
{
    // 宣告變數 (定義欄位 field)
    // 修飾詞 欄位類型 欄位名稱 (指定 值);
    [Header("走路速度"), Range(0.5f, 50f)]
    public float speed = 1.5f;
    [Header("跳躍高度"), Range(10, 1000)]
    public int jump = 100;
    [Header("是否在地板上"), Tooltip("用來判斷角色是否站在地板上")]
    public bool isGorund;       // true 與 false (預設值)
    [Header("玩家名稱")]
    public string playerName = "KID";

    public AudioClip soundAtk;
    public AudioClip soundJump;
    public AudioClip soundGround;
    public AudioClip soundDamage;
    public AudioClip soundDead;

    // 動畫控制器
    public Animator ani;
    // 音效來源 (喇叭)
    public AudioSource aud;

    // 定義方法 method
    // 修飾詞 傳回類型 方法名稱 (參數) { 敘述 }
    public void Move()
    {
        // 練習判斷按 D 輸出往右走
        // 練習判斷按 A 輸出往左走
        if (Input.GetKeyDown(KeyCode.D))
        {
            print("往右走");
            ani.SetBool("跑步開關", true);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            ani.SetBool("跑步開關", false);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            print("往左走");
            ani.SetBool("跑步開關", true);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            ani.SetBool("跑步開關", false);
        }
    }

    public void Jump()
    {
        // 練習判斷按 Space 輸出跳躍
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ani.SetBool("跳躍開關", true);
            aud.PlayOneShot(soundJump, 1.5f);
        }
    }

    public void Attack()
    {
        // 練習判斷按 Mouse0 輸出攻擊
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            print("攻擊");
            ani.SetTrigger("攻擊觸發");
            aud.PlayOneShot(soundAtk, 1.5f);
        }
    }

    public void Damage()
    {
        print("受傷");
        ani.SetTrigger("受傷觸發");
        aud.PlayOneShot(soundDamage, 1.5f);
    }

    public void Dead()
    {
        print("死亡");
        ani.SetBool("死亡開關", true);
        aud.PlayOneShot(soundDead, 2f);
    }

    // 事件 - 在特定時間會執行的方法
    // 開始：遊戲播放時執行一次
    private void Start()
    {
        
    }

    // 更新：一秒執行約 60 次 (60FPS)
    private void Update()
    {
        Move();
        Jump();
        Attack();

        // 測試
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Damage();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Dead();
        }
    }
}