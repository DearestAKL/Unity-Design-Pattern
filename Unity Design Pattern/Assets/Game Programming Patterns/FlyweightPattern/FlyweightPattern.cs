using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlyweightPattern
{
    public class FlyweightPattern : MonoBehaviour
    {
        void Start()
        {
            TreeFactory factory = new TreeFactory();
            string[] strs = new string[7] { "TreeA", "TreeB", "TreeC", "TreeA", "TreeB", "TreeC", "TreeB" };

            int pointSize = 10;

            for (int i = 0; i < strs.Length; i++)
            {
                Tree tree = factory.GetTree(strs[i]);
                tree.Display(pointSize);
            }
        }
    }

    class TreeFactory
    {
        private Dictionary<string, Tree> trees = new Dictionary<string, Tree>();

        public Tree GetTree(string name)
        {
            Tree tree = null;
            if (trees.ContainsKey(name))
            {
                tree = trees[name];
            }
            else
            {
                switch (name)
                {
                    case "TreeA":
                        tree = new TreeA();
                        break;
                    case "TreeB":
                        tree = new TreeB();
                        break;
                    case "TreeC":
                        tree = new TreeC();
                        break;
                    default:
                        break;
                }
            }
            return tree;
        }
    }

    abstract class Tree
    {
        protected string name;
        protected int height;
        protected int width;
        protected Color color;

        protected int pointSize;

        public abstract void Display(int pointSize);
    }

    class TreeA : Tree
    {
        public TreeA()
        {
            this.name = "TreeA";
            this.height = 100;
            this.width = 20;
            this.color = Color.red;
        }

        public override void Display(int pointSize)
        {
            this.pointSize = pointSize;
            Debug.Log(this.name +
              " (pointsize " + this.pointSize + ")");
        }
    }

    class TreeB : Tree
    {
        public TreeB()
        {
            this.name = "TreeB";
            this.height = 120;
            this.width = 30;
            this.color = Color.green;
        }

        public override void Display(int pointSize)
        {
            this.pointSize = pointSize;
            Debug.Log(this.name +
              " (pointsize " + this.pointSize + ")");
        }
    }

    class TreeC : Tree
    {
        public TreeC()
        {
            this.name = "TreeC";
            this.height = 50;
            this.width = 10;
            this.color = Color.black;
        }

        public override void Display(int pointSize)
        {
            this.pointSize = pointSize;
            Debug.Log(this.name +
              " (pointsize " + this.pointSize + ")");
        }
    }
}