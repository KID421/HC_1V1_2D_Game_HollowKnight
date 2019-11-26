using UnityEngine;

public class LearnIf : MonoBehaviour
{
    // 欄位
    // 修飾詞 類型 名稱;
    public bool test;

    public bool weapon;

    // Start 遊戲開始執行一次
    private void Start()
    {
        if (true)
        {
            print("測試!");
        }

        // 如果
        if (test)
        {
            // () 內部林值勾選會執行這裡
            print("布林值勾選了!!!");
        }
        // 否則
        else
        {
            // () 內部林值取消勾選會執行這裡
            print("布林值取消勾選了!!!");
        }

        if (weapon)
        {
            print("AK47");
        }
        else
        {
            print("小刀");
        }
    }

    // 一秒執行約 60 次
    private void Update()
    {
        // Unity API
        // 輸入.取得按鍵(按鍵列舉.名稱)
        // 使用這個方法會得到一個布林值
        // 玩家按下指定按鍵會得到 true 沒按則是 false
        print(Input.GetKeyDown(KeyCode.Space));

        if (Input.GetKeyDown(KeyCode.D))
        {
            print("往右走!!!");
        }
    }
}
