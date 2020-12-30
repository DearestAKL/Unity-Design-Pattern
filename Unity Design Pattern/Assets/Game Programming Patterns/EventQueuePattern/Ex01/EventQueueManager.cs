using System.Collections.Generic;

namespace EventQueuePatternEx01
{
    /// <summary>
    /// 事件管理
    /// </summary>
    public class EventQueueManager
    {
        private static EventQueueManager instance;
        public static EventQueueManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EventQueueManager();
                }
                return instance;
            }
        }

        //泛型委托
        public delegate void EventDelegate<T>(T e) where T : GameEvent;
        //普通委托
        public delegate void EventDelegate(GameEvent e);

        private Dictionary<System.Type, EventDelegate> DelegatesDic = new Dictionary<System.Type, EventDelegate>();
        private Dictionary<System.Delegate, EventDelegate> DelegateLookupDic = new Dictionary<System.Delegate, EventDelegate>();

        /// <summary>
        /// 添加监听者
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="del"></param>
        public void AddListener<T>(EventDelegate<T> del) where T : GameEvent
        {
            EventDelegate internalDelegate = (e) => { 
                del((T)e); 
            };

            //检查是否已存在
            if(DelegateLookupDic.ContainsKey(del) && DelegateLookupDic[del] == internalDelegate)
            {
                return;
            }

            //加入字典
            DelegateLookupDic[del] = internalDelegate;

            EventDelegate tempDel;
            if (DelegatesDic.TryGetValue(typeof(T), out tempDel))
            {
                DelegatesDic[typeof(T)] = tempDel += internalDelegate;
            }
            else
            {
                DelegatesDic[typeof(T)] = internalDelegate;
            }
        }

        /// <summary>
        /// 移除监听者
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="del"></param>
        public void RemoveListener<T>(EventDelegate<T> del) where T : GameEvent
        {
            EventDelegate internalDelegate;
            if (DelegateLookupDic.TryGetValue(del, out internalDelegate))
            {
                EventDelegate tempDel;
                if (DelegatesDic.TryGetValue(typeof(T), out tempDel))
                {
                    tempDel -= internalDelegate;
                    if (tempDel == null)
                    {
                        DelegatesDic.Remove(typeof(T));
                    }
                    else
                    {
                        DelegatesDic[typeof(T)] = tempDel;
                    }
                }

                DelegateLookupDic.Remove(del);
            }
        }

        public void AddEventToQueue(GameEvent e)
        {
            EventDelegate del;
            if(DelegatesDic.TryGetValue(e.GetType(),out del))
            {
                del.Invoke(e);
            }
        }
    }
}