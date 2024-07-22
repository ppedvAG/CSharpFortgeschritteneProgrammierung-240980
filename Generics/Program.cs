using Sprachfeatures;

namespace Generics
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nGenericConstraintSample");
            GenericConstraintSample();

            Console.WriteLine("\nTest Datastore");
            var dataStore = new DataStore<SomeFooClass>();

            dataStore.Add(0, new SomeFooClass());
            dataStore.AndNowForSomethingSpecial(new { Baz = 13 });
        }

        private static void GenericConstraintSample()
        {
            // Problemstellung: Wenn wir sehr viele Interfaces verwenden, weil wir bestimmte Auspraegungen haben wollen,
            // kann es sehr schnell unuebersichtlich werden (Interface-"Wald")
            // Loesung sind constraints fuer genersiche Klassen
            var superInstance = new MyClassWithASuperInterace();

            var simpleInstanceWithMultipleInterfaces = new MyClassForTheGenericContraintSample();

            // Alter Ansatz: Ein Superinterface was mehrere Methoden besitzt.
            void DoSomething(ISuperInterace instance)
            {
                instance.Cook();
                instance.Eat();
            }

            // Besserer Ansatz: Ein Interface mit Constraints fuer die Interfaces welche ich haben moechte
            void DoSomethingBetter<T>(T instance) where T : IEatable, ICookable
            {
                //var newInstance = new T(); // Geht nicht

                // das war die alternative welche aber nicht typsicher ist
                // hier koennen viele Arten von Exception fliegen
                var newInstance = (T)Activator.CreateInstance(typeof(T));

                instance.Cook();
                instance.Eat();
            }

            // Besserer Ansatz: Ein Interface mit Constraints fuer die Interfaces welche ich haben moechte
            void DoSomethingWithDefaultConstructor<T>(T instance) where T : IEatable, ICookable, new()
            {
                var newInstance = new T(); // New contraint erlaubt uns eine Instanz zu erstellen.

                instance.Cook();
                instance.Eat();
            }
        }
    }

    public class SomeFooClass : IVisible
    {
        public bool Visible { get; } = true;
    }
}
