using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SingletonPattern
{
    public class SingletonPattern : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            //单例A
            SingletonA.Instance.DoSomething();

            //单例B
            SingletonB.Instance.DoSomething();

            SingletonB sb1 = SingletonB.Instance;
            SingletonB sb2 = SingletonB.Instance;
            if (sb1 == sb2)
            {
                Debug.Log("Objects are the same instance");
            }
        }
    }

    /// <summary>
    /// 单例类基类（抽象类、泛型，其他类只需继承此类即可成为单例类）
    /// 继承该类的，即成为一个单例类
    /// </summary>
    public class Singleton<T> where T : class, new()
    {
        private static T instance = null;

        public static T Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new T();
                    return instance;
                }
                else
                {
                    return instance;
                }
            }
        }

        protected virtual void Awake()
        {
            instance = this as T;
        }
    }

    /// <summary>
    /// 继承自Singleton<T>的单例
    /// </summary>
    public class SingletonA : Singleton<SingletonA>
    {
        public void DoSomething()
        {
            Debug.Log("SingletonA:DoSomething!");
        }
    }

    /// <summary>
    /// 继承自Singleton<T>的单例
    /// </summary>
    public class SingletonB : Singleton<SingletonB>
    {
        public void DoSomething()
        {
            Debug.Log("SingletonB:DoSomething!");
        }
    }
}