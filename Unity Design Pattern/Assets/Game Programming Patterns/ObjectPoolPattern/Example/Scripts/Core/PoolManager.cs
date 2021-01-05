using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPoolPattern
{
    /// <summary>
    /// 对象池类
    /// </summary>
    public class Pool
    {
        //池子对象可用对象的栈
        private Stack<PoolObject> availableObjectStack = new Stack<PoolObject>();

        //名字
        private string poolName;
        //预制体对象
        private GameObject prefab;
        //尺寸
        private int poolSize;
        //是否固定容量
        private bool fixedSize;

        /// <summary>
        /// 池子构造函数
        /// </summary>
        /// <param name="poolName">对象池名字</param>
        /// <param name="prefab">对象池预制体</param>
        /// <param name="poolSize">对象池初始对象数量</param>
        /// <param name="fixedSize">对象池是否固定容量</param>
        public Pool(string poolName,GameObject prefab,int poolSize, bool fixedSize)
        {
            this.poolName = poolName;
            this.prefab = prefab;
            this.poolSize = poolSize;
            this.fixedSize = fixedSize;
            for (int i = 0; i < poolSize; i++)
            {

            }
        }

        /// <summary>
        /// 将对象加入池，复杂度o(1)
        /// </summary>
        private void AddObjectToPool(PoolObject po)
        {
            po.gameObject.SetActive(false);
            availableObjectStack.Push(po);
            po.inPool = true;
        }

        /// <summary>
        /// 新建一个对象实例
        /// </summary>
        private PoolObject NewObjectInstance()
        {
            GameObject go = GameObject.Instantiate(prefab);
            PoolObject po = go.GetComponent<PoolObject>();
            if(po == null)
            {
                po = go.AddComponent<PoolObject>();
            }
            po.poolName = poolName;
            return po;
        }

        /// <summary>
        /// 获取一个池中对象，如果池子中无可用对象，则新建一个对象实例
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <returns></returns>
        public PoolObject NextAvailableObject(Vector3 position,Quaternion rotation)
        {
            PoolObject po = null;
            if(availableObjectStack.Count > 0)
            {
                //池子中 有 从池子中取
                po = availableObjectStack.Pop();
            }
            else if(fixedSize)
            {
                //池子中无对象 生成一个新的
                poolSize++;
                po = NewObjectInstance();
                Debug.Log($"扩充池子{poolName}");
            }
            else
            {
                Debug.LogWarning($"对象池{poolName}内无可用对象");
            }

            GameObject result = null;
            if(po != null)
            {
                po.inPool = false;
                result = po.gameObject;
                result.SetActive(true);

                result.transform.position = position;
                result.transform.rotation = rotation;
            }

            return po;
        }

        public void ReturnObjectToPool(PoolObject po)
        {
            if (poolName.Equals(po.poolName))
            {
                if (po.inPool)
                {
                    Debug.LogWarning("已在池子中");
                }
                else
                {
                    AddObjectToPool(po);
                }
            }
            else
            {
                Debug.LogError($"没有池子{poolName}");
            }
        }
    }

    public class PoolManager : MonoBehaviour
    {
        #region 单例
        private static PoolManager instance;
        public static PoolManager Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new PoolManager();
                }
                return instance;
            }
        }
        #endregion

        /// <summary>
        /// 对象池索引<对象池名称，对应的池子类>
        /// </summary>
        private Dictionary<string, Pool> poolDic = new Dictionary<string, Pool>();
        public Dictionary<string,Pool> PoolDic
        {
            get { return poolDic; }
            set { poolDic = value; }
        }

        [Header("[对象池信息，(运行时修改无效)]")]
        public PoolInfo[] poolInfos;

        PoolManager()
        {
            instance = this;
        }

        public void Init()
        {
            CheckForDuplicatePoolNames();
            CreateAllPools();
        }

        /// <summary>
        /// 检测对象池是否重名
        /// </summary>
        private void CheckForDuplicatePoolNames()
        {
            string poolName;
            for (int i = 0; i < poolInfos.Length; i++)
            {
                poolName = poolInfos[i].poolName;
                if(poolName.Length == 0)
                {
                    Debug.LogError("对象池没有名字");
                }
                for (int j = i+1; j < poolInfos.Length; j++)
                {
                    if (poolName.Equals(poolInfos[j].poolName))
                    {
                        Debug.LogError("已存在该对象池命");
                    }
                }
            }
        }

        /// <summary>
        /// 创建所有对象池
        /// </summary>
        private void CreateAllPools()
        {
            for (int i = 0; i < poolInfos.Length; i++)
            {
                var poolInfo = poolInfos[i];
                Pool pool = new Pool(poolInfo.poolName, poolInfo.prefab, poolInfo.poolSize, poolInfo.fixedSize);
                poolDic[poolInfo.poolName] = pool;
            }
        }

        public PoolObject GetPoolObjectFromPool(string poolName,Vector3 position,Quaternion rotation)
        {
            PoolObject result = null;

            if (poolDic.ContainsKey(poolName))
            {
                Pool pool = poolDic[poolName];
                result = pool.NextAvailableObject(position, rotation);

                if (result == null)
                {
                    Debug.LogWarning("池子内有没有可用对象，且不可扩充: " + poolName);
                }

            }
            else
            {
                Debug.LogError("池子不存在: " + poolName);
            }

            return result;
        }

        public void ReturnObjectToPool(PoolObject po)
        {
            if (po == null)
            {
                Debug.LogWarning("池子对象为空" );
            }
            else
            {
                if (poolDic.ContainsKey(po.poolName))
                {
                    Pool pool = poolDic[po.poolName];
                    pool.ReturnObjectToPool(po);
                }
                else
                {
                    Debug.LogWarning("池子不存在: " + po.poolName);
                }
            }
        }
    }

    /// <summary>
    /// 对象池信息
    /// </summary>
    [System.Serializable]
    public class PoolInfo
    {
        //名称
        public string poolName;
        //prefab对象
        public GameObject prefab;
        //尺寸
        public int poolSize;
        //是否固定尺寸的池
        public bool fixedSize;
    }
}