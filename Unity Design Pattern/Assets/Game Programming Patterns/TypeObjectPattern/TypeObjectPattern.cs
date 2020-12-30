using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TypeObjectPattern
{
    public class TypeObjectPattern : MonoBehaviour
    {
        void Start()
        {
            //创建种类
            Breed Troll = new Breed(null, 25, "The troll hits you!");
            Breed TrollArcher = new Breed(Troll, 0, "The troll archer fires an arrow!");
            Breed TrollWizard = new Breed(Troll, 0, "The troll wizard casts a spell on you!");

            //通过种类 创建monster对象
            Monster troll = Troll.NewMonster();
            troll.ShowAttack();

            Monster trollArcher = TrollArcher.NewMonster();
            trollArcher.ShowAttack();

            Monster trollWizard = TrollWizard.NewMonster();
            trollWizard.ShowAttack();
        }
    }

    /// <summary>
    /// 种类
    /// </summary>
    public class Breed
    {
        private int health;//初始值
        private string attack;
        Breed parent;

        public Breed(Breed parent, int health, string attack)
        {
            this.parent = null;
            this.health = health;
            this.attack = attack;

            if(parent != null)
            {
                this.parent = parent;

                //是0，从父层拿
                if (health == 0)
                {
                    this.health = parent.GetHealth();
                }
                //是null，从父层拿
                if (attack == null)
                {
                    this.attack = parent.GetAttack();
                }
            }
        }

        public int GetHealth()
        {
            return health;
        }

        public string GetAttack()
        {
            return attack;
        }

        public Monster NewMonster()
        {
            return new Monster(this);
        }
    }

    /// <summary>
    /// 怪物
    /// </summary>
    public class Monster
    {
        private int health;
        private string attack;
        private Breed breed;

        public Monster(Breed breed)
        {
            this.breed = breed;
            health = breed.GetHealth();
            attack = breed.GetAttack();
        }

        public string GetAttack()
        {
            return attack;
        }

        public void ShowAttack()
        {
            Debug.Log(attack);
        }
    }
}