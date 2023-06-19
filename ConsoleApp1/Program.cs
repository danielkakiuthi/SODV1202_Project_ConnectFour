using System;

public class Base
{
    public Base()
    {
        Console.WriteLine("Base Constructor");
    }

    public virtual void Func()
    {
        Console.WriteLine("Base Func");
    }
}

public class Derived : Base
{
    public Derived()
    {
        Console.WriteLine("Derived Constructor");
    }

    public override void Func()
    {
        Console.WriteLine("Derived Func");
    }
}

class Program
{
    static void Main(string[] args)
    {
        var b = new Base();
        b.Func();
        var d = new Derived();
        d.Func();
    }
}
