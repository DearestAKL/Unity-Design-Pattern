using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ComponentPatternExample
{
    public class CommandPattern : MonoBehaviour
    {
        Game rpgGame = new Game();

        void Start()
        {
            rpgGame.Start();
        }

        void Update()
        {
            rpgGame.Update();
        }
    }

    class Game
    {
        public int x = 0, y = 0;

        //命令
        //已经执行过的命令栈
        private Stack<Command> executeCommands = new Stack<Command>();
        //回溯的命令栈
        private Stack<Command> undoCommands = new Stack<Command>();

        //组件
        private InputComponent inputComponent;

        //组件List
        public List<BaseComponent> ComponentList = new List<BaseComponent>();
        //组件List容量
        int componentAmount = -1;

        public void Start()
        {
            inputComponent = new PlayerInputComponent();

            ComponentList.Add(inputComponent);

            Debug.Log("Game Components Initialization Finish...");
            Debug.Log("Please enter LeftArrow or RightArrow button to play...");
        }

        public void Update()
        {
            if (ComponentList == null)
            {
                return;
            }
            componentAmount = ComponentList.Count;
            for (int i = 0; i < componentAmount; i++)
            {
                ComponentList[i].Update(this);
            }
        }

        public void MoveTo(int x, int y)
        {
            this.x = x;
            this.y = y;

            Debug.Log($"X:{this.x},Y:{this.y}");
        }

        //执行命令
        public void Execute(Command command)
        {
            command.execute();
            executeCommands.Push(command);
        }

        //撤消命令
        public void Undo()
        {
            if (executeCommands.Count <= 0)
            {
                Debug.LogWarning("没有可以撤消的命令");
                return;
            }
            var command = executeCommands.Pop();
            command.undo();
            undoCommands.Push(command);
        }

        //重做命令
        public void Redo()
        {
            if(undoCommands.Count <= 0)
            {
                Debug.LogWarning("没有可以重做的命令");
                return;
            }
            var command = undoCommands.Pop();
            command.execute();
            executeCommands.Push(command);
        }
    }

    #region 组件
    /// <summary>
    /// 输入组件
    /// </summary>
    class PlayerInputComponent : InputComponent
    {
        public void Update(Game game)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                int destX = game.x - WALK_ACCELERATION;
                game.Execute(new MoveUnitCommand(game, destX, game.y));
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                int destX = game.x + WALK_ACCELERATION;
                game.Execute(new MoveUnitCommand(game, destX, game.y));
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                game.Undo();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                game.Redo();
            }
        }

        private int WALK_ACCELERATION = 1;
    }
    #endregion

    #region 接口
    interface BaseComponent
    {
        void Update(Game game);
    }
    interface InputComponent : BaseComponent
    {
        new void Update(Game game);
    }
    #endregion

    #region 命令
    class Command
    {
        public Command(){}

        public virtual void execute(){}

        public virtual void undo(){}
    }

    class MoveUnitCommand : Command
    {
        private Game game;
        private int xBefore, yBefore;
        private int xNow, yNow;
        public MoveUnitCommand(Game game,int x,int y)
        {
            this.game = game;
            xBefore = game.x;
            yBefore = game.y;
            xNow = x;
            yNow = y;
        }

        public override void execute()
        {
            base.execute();
            game.MoveTo(xNow, yNow);
        }

        public override void undo()
        {
            base.undo();
            game.MoveTo(xBefore, yBefore);
        }
    }
    #endregion
}