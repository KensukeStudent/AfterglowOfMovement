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

    private MoveManager move = null;

    private SpriteRenderer spriteRenderer = null;

    private void Awake()
    {
        anim = new(this);
        move = new(this);

        TryGetComponent(out player);
        TryGetComponent(out spriteRenderer);
    }

    private void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");

        move.OnUpdate();

        anim.SetRunAnim(!Mathf.Approximately(0, horizontal)); // 走りアニメーション
        anim.SetBrakingAnim(move.BrakingTimer); // ブレーキアニメーション

        if (horizontal != 0) // 画像の向き
        {
            spriteRenderer.flipX = horizontal > 0 ? false : true;
        }

        var pos = player.position;

        pos.x += move.VelocityX;
        pos.y += move.VelocityY;

        player.position = pos;
    }
}
