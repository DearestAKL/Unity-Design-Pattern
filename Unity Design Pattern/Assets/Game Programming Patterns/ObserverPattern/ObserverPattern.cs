using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObserverPattern
{
    public class ObserverPattern : MonoBehaviour
    {
        void Start()
        {
            NewspaperSubject s = new NewspaperSubject();

            s.Attach(new PeopleObserver(s, "ZHANGSAN"));
            s.Attach(new PeopleObserver(s, "LISI"));
            s.Attach(new PeopleObserver(s, "WANGWU"));

            // Change subject and notify observers
            s.SubjectState = "RMRB";
            s.Notify();
            // Change subject and notify observers again
            s.SubjectState = "JFRB";
            s.Notify();
        }
    }

    #region 抽象
    /// <summary>
    /// 被观察者
    /// </summary>
    abstract class Subject
    {
        private List<Observer> observers = new List<Observer>();

        //连接-订阅
        public void Attach(Observer observer)
        {
            observers.Add(observer);
        }

        //分离-取消订阅
        public void Detach(Observer observer)
        {
            observers.Remove(observer);
        }

        //通知
        public void Notify()
        {
            foreach (Observer observer in observers)
            {
                observer.Update();
            }
        }
    }

    /// <summary>
    /// 观察者
    /// </summary>
    abstract class Observer
    {
        public abstract void Update();
    }
    #endregion

    #region 实例
    /// <summary>
    /// 人
    /// </summary>
    class PeopleObserver : Observer
    {
        private string name;
        private string observerState;
        private NewspaperSubject subject;

        public PeopleObserver(NewspaperSubject subject,string name)
        {
            this.name = name;
            this.subject = subject;
        }

        public override void Update()
        {
            observerState = subject.SubjectState;
            Debug.Log($"{name}收到了{subject.SubjectState}的报纸");
        }

        public NewspaperSubject Subject
        {
            get { return subject; }
            set { subject = value; }
        }
    }

    /// <summary>
    /// 报社
    /// </summary>
    class NewspaperSubject : Subject
    {
        private string subjectState;

        // Gets or sets subject state
        public string SubjectState
        {
            get { return subjectState; }
            set { subjectState = value; }
        }
    }
    #endregion
}