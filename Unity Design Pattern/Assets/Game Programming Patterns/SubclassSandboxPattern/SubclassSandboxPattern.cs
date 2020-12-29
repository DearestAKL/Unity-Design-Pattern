using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubclassSandboxPattern
{
    public class SubclassSandboxPattern : MonoBehaviour
    {
        List<SuperPower> superPowers = new List<SuperPower>();

        private float elapsedTime = 0f;

        void Start()
        {
            superPowers.Add(new SkyLaunch());
            superPowers.Add(new GroundDive());
            superPowers.Add(new FlashSpeed());
            elapsedTime = Time.realtimeSinceStartup;
        }


        void Update()
        {
            //Trigger once per second（每一秒触发一次）
            if (Time.realtimeSinceStartup - elapsedTime > 1f)
            {
                for (int i = 0; i < superPowers.Count; i++)
                {
                    superPowers[i].Activate();
                }
                elapsedTime = Time.realtimeSinceStartup;
            }
        }
    }

    public abstract class SuperPower
    {
        public abstract void Activate();

        protected void Move(float speed)
        {
            Debug.Log("移动速度： " + speed + "(速度)");
        }

        protected void PlaySound(string soundEffect)
        {
            Debug.Log("播放音效： " + soundEffect + "(音效)");
        }

        protected void SpawnParticles(string particles)
        {
            Debug.Log("生成粒子特效： " + particles + "(粒子特效)");
        }
    }

    public class SkyLaunch : SuperPower
    {
        //子类实现自己版本的Activate()方法
        public override void Activate()
        {
            Debug.Log("--------------------------SkyLaunch SuperPower Activate!--------------------");
            //组合子类自己独特的功能
            Move(10f);
            PlaySound("SkyLaunch");
            SpawnParticles("SkyLaunch Particles");
        }
    }

    public class GroundDive : SuperPower
    {
        //子类实现自己版本的Activate()方法
        public override void Activate()
        {
            Debug.Log("--------------------------GroundDive SuperPower Activate!--------------------");
            //组合子类自己独特的功能
            Move(10f);
            PlaySound("GroundDive");
            SpawnParticles("GroundDive Particles");
        }
    }

    public class FlashSpeed : SuperPower
    {
        //子类实现自己版本的Activate()方法
        public override void Activate()
        {
            Debug.Log("--------------------------FlashSpeed SuperPower Activate!--------------------");
            //组合子类自己独特的功能
            Move(10f);
            PlaySound("FlashSpeed");
            SpawnParticles("FlashSpeed Particles");
        }
    }
}