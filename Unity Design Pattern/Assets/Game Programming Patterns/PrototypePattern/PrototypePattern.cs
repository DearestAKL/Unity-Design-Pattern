using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrototypePattern
{
    public class PrototypePattern : MonoBehaviour
    {
        void Start()
        {
            HousePrototype house = new HousePrototype("house");
            HousePrototype houseClone = new HousePrototype("houseClone");
            Debug.Log("HouseCloned: " + houseClone.Id);

            CarPrototype car = new CarPrototype("car");
            CarPrototype carClone = new CarPrototype("carClone");
            Debug.Log("CarCloned: " + carClone.Id);
        }
    }

    abstract class Prototype
    {
        private string id;

        public Prototype(string id)
        {
            this.id = id;
        }

        public string Id
        {
            get { return id; }
        }

        public abstract Prototype Clone();
    }

    class HousePrototype : Prototype
    {
        public HousePrototype(string id) : base(id)
        {

        }
        public override Prototype Clone()
        {
            return (Prototype)this.MemberwiseClone();
        }
    }

    class CarPrototype : Prototype
    {
        public CarPrototype(string id) : base(id)
        {

        }
        public override Prototype Clone()
        {
            return (Prototype)this.MemberwiseClone();
        }
    }
}