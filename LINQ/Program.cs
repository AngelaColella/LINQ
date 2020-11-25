using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace LINQconsole
{
    class Program
    {
        public delegate int Sum(int val1, int val2);
        //il delegate è la firma di una funzione 
        //Posso assegnargli una qualunque funzoone che prende due int e restituisce due int

        public static int PrimaSomma(int val1, int val2)
        {
            return val1 + val2;
        }
        public static int SecondaSomma(int valore1, double valore2)
        {
            return valore1 + (int)valore2;
        }

        public static void CallMe(Sum functionToInvoke)
        {
            functionToInvoke(1, 2);
        }

        public static int AnotherFunc() { return 0; }


        static void Main(string[] args)
        {

            #region BusinessProcess

            var process = new BusinessProcess();

            // sottoscrizione dell'evento
            process.Started += Process_Started;
            process.Started += Process_Started1;
            process.StartedCore += Process_StartedCore;
            //Process_Started è l'event handler (metodo)
            //quando sollevo l'evento Process_Started è in ascolto 

            process.Completed += Process_Completed;
            process.CompletedCore += Process_CompletedCore;
            process.ProcessDATA(); 
            #endregion

            #region STEP ONE: EXTENSION METHODS on STRINGS
   

            //Definisco un anonymous type 
            var persona = new { a = 1, b = 2 };
            var persona2 = new { aa = 2, bb = 1 };
            //Il compilatore non riconosce che sono la stessa cosa, sono diverse.
            //In memoria come classe, indirizzo nello stack, il corpo nell'heap.
            //Non ho dichiarato un tipo -> NON POSSONO ESSERE DELLO STESSO TIPO
            //Non posso instanziarli

            string hello = "20.01";
            double a = hello.ToDouble();
            //ToDouble è della classe stringExtension
            //L'intellegence riconosce la classe e riconosce ToDouble come extension method senza necessità di istanziare la classe

            string example = "230";
            example.ToUpper(); //metodo di string
            Console.WriteLine(example.ToDouble()); //metodo di StringExtension (extension method)

            var string_with_prefix = example.WithPrefix("[TST]"); //metodo di StringExtension (extension method)
            Console.WriteLine(string_with_prefix);

            MyString example2 = new MyString(); 
            example2.Value = "Example";
            example2.Value.ToUpper(); //metodo di string
            // OPPURE
            example2.ToUpper(); //metodo di MyString

            #endregion

            #region SOMMA
            Sum lamiaSomma = new Sum(PrimaSomma);
            // OPPURE
            Sum lamiaSomma2 = PrimaSomma;

            // Sum == Func<int, int, int>

            // Func and Action
            Func<int, double, int> primaFunc = SecondaSomma; // output1, output2, input
            Func<int, int, int> secondaFunc = PrimaSomma;

            Action<int> primaAction;

            //// ERRORE!!! Wrong Signature
            //// lamiaSomma = SecondaSomma;

            //CallMe(lamiaSomma);
            //CallMe(PrimaSomma);

            //// ERRORE
            //CallMe(SecondaSomma);

            #endregion

            #region LAMBDA
            // Vediamo 3 modi per scrivere una funzione che moltiplica un int in input per 2

            // 1. Metodo Multiply usato in modo "convenzionale"
            int result = Multiply(3);

            // 2. Lambda
            // (input)  => expression 
            Func<int, int> lamdbaZero = x => 2 * x;
            lamdbaZero(3);

            // 3. Lambda (in questo caso non sarebbe necessaria una sequenza di statements)
            // (input)  => {statements}
            Func<int, int> lamdbaZeroZero = x =>
            {
                var result = 2 * x;
                return result;
            };
            lamdbaZeroZero(3);

            // 4. Lambda 
            Func<int, int> lamdbaZeroZero1 = x => { return 2 * x; };

            // 5. Metodo Multiply associato ad una funzione. Non è una lambda
            Func<int, int> lamdbaZeroZeroZero = Multiply;


            // Vediamo cosa c'è dietro all'espressione riportata nella slide:
            // var results = Where(dataInt, x => x > 2);
            // Scriviamo il metodo Where

            var list = new List<int> { 1, 2, 3, 4, 5, 6 };
           
            var results = list.Where(x => x > 2);

            Func<int, double, bool> lambdaOne = (x, y) => x > (int)y; 

            #endregion

            #region EMPLOYEE
            //List<Employee<int>> data = new List<Employee<int>>
            //{
            //    new Employee<int>()
            //}; 

            Employee<int> firstEmployee = new Employee<int>();

            List<Employee> data = new List<Employee> { };
            foreach (var value in data)
                Console.WriteLine(value.Name);

            foreach (var value in data)
                Console.WriteLine("#" + value.Name);

            var person = new { firstName = "Roberto", lastName = "Ajolfi", eta = 12 };

            var person2 = new { nome = "Alice", cognome = "Colella" };

            var person3 = person2;

            //List<EmployeeInt> employees = new List<EmployeeInt>
            //{
            //    new EmployeeInt { ID = 1, Name ="Roberto"},
            //    new EmployeeInt { ID = 2, Name ="Alice"},
            //    new EmployeeInt { ID = 3, Name ="Mauro"},
            //    new EmployeeInt { ID = 4, Name ="Roberto"},
            //};

            //var result = employees.Where("ID", "1");
            //var result2 = employees.Where("Name", "Roberto");

            //// value => value * value

            //ParameterExpression y = Expression.Parameter(typeof(int), "value");
            //var basettoni = new ParameterExpression[] {
            //    y
            //};

            //Expression<Func<int, int>> squareExpression =
            //   Expression.Lambda<Func<int, int>>(
            //    Expression.Multiply(y, y),
            //    basettoni
            //   );

            //Expression<Func<int, int>> squareExpression2 = value => value * value;

            //Func<int, int> funzione = squareExpression.Compile();
            //Console.WriteLine(funzione(3)); 
            #endregion


        }

        #region Event Handler legati ai delegate dichiarati in BusinessProcess
        private static void Process_CompletedCore(object sender, BusinessProcess.ProcessEndEventArgs e)
        {
            Console.WriteLine("Ciao, sono l'handler di CompletedCore, la durata del processo è {0} ms", e.Duration);
        }

        private static void Process_StartedCore(object sender, EventArgs e)
        {
            //Console.WriteLine("Ciao, sono l'handler del delegate non creato, il processo è iniziato.");
            Console.WriteLine("Ricevuto StartedCore");
        }

        private static void Process_Completed(int duration)
        {
            //Console.WriteLine("Ciao, sono l'handler di process Completed, il processo ha impiegato {0} ms", duration);
            Console.WriteLine($"Process Completed (duration: {duration})");
        }

        private static void Process_Started1()
        {
            Console.WriteLine("Ciao, sono il secondo handler, il processo è iniziato.");
        }

        private static void Process_Started()
        {
            Console.WriteLine("Ciao, sono il primo handler, il processo è iniziato.");
        }
        #endregion

        private static List<int> Where(List<int> data, Func<int, bool> condizione)
        {
            var results = new List<int>();

            foreach (int value in data)
                if (condizione(value))
                    results.Add(value);

            return results;
        }

        private static int SampleForLambda(int val1, int val2)
        {
            return val1 * val2;
        }

        private static int Multiply(int x)
        {
            return 2 * x;
        }
    }

    #region CLASSES EMPLOYEEE
    internal class Employee
    {
        //internal = visibile solo nell'assembly corrente.
        public string Name { get; set; }
    }
    internal class Employee<T>
    {
        public T ID { get; set; }
        public string Name { get; set; }
    }
    internal class EmployeeInt
    {
        public string Name { get; set; }

        private int _id;
        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("ID must be greater than zero.");
                _id = value;
            }
        }
    }
    internal class EmployeeString
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
    #endregion

    internal class MyString
    {
        public string Value { get; set; }

        public string ToUpper()
        {
            return Value.ToUpper();
        }
    }

}
