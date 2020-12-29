using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TypeObjectPattern
{
    public class TypeObjectPattern : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

    public abstract class Monster
    {
        private int health;

        public int GetHealth
        {
            get { return health; }
        }
    }
}