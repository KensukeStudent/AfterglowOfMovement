using UnityEngine;

/// <summary>
/// アニメーターで使用しているキー設定
/// </summary>
public class PlayerAnimator : MonoBehaviour
{
    private int IsRun => Animator.StringToHash("IsRun");

    private int Braking => Animator.StringToHash("Braking");

    private Animator animator = null;

    private void Awake()
    {
        TryGetComponent(out animator);
    }

    /// <summary>
    /// 走りアニメーション
    /// </summary>
    public void SetRunAnim(bool isRun)
    {
        animator.SetBool(IsRun, isRun);
    }

    /// <summary>
    /// ブレーキアニメーション
    /// </summary>
    public void SetBrakingAnim(float value)
    {
        animator.SetFloat(Braking, value);
    }
}
