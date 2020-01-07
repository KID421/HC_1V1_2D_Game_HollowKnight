using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region 欄位
    // 宣告變數 (定義欄位 field)
    // 修飾詞 欄位類型 欄位名稱 (指定 值);

    [Header("走路速度"), Range(0.5f, 500f)]
    public float speed = 1.5f;
    [Header("跳躍高度"), Range(10, 3000)]
    public int jump = 100;
    [Header("是否在地板上"), Tooltip("用來判斷角色是否站在地板上")]
    public bool isGorund;       // true 與 false (預設值)
    [Header("玩家名稱")]
    public string playerName = "KID";
    [Header("音效區域")]
    public AudioClip soundAtk;
    public AudioClip soundJump;
    public AudioClip soundGround;
    public AudioClip soundDamage;
    public AudioClip soundDead;
    public AudioClip soundCoin;
    [Header("介面")]
    public Text textCoin;
    [Header("攻擊位置")]
    public Transform pointAtk;
    [Header("攻擊長度"), Range(0, 3)]
    public float rangeAtk = 1.5f;
    [Header("攻擊力"), Range(0, 300)]
    public float attack = 20;

    private int coin;

    private Animator ani;       // 動畫控制器
    private AudioSource aud;    // 音效來源 (喇叭)
    private Rigidbody2D r2d;    // 剛體：物理
    private Transform tra;      // 變形
    #endregion

    #region 方法
    public void Move()
    {
        if (Input.GetKeyDown(KeyCode.D)) tra.eulerAngles = new Vector3(0, 0, 0);     // 變形.歐拉角度 = 新 三維向量 (0, 0, 0)
        if (Input.GetKeyDown(KeyCode.A)) tra.eulerAngles = new Vector3(0, 180, 0);     // 變形.歐拉角度 = 新 三維向量 (0, 180, 0)

        if (ani.GetCurrentAnimatorStateInfo(0).IsName("攻擊")) return;    // 如果 目前動畫 名稱是 "攻擊" 跳出

        float h = Input.GetAxis("Horizontal");                            // 水平軸向：A、左：-1，D、右：1，不按：0
        r2d.AddForce(new Vector2(speed * h, 0));
        ani.SetBool("跑步開關", h != 0);
    }

    public void Jump()
    {
        // 練習判斷按 Space 輸出跳躍 並且 在地板上
        if (Input.GetKeyDown(KeyCode.Space) && isGorund)
        {
            isGorund = false;                       // 不在地板上
            ani.SetBool("跳躍開關", true);
            aud.PlayOneShot(soundJump, 1.5f);
            r2d.AddForce(new Vector2(0, jump));
        }
    }

    public void Attack()
    {
        // 練習判斷按 Mouse0 輸出攻擊
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ani.SetTrigger("攻擊觸發");
            aud.PlayOneShot(soundAtk, 1.5f);

            RaycastHit2D hit = Physics2D.Raycast(pointAtk.position, pointAtk.right, rangeAtk, 1 << 9);  // 碰撞資訊 = 物理.射線碰撞(中心點，方向，長度，圖層)
            if (hit) hit.collider.GetComponent<Enemy>().Damage(attack);
        }
    }

    public void Damage()
    {
        ani.SetTrigger("受傷觸發");
        aud.PlayOneShot(soundDamage, 1.5f);
    }

    public void Dead()
    {
        ani.SetBool("死亡開關", true);
        aud.PlayOneShot(soundDead, 2f);
    }
    #endregion

    #region 事件
    // 事件 - 在特定時間會執行的方法
    // 開始：遊戲播放時執行一次
    private void Start()
    {
        // 取得元件<泛型>()
        ani = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
        r2d = GetComponent<Rigidbody2D>();
        tra = GetComponent<Transform>();

        pointAtk = tra.GetChild(0);     // 變形.取得子物件(編號) *編號從 0 開始
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

    // 碰撞開始：碰到其他碰撞器開始時執行一次 (碰撞參數：儲存碰到物件的碰撞資訊)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "地板")
        {
            isGorund = true;
            ani.SetBool("跳躍開關", false);
            aud.PlayOneShot(soundGround, 0.3f);
        }
    }

    // 觸發開始 (有勾選 IsTrigger 的碰撞器)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "金幣") // 如果 碰到.物件.標籤 等於 "金幣"
        {
            aud.PlayOneShot(soundCoin, 1.5f);   // 播放(金幣音效)
            Destroy(collision.gameObject);      // 刪除(碰到.物件)
            coin++;                             // 數量 + 1
            textCoin.text = "金幣：" + coin;    // 更新介面
        }
    }

    // 繪製圖示 (開發者使用)
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;                                           // 圖示.顏色 = 顏色.紅色
        Gizmos.DrawRay(pointAtk.position, pointAtk.right * rangeAtk);       // 圖示.繪製射線(中心點，方向 * 長度)
    }
    #endregion
}