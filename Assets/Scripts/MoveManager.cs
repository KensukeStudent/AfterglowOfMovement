using System.Collections.Generic;
using UnityEngine;

public class MoveManager
{
    public enum MoveType
    {
        Normal
    }

    private Dictionary<MoveType, MoveControlInfo> moveDict = null;

    private MoveControlInfo current = null;

    /// <summary>
    /// x軸移動速度
    /// <para>Time.deltatimeされた結果が返ってくる</para>
    /// </summary>
    public float VelocityX => current == null ? 0 : current.move.VelocityX;

    /// <summary>
    /// y軸移動速度
    /// <para>Time.deltatimeされた結果が返ってくる</para>
    /// </summary>
    public float VelocityY => current == null ? 0 : current.move.VelocityY;

    /// <summary>
    /// ブレーキタイム
    /// </summary>
    public float BrakingTimer => current == null ? 0 : current.move.BrakingTimer;

    public MoveManager(MonoBehaviour parent)
    {
        parent.TryGetComponent(out MoveControl move);
        moveDict = new Dictionary<MoveType, MoveControlInfo>()
        {
            { MoveType.Normal, new MoveControlInfo(){ move = move, update = move }}
        };

        ChangeMoveContorl(MoveType.Normal);
    }

    public void OnUpdate()
    {
        current?.update.OnUpdate();
    }

    /// <summary>
    /// 移動方法の更新
    /// </summary>
    public void ChangeMoveContorl(MoveType moveType)
    {
        current = moveDict[moveType];
    }

    public class MoveControlInfo
    {
        public IMove move;
        public IUpdate update;
    }
}
