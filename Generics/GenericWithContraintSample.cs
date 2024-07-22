namespace Sprachfeatures
{
    public class MyClassWithASuperInterace : ISuperInterace
    {
        public void Cook()
        {
            Console.WriteLine("Ich koche");
        }

        public void Eat()
        {
            Console.WriteLine("Ich esse");
        }
    }

    public class MyClassForTheGenericContraintSample : IEatable, ICookable
    {
        public void Cook()
        {
            Console.WriteLine("Ich koche");
        }

        public void Eat()
        {
            Console.WriteLine("Ich esse");
        }
    }

    // Alter Ansatz: Composition von verschiedenen Interfaces was bei vielen Auspraegungen
    // unuebersichtlich werden kann.
    public interface ISuperInterace : IEatable, ICookable
    {

    }

    public interface IEatable
    {
        void Eat();
    }

    public interface ICookable
    {
        void Cook();
    }
}
