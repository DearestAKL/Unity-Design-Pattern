using System;

namespace EventQueuePatternEx01
{
    /// <summary>
    /// 事件实体类
    /// </summary>
    public class MessageEvent : GameEvent, IMessageEvent
    {
        public DateTime timeRaised { private set; get; }
        public MessagePriority priority { private set; get; }
        public float displayTime { private set; get; }
        public object message { private set; get; }

        public MessageEvent(object message, float displayTime, MessagePriority priority)
        {
            this.message = message;
            this.displayTime = displayTime;
            this.priority = priority;
            timeRaised = DateTime.Now;
        }
    }
}