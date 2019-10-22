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

    // 陣列
    public AudioClip[] sounds;

    // 定義方法 method
    // 修飾詞 傳回類型 方法名稱 (參數) { 敘述 }
    public void Move()
    {
        print("移動!!!");
    }

    // 事件 - 在特定時間會執行的方法
    // 開始：遊戲播放時執行一次
    private void Start()
    {
        Move();
    }
    // 更新：一秒執行約 60 次 (60FPS)
    private void Update()
    {
        Move();
    }
}