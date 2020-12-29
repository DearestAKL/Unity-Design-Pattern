using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern
{
    /// <summary>
    /// 状态模式 状态机
    /// </summary>
    public class StatePattern : MonoBehaviour
    {
        private Heroine heroine;

        void Start()
        {
            heroine = new Heroine();
        }


        void Update()
        {
            heroine.HandleInput();
            heroine.Update();
        }
    }

    public enum HeroineState
    {
        Standing,
        Jumping,
        Ducking,
        Driving
    }

    public class Heroine
    {
        IHeroineBaseState state;
        static StandingState standing;
        static JumpingState jumping;
        static DuckingState ducking;
        static DrivingState driving;


        public Heroine()
        {
            standing = new StandingState(this);
            jumping = new JumpingState(this);
            ducking = new DuckingState(this);
            driving = new DrivingState(this);

            SetHeroineState(HeroineState.Standing);
        }

        public void SetHeroineState(HeroineState stateType)
        {
            if(state != null)
                state.OnLeave();

            switch (stateType)
            {
                case HeroineState.Standing:
                    state = standing;
                    break;
                case HeroineState.Jumping:
                    state = jumping;
                    break;
                case HeroineState.Ducking:
                    state = ducking;
                    break;
                case HeroineState.Driving:
                    state = driving;
                    break;
                default:
                    break;
            }

            state.OnEnter();
        }

        public void HandleInput()
        {
            state.HandleInput();
        }

        public void Update()
        {
            state.Update();
        }
    }

    public interface IHeroineBaseState
    {
        void Update();

        void HandleInput();

        void OnEnter();

        void OnLeave();
    }

    /// <summary>
    /// 站立状态
    /// </summary>
    public class StandingState : IHeroineBaseState
    {
        private Heroine heroine;
        public StandingState(Heroine heroine)
        {
            this.heroine = heroine;
        }

        public void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                heroine.SetHeroineState(HeroineState.Jumping);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                heroine.SetHeroineState(HeroineState.Ducking);
            }
        }

        public void OnEnter()
        {
            Debug.Log("进入站立状态");
        }

        public void OnLeave()
        {
            Debug.Log("离开站立状态");
        }

        public void Update()
        {
            //Debug.Log("站立状态");
        }
    }

    /// <summary>
    /// 跳跃状态
    /// </summary>
    public class JumpingState : IHeroineBaseState
    {
        private Heroine heroine;
        public JumpingState(Heroine heroine)
        {
            this.heroine = heroine;
        }

        public void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Debug.Log("已经在跳跃状态中！");
                return;
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                heroine.SetHeroineState(HeroineState.Driving);
            }
        }

        public void OnEnter()
        {
            Debug.Log("进入跳跃状态");
        }

        public void OnLeave()
        {
            Debug.Log("离开跳跃状态");
        }

        public void Update()
        {
            //Debug.Log("跳跃状态");
        }
    }

    /// <summary>
    /// 下蹲状态
    /// </summary>
    public class DuckingState : IHeroineBaseState
    {
        private Heroine heroine;
        public DuckingState(Heroine heroine)
        {
            this.heroine = heroine;
        }

        public void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                heroine.SetHeroineState(HeroineState.Standing);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Debug.Log("已经在下蹲状态中！");
                return;
            }
        }

        public void OnEnter()
        {
            Debug.Log("进入下蹲状态！");
        }

        public void OnLeave()
        {
            Debug.Log("离开下蹲状态！");
        }

        public void Update()
        {
            //Debug.Log("下蹲状态！");
        }
    }

    //下斩状态
    public class DrivingState : IHeroineBaseState
    {
        private Heroine heroine;
        public DrivingState(Heroine heroine)
        {
            this.heroine = heroine;
        }

        public void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                heroine.SetHeroineState(HeroineState.Standing);
            }
        }

        public void OnEnter()
        {
            Debug.Log("进入下斩状态！");
        }

        public void OnLeave()
        {
            Debug.Log("离开下斩状态！");
        }

        public void Update()
        {
            //Debug.Log("下斩状态！");
        }
    }
}