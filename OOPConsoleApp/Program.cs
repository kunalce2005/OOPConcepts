using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPConsoleApp
{
    
    class Program
    {

        static void Main(string[] args)
        {
            ClassA obj = new ClassA();
            // Calls different method even though parameter is optional
            obj.Test1();
            obj.Test1(0);

            TestShadowOrMethdHiding();

            TestOverriding();
            TestOverridingAndShadowing();
        }

        static void TestShadowOrMethdHiding()
        {

            Console.WriteLine("*************Test Shadowing/Method hiding");
            FileLogger obj1 = new FileLogger();
            obj1.Log(); // Calls FileLogger's method as reference is of type BaseLogger

            BaseLogger obj2= new FileLogger();
            obj2.Log(); // Calls BaseLogger's method as reference is of type BaseLogger & FileLog only creates shadow of parent's Log() method

            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
        }
        
        static void TestOverriding() 
        {

            Console.WriteLine("*************Test Overriding");
            LogError obj1 = new LogError();
            obj1.Log();

            obj1 = new LogErrorAndSendMail();
            obj1.Log(); // Calls LogErrorAndSendMail's Log method, as it overrides implementation of LogError class

            obj1 = new LogErrorAndSendMailAndRaiseTicket();
            obj1.Log(); // Calls LogErrorAndSendMailAndRaiseTicket's Log method, as it overrides implementation of LogErrorAndSendMail class

            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
        }

        static void TestOverridingAndShadowing()
        {

            Console.WriteLine("*************Test Overriding and Shadowing");
            LogError obj1 = new LogErrorAndSendMailAndRaiseTicket();
            obj1.LogStackTrace(); // Calls LogError's Log method, as it LogErrorAndSendMailAndRaiseTicket & LogErrorAndSendMail has not overriden implementation of LogError class

            //obj1 = new LogErrorAndSendMail();
            //obj1.Log(); // Calls LogErrorAndSendMail's Log method, as it overrides implementation of LogError class

            //obj1 = new LogErrorAndSendMailAndRaiseTicket();
            //obj1.Log(); // Calls LogErrorAndSendMailAndRaiseTicket's Log method, as it overrides implementation of LogErrorAndSendMail class

            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
        }

    }

    #region "Abstact"
    public abstract class AbstractBase // Can declare class as abstract without having any abstract method. But should declare class as abstract if it cannot any abstract methodf.
    {
        public abstract void Method1();
    }

    public abstract class AbstractChild: AbstractBase // should mark class as abstract if it inherits abstract class but doesn't implement parent class's members
    {
    }
    #endregion

    #region "Overloading"
    public class ClassA
    {
        public void Test1(){}
        public void Test1(int i=0) { } // VALID with even though Test1() is there
        //public string Test1(int i = 0) { return ""; } // INVALID as overloading considers only method signature(method name+parameters) & not return value type

        public void Test1(ref int j) { } // VALID as ref & val type are considered different
        //public void Test1(out int j) { } // INVALID as ref & out type are considered same
        //public void Test1(ref int j=0) { } // INVALID as ref/out parameter cannot have default value
    }
    #endregion
    
    #region "Constructor Calling"
    public class Base
    {
        // Default constructor is the one which has no parameters or has parameters with default values

        //public Base() // Default constructor
        //{
        //}
        //public Base(double i = 0) // Default constructor
        //{
        //}

        // If you don't define any constructor, it will generate default constructor
        public Base(int i) // If you define any constructor, it will not generate default constructor
        {
        }
    }

    public class Child : Base
    {

        //public Child() //INVALID as it always tries to call default constructor of parent class(i.e. base() which is not found)
        //{
        //}

        //public Child(int i) //INVALID as it always tries to call default constructor of parent class(i.e. base() which is not found)
        //{
        //}

        public Child(int i): base(i) // VALID can override calling parent's default constructor by explictly calling parent's desired constructor (i.e. base(i))
        {
        }
        
    }

    //public class GrandChild : Child // INVALID as "Child" class doesn't contain default constructor
    //{
    //    //public GrandChild(int i) // INVALID as "Child" class doesn't contain default constructor
    //    //{
    //    //}
    //}

    #endregion

    #region "Shadow/Method Hiding"

    public class BaseLogger
    {
        public void Log()
        {
            Console.WriteLine("Base Log");
        }
        public void Log1(int a)
        {
            Console.WriteLine("Base Log");
        }
        public void Log1(ref int a)
        {
            a = 0;
            Console.WriteLine("Base Log");
        }
    }

    public class FileLogger : BaseLogger
    {
        public void Log() // If you specify same method, by default it does shadowing
        {
            Console.WriteLine("FileLog");
        }
        public void Log1(out int a)
        {
            a = 0;
            Console.WriteLine("Base Log");
        }
        //public new void Log() // Same as above, just added "new" keyword to explicitly mark shadowing
        //{
        //    Console.WriteLine("FileLog");
        //}
    }

    #endregion

    #region "Overriding and Shadowing"

    public class LogError
    {
        public virtual void Log() // Bydefault method is non-virtual. Make it virtual to let it overridden in it's Inheriting class
        {
            Console.WriteLine("Log Error");
        }

        public virtual void LogStackTrace()
        {
            Console.WriteLine("Log StackTrace");
        }
    }

    public class LogErrorAndSendMail : LogError
    {
        public override void Log() // Override virtual method. "Override" method is always Virtual by default(can be overridden).
        {
            Console.WriteLine("Log Error & Send Mail");
        }

        ///public override virtual void LogStackTrace() // INVALID as member marked as override cannot be marked as new or virtual
        public override void LogStackTrace()
        {
            Console.WriteLine("Log StackTrace & Send Mail");
        }
    }


    public class LogErrorAndSendMailAndRaiseTicket : LogErrorAndSendMail
    {
        public override void Log() // By default method is non-virtual. Make it virtual to let it overridden in it's Inheriting class
        {
            Console.WriteLine("Log Error & Send Mail & Raise Ticket");
        }

        public new void LogStackTrace()
        {
            Console.WriteLine("Log StackTrace & Send Mail & Raise Ticket");
        }
    }

    #endregion

}
