using UnityEngine;
/// <summary>
/// 创建人：一个人心
/// 功能说明：UI循环列表
/// </summary>
public class LoopList
{
    /// <summary>
    /// 循环列表回调
    /// </summary>
    public interface ILoopListSetupData
    {
        /// <summary>
        /// 设置容器中数据下标
        /// </summary>
        /// <param name="containerIndex">容器下标</param>
        /// <param name="dataIndex">数据下标</param>
        void OnSetDataIndexToContainer(int containerIndex, int dataIndex);
    }

    /// <summary>
    /// 回调
    /// </summary>
    public ILoopListSetupData controller;

    /// <summary>
    /// 数据大小
    /// </summary>
    public int DataLength { get; private set; } = 1;
    /// <summary>
    /// 容器大小
    /// </summary>
    public int ContainersLength { get; private set; } = 1;
    /// <summary>
    /// 当前索引
    /// </summary>
    public int CurrentIndex { get; private set; } = 0;

    private int forceIndex;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="controller">受控者</param>
    /// <param name="forceIndex">聚焦的容器下标</param>
    /// <param name="dataLength">数据大小</param>
    /// <param name="containersLength">容器大小</param>
    /// <param name="startIndex">开始的数据下标</param>
    public void Initialize(ILoopListSetupData controller, int forceIndex, int dataLength, int containersLength, int startIndex = 0)
    {
        this.controller = controller;
        this.forceIndex = forceIndex;
        this.DataLength = dataLength;
        this.ContainersLength = containersLength;
        this.CurrentIndex = startIndex;
        SetUp(CurrentIndex);
    }

    /// <summary>
    /// 设置
    /// </summary>
    /// <param name="newCurrentIndex">新的当前下标</param>
    private void SetUp(int newCurrentIndex)
    {
        //int startIndex = ((newCurrentIndex - forceIndex) % DataLength + DataLength) % forceIndex;
        int startIndex = (newCurrentIndex - forceIndex + DataLength) % DataLength;
        // 为每个容器指定新的对应数据下标
        for (int i = 0; i < ContainersLength; ++i)
        {
            controller?.OnSetDataIndexToContainer(i, startIndex);
            startIndex = (startIndex + 1) % DataLength;
        }
    }

    #region 运算符重载

    /// <summary>
    /// 重载+
    /// </summary>
    /// <param name="list"></param>
    /// <param name="num"></param>
    /// <returns></returns>
    public static LoopList operator +(LoopList list, int num)
    {
        int index = list.CurrentIndex;
        index = (index + (num % list.DataLength) + list.DataLength) % list.DataLength;
        list.CurrentIndex = index;
        list.SetUp(list.CurrentIndex);
        return list;
    }

    /// <summary>
    /// 重载-
    /// </summary>
    /// <param name="list"></param>
    /// <param name="num"></param>
    /// <returns></returns>
    public static LoopList operator -(LoopList list, int num)
    {
        int index = list.CurrentIndex;
        index = (index - (num % list.DataLength) + list.DataLength) % list.DataLength;
        list.CurrentIndex = index;
        list.SetUp(list.CurrentIndex);
        return list;
    }

    #endregion
}
