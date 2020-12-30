using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventQueuePatternEx02
{
    public class EventQueuePatternEx02 : MonoBehaviour
    {
        Receiver1 receiver1 = new Receiver1();
        Receiver2 receiver2 = new Receiver2();

        void Start()
        {

        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                //EventManger.Instance.SendEvent(EventType.UI_Event1, 1);

                EventManger.Instance.SendEvent(EventType.UI_Event1, "666");

                EventManger.Instance.SendEvent(EventType.UI_Event1, new GameObject());
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                EventManger.Instance.SendEvent(EventType.UI_Event2, new GameObject(), new GameObject());
            }


            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                EventManger.Instance.RegisterEvent(EventType.UI_Event3, receiver1.OnEventProcess3);
                EventManger.Instance.RegisterEvent(EventType.UI_Event3, receiver1.OnEventProcess4);
            }
            if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                EventManger.Instance.SendEvent(EventType.UI_Event3, 666);
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                EventManger.Instance.UnRegisterEvent(EventType.UI_Event3, receiver1.OnEventProcess3);
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                EventManger.Instance.UnRegisterEvent(EventType.UI_Event3);
            }

            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                EventManger.Instance.RegisterEvent(EventType.UI_Event4, receiver2.OnEventProcess4);
            }
            if (Input.GetKeyDown(KeyCode.Keypad6))
            {
                GameObject msgObj = new GameObject();
                //多个消息参数
                EventManger.Instance.SendEvent(EventType.UI_Event4, 666, "hello", msgObj);
            }

        }

        void OnEnable()
        {
            receiver1.RegisterDelegates();
            receiver2.RegisterDelegates();
        }

        void OnDisable()
        {
            receiver1.UnRegisterDelegates();
            receiver2.RegisterDelegates();
        }
    }

    public class Receiver1
    {
        public void RegisterDelegates()
        {
            EventManger.Instance.RegisterEvent(EventType.UI_Event1, OnEventProcess1);
            EventManger.Instance.RegisterEvent(EventType.UI_Event2, OnEventProcess1);
        }


        public void UnRegisterDelegates()
        {
            EventManger.Instance.UnRegisterEvent(EventType.UI_Event1);
            EventManger.Instance.UnRegisterEvent(EventType.UI_Event2);
        }

        private void OnEventProcess1(BaseEventMsg msg)
        {
            Debug.Log("Receiver1! OnEventProcess1");


            if (msg != null && msg.Params.Length > 0)
            {
                //                 int data = Convert.ToInt32(msg.Params[0]);
                //                 Debug.Log("Receiver1! data= int:" + data);

                var data0 = msg.Params[0] as string;
                if (data0 != null)
                {
                    Debug.Log("Receiver1! data= string:" + data0);
                }

                var data1 = msg.Params[0] as GameObject;
                if (data1 != null)
                {
                    Debug.Log("Receiver1! data= GameObject:" + data1);
                }



                //Debug.Log("Receiver1!");
                //int data = (int)msg.Params[0];
                //Debug.Log("Receiver1! data=" + data.ToString());
            }
        }

        private void OnEventProcess2(BaseEventMsg msg)
        {
            Debug.Log("Receiver1! OnEventProcess2");
        }

        public void OnEventProcess3(BaseEventMsg msg)
        {
            int data = Convert.ToInt32(msg.Params[0]);
            Debug.Log("Receiver1! data= int:" + data);

            Debug.Log("Receiver1! OnEventProcess3 int = " + data.ToString());
        }
        public void OnEventProcess4(BaseEventMsg msg)
        {
            Debug.Log("Receiver1! OnEventProcess4");
        }

    }


    public class Receiver2
    {
        public void RegisterDelegates()
        {
            EventManger.Instance.RegisterEvent(EventType.UI_Event1, OnEventProcess1);
            EventManger.Instance.RegisterEvent(EventType.UI_Event2, OnEventProcess2);
        }


        public void UnRegisterDelegates()
        {
            EventManger.Instance.UnRegisterEvent(EventType.UI_Event1);
        }

        private void OnEventProcess1(BaseEventMsg msg)
        {
            Debug.Log("Receiver2! OnEventProcess1");
            if (msg != null && msg.Params.Length > 0)
            {
            }
        }

        private void OnEventProcess2(BaseEventMsg msg)
        {
            Debug.Log("Receiver2! OnEventProcess2");
        }

        public void OnEventProcess3(BaseEventMsg msg)
        {
            Debug.Log("Receiver2! OnEventProcess3");
        }

        public void OnEventProcess4(BaseEventMsg msg)
        {
            Debug.Log("Receiver2! OnEventProcess4");

            if (msg != null && msg.Params.Length > 2)
            {

                int data1 = (int)msg.Params[0];

                string data2 = msg.Params[1] as string;

                GameObject data3 = msg.Params[2] as GameObject;

                if (data2 != null && data3 != null)
                {
                    Debug.Log("Receiver2! OnEventProcess4 DATA:" +
                        "Data1= String" + data1.ToString() +
                        ", Data2= string :" + data2 +
                        ", Data3 GameObject :" + data3.name);
                }


            }


        }

    }
    public class EventManger
    {
        private static EventManger instance;
        public static EventManger Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EventManger();
                }
                return instance;
            }
        }

        public delegate void EventHandler(BaseEventMsg Msg);

        // 消息ID,代理存储字典Map - <(int)EventType,EventHandler代理>
        private Dictionary<int, EventHandler> m_EventHandlerMap = new Dictionary<int, EventHandler>();

        /// <summary>
        /// 注册事件
        /// </summary>
        public void RegisterEvent(EventType eventType, EventHandler eventHandler)
        {

            if (m_EventHandlerMap == null)
            {
                m_EventHandlerMap = new Dictionary<int, EventHandler>();
            }
            int eventTypeID = (int)eventType;
            if (m_EventHandlerMap.ContainsKey(eventTypeID))
            {
                m_EventHandlerMap[eventTypeID] += eventHandler;
            }
            else
            {
                m_EventHandlerMap.Add(eventTypeID, eventHandler);
            }

        }

        /// <summary>
        /// 反注册事件-反注册该EventType下所有的注册消息
        /// </summary>
        public void UnRegisterEvent(EventType eventType)
        {
            int eventTypeID = (int)eventType;

            if (m_EventHandlerMap == null)
            {
                return;
            }

            if (m_EventHandlerMap.ContainsKey(eventTypeID))
            {
                m_EventHandlerMap.Remove(eventTypeID);
            }
        }

        /// <summary>
        /// 反注册事件-反注册该EventType下指定的EventHandler
        /// </summary>
        public void UnRegisterEvent(EventType eventType, EventHandler eventHandler)
        {
            int eventTypeID = (int)eventType;

            if (m_EventHandlerMap == null)
            {
                return;
            }
            //删除eventHandler指定的消息响应
            if (m_EventHandlerMap.ContainsKey(eventTypeID))
            {
                m_EventHandlerMap[eventTypeID] -= eventHandler;
            }
        }



        /// <summary>
        /// 从消息ID,代理存储字典Map中找到对应的消息ID绑定的事件消息
        /// </summary>
        public void SendEvent(EventType eventType, BaseEventMsg Msg)
        {
            int eventTypeID = (int)eventType;

            if (m_EventHandlerMap == null)
            {
                return;
            }

            if (m_EventHandlerMap.ContainsKey(eventTypeID))
            {
                if (m_EventHandlerMap[eventTypeID] != null)
                {
                    m_EventHandlerMap[eventTypeID](Msg);
                    //m_EventHandlerMap[eventTypeID].Invoke(Msg);
                }
            }
        }

        /// <summary>
        /// 发送事件
        /// </summary>
        public void SendEvent(EventType eventType, params object[] inParams)
        {
            BaseEventMsg Msg = new BaseEventMsg();
            if (Msg != null)
            {
                Msg.SetEventMsg(eventType, inParams);
                SendEvent(eventType, Msg);
            }
        }
    }

    /// <summary>
    /// 消息内容
    /// </summary>
    public class BaseEventMsg
    {
        public EventType MsgType;
        public object[] Params = null;

        public BaseEventMsg()
        {

        }

        public BaseEventMsg(EventType inMsgType, params object[] InParams)
        {
            MsgType = inMsgType;
            Params = InParams;
        }

        public void SetEventMsg(EventType inMsgType, params object[] InParams)
        {
            MsgType = inMsgType;
            Params = InParams;
        }

    }

    /// <summary>
    ///  消息类型枚举
    /// </summary>
    public enum EventType
    {
        None,

        //UI事件消息
        UI_Event1 = 1,
        UI_Event2 = 2,
        UI_Event3 = 3,
        UI_Event4 = 4,
        UI_Event5 = 5,

        //渲染事件消息
        Render_Event1 = 100,
        Render_Event2,
    }
}