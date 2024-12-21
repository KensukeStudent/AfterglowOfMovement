using UnityEngine;

public class MoveControl : MonoBehaviour, IMove, IUpdate
{
    public float VelocityX { private set; get; }

    public float VelocityY { private set; get; }

    public float BrakingTimer { private set; get; }

    [SerializeField]
    private float moveSpeed;

    /// <summary>
    /// 移動余韻距離
    /// </summary>
    [SerializeField, Range(0.1f, 5)]
    private float brakeDistance = 0;

    /// <summary>
    /// アニメーション時間
    /// </summary>
    [SerializeField]
    private float duration = 0;

    private float currentVelocity;

    private int direction;

    public void OnUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal != 0)
        {
            VelocityX = horizontal * moveSpeed * Time.deltaTime;

            currentVelocity = horizontal * moveSpeed;
            BrakingTimer = 1;
            direction = horizontal > 0 ? 1 : -1;
        }
        else if (BrakingTimer > 0)
        {
            // ブレーキ

            float addVelocity = direction * AddVelocity(Mathf.Abs(moveSpeed));
            currentVelocity += addVelocity * Time.deltaTime;
            VelocityX = currentVelocity * Time.deltaTime;

            BrakingTimer -= Time.deltaTime / duration;
        }
        else
        {
            VelocityX = 0;
        }
    }

    /// <summary>
    /// 減速度
    /// </summary>
    private float AddVelocity(float vi)
    {
        // s = vi*t + 1/2 a * t^2
        // a = 2s - 2vi*t / t^2

        return (2 * brakeDistance - 2 * vi * duration) / Mathf.Pow(duration, 2);
    }
}
