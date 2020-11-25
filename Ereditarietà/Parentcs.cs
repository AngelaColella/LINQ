using System;
using System.Collections.Generic;
using System.Text;

namespace Ereditarietà
{
    public class Parent
    {
        public int ID { get; set; }
        public virtual void MethodOne() { }
    }

    public class ParentTwo
    {
        public int ID { get; set; }
    }

    public class Child : Parent, IParentTwo
    {
        public int ID2 { get; set; }
    }

    public interface IParentTwo
    {
        public int ID2 { get; set; }
    }
    
    // composizione
    //public class ChildTwo : Parent, IParentTwo
    //{
    //    private ParentTwo ParentTwo { get; set; }
    //    public int ID2 { get => ParentTwo.ID2; set => ParentTwo.ID2 = value; }
    //}


}
