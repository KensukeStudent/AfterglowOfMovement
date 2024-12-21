public interface IMove
{
    /// <summary>
    /// x軸移動速度
    /// <para>Time.deltatimeされた結果が返ってくる</para>
    /// </summary>
    float VelocityX { get; }

    /// <summary>
    /// y軸移動速度
    /// <para>Time.deltatimeされた結果が返ってくる</para>
    /// </summary>
    float VelocityY { get; }

    /// <summary>
    /// 移動ブレーキタイム
    /// </summary>
    float BrakingTimer { get; }
}