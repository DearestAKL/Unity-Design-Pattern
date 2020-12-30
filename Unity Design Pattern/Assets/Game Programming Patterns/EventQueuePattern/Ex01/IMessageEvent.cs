using System;

namespace EventQueuePatternEx01
{
    /// <summary>
    /// 事件接口
    /// </summary>
    public interface IMessageEvent
    {
        DateTime timeRaised { get; }
        float displayTime { get; }
        MessagePriority priority { get; }
        object message { get; }
    }
}