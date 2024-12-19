using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Transform player = null;

    private int IsRun => Animator.StringToHash("IsRun");

    private int Braking => Animator.StringToHash("Braking");

    private Animator animator = null;

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

    private float timer = 0;

    private void Awake()
    {
        TryGetComponent(out player);
        TryGetComponent(out animator);
        TryGetComponent(out spriteRenderer);

        timer = 0;
    }

    private void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");

        animator.SetBool(IsRun, !Mathf.Approximately(0, horizontal));
        animator.SetFloat(Braking, timer);

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
            float addVelocity = direction * AddVelocity(Mathf.Abs(velocity)) * 2;
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
        // s = vi*t + 0.5 a * t^2
        // 0.5*a*t^2 = s - vi*t
        // a = s-vi*t / 0.5*t^2

        return (brakeDistance - vi * duration) / (0.5f * Mathf.Pow(duration, 2));
    }
}
