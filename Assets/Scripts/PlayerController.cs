using UnityEngine;

/// <summary>
/// 移動後の滑りを指定秒数行うプレイヤークラス
/// </summary>
public class PlayerController : MonoBehaviour
{
    private Transform player = null;

    /// <summary>
    /// プレイヤーアニメーター呼び出し用
    /// </summary>
    private PlayerAnimator anim = null;

    private SpriteRenderer spriteRenderer = null;

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

    /// <summary>
    /// 加速度
    /// </summary>
    private float velocity = 0;

    /// <summary>
    /// 現在の加速度
    /// </summary>
    private float currentVelocity = 0;

    /// <summary>
    /// アニメーション時間
    /// </summary>
    private float timer = 0;

    private void Awake()
    {
        TryGetComponent(out player);
        TryGetComponent(out anim);
        TryGetComponent(out spriteRenderer);

        timer = 0;
    }

    private void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");

        anim.SetRunAnim(!Mathf.Approximately(0, horizontal)); // 走りアニメーション
        anim.SetBrakingAnim(timer); // ブレーキアニメーション

        var pos = player.position;

        if (horizontal != 0)
        {
            // 入力による移動
            spriteRenderer.flipX = horizontal > 0 ? false : true;
            pos.x += horizontal * moveSpeed * Time.deltaTime;

            velocity = horizontal * moveSpeed;
            currentVelocity = velocity;
            timer = 1;
        }
        else if (timer > 0)
        {
            // 余韻アニメーション再生
            int direction = spriteRenderer.flipX ? -1 : 1;
            float addVelocity = direction * AddVelocity(Mathf.Abs(velocity));
            currentVelocity += addVelocity * Time.deltaTime;
            pos.x += currentVelocity * Time.deltaTime;

            timer -= Time.deltaTime / duration;

            Debug.Log($"add: {addVelocity}. currentVelocity: {currentVelocity}. timer: {timer}");
        }

        player.position = pos;
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
