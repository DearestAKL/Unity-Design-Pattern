using UnityEngine;

namespace ObjectPoolPattern
{
    public class PoolObject : MonoBehaviour
    {
        //名称
        public string poolName;
        //是否已在池中（还未使用，待使用）
        public bool inPool;
    }
}