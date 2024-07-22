namespace Generics
{
    public class DataStore<T> where T : class, IVisible, new()
    {
        public T[] Data { get; set; } = new T[0];

        public DataStore()
        {
            
        }

        public void Add(int index, T item)
        {
            Data[index] = item;
        }

        public T GetValue(int index)
        {
            if (Data[index].Visible)
            {
                return Data[index];
            }
            return new T();
        }

        public TSpecial AndNowForSomethingSpecial<TSpecial>(TSpecial item)
        {
            Console.WriteLine(default(TSpecial));
            return item;
        }
    }

    public class FooStore : DataStore<Foo>
    {
        public void DoSometingFooy(Foo item)
        {
            // do something
        }
    }

    public class Foo : IVisible
    {
        public bool Visible => true;
    }
}
