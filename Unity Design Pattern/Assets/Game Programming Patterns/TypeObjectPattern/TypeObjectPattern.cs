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
        private int attack;

        public int GetAttack
        {
            get { return attack; }
        }

    }

}