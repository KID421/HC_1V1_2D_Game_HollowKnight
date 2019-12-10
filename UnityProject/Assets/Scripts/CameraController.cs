using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("目標物件")]
    public Transform target;
    [Header("速度"), Range(0, 100)]
    public float speed = 1.5f;
    [Header("水平與垂直範圍限制")]
    public Vector2 limitHorizontal;
    public Vector2 limitVerticle;

    /// <summary>
    /// 追蹤目標物件的方法。
    /// </summary>
    private void Track()
    {
        Vector3 posTarget = target.position;                                            // 目標座標
        Vector3 posCamera = new Vector3(posTarget.x, posTarget.y, -10);                 // 取得目標 X、Y 座標，Z 固定在原本的 -10

        posCamera.x = Mathf.Clamp(posCamera.x, limitHorizontal.x, limitHorizontal.y);   // 攝影機.X 夾在範圍內
        posCamera.y = Mathf.Clamp(posCamera.y, limitVerticle.x, limitVerticle.y);       // 攝影機.Y 夾在範圍內

        // 攝影機.座標 = 三維向量.插值(攝影機.座標，攝影機.新座標，百分比)
        transform.position = Vector3.Lerp(transform.position, posCamera, speed * Time.deltaTime);
    }

    // LateUpdate 在 Update 後執行，適合用來控制攝影機
    private void LateUpdate()
    {
        Track();
    }
}
