namespace Generics
{
    public class LazySampleClass
    {
        // Vermeiden weil die Klasse Ressourcenhungrig ist.
        private HeavyDutyClass _heavyDutyClassClassic = null;// = new HeavyDutyClass();
        private HeavyDutyClass _heavyDutyClassShort = null;// = new HeavyDutyClass();

        // Variante 1
        public HeavyDutyClass InstanceWithClassicalApproach 
        { 
            get
            {
                // Lazy Loading
                if (_heavyDutyClassClassic == null)
                {
                    _heavyDutyClassClassic = new HeavyDutyClass();
                }
                return _heavyDutyClassClassic;
            }
        }

        // Variante 2 mit Lazy
        private readonly Lazy<HeavyDutyClass> _heavyDutyClassLazy = new Lazy<HeavyDutyClass>(() => new HeavyDutyClass());

        // Variante 3 Short Syntax
        public HeavyDutyClass InstanceShortSyntax => (_heavyDutyClassShort ??= new HeavyDutyClass());

        public void Start()
        {
            // Eine Instanz wird erst erstellt, wenn die Klasse benoetigt wird
            InstanceWithClassicalApproach.Info();

            // Besserer Ansatz mit Lazy Klasse
            Console.WriteLine("Wurde Instanz der Klasse bereits erzeugt? " + _heavyDutyClassLazy.IsValueCreated);
            _heavyDutyClassLazy.Value.Info();
        }
    }

    public class HeavyDutyClass
    {
        public void Info()
        {
            Console.WriteLine("I am heavy duty and require a lot of resources...");
        }
    }
}
