using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ComponentPattern
{
    public class ComponentPattern : MonoBehaviour
    {
        Game game = new Game();

        void Start()
        {
            game.Start();
        }

        void Update()
        {
            game.Update();
        }
    }

    /// <summary>
    /// 游戏类
    /// </summary>
    class Game
    {
        //组件
        private IInputComponent inputComponent;
        private IPhysicsComponent physicsComponent;
        private IGraphicsComponent graphicsComponent;
        //组件List
        public List<IBaseComponent> ComponentList = new List<IBaseComponent>();

        public void Start()
        {
            inputComponent = new PlayerInputComponent();
            physicsComponent = new PlayerPhysicsComponent();
            graphicsComponent = new PlayerGraphicsComponent();

            ComponentList.Add(inputComponent);
            ComponentList.Add(physicsComponent);
            ComponentList.Add(graphicsComponent);

            Debug.Log("组件初始化完成");
        }

        public void Update()
        {
            if (ComponentList == null)
            {
                return;
            }
            var count = ComponentList.Count;
            for (int i = 0; i < count; i++)
            {
                ComponentList[i].Update(this);
            }
        }
    }

    #region 接口

    interface IBaseComponent
    {
        void Update(Game game);
    }

    interface IGraphicsComponent : IBaseComponent
    {
        new void Update(Game game);
    }

    interface IPhysicsComponent : IBaseComponent
    {
        new void Update(Game game);
    }

    interface IInputComponent : IBaseComponent
    {
        new void Update(Game game);
    }

    #endregion

    #region 组件

    class PlayerGraphicsComponent : IGraphicsComponent
    {
        public void Update(Game game)
        {
            
        }
    }

    class PlayerPhysicsComponent : IPhysicsComponent
    {
        public void Update(Game game)
        {

        }
    }

    class PlayerInputComponent : IInputComponent
    {
        public void Update(Game game)
        {

        }
    }
    #endregion
}