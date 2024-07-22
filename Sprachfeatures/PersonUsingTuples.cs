namespace Sprachfeatures
{
    public class PersonUsingTuples
    {
        public string Vorname { get; set; }

        public string ZweiterName { get; set; }

        public string Nachname { get; set; }

        public (string, string, string) VollerName() 
        {
            var zweiterName = !string.IsNullOrEmpty(ZweiterName) ? $" {ZweiterName}" : "";
            return (Vorname, zweiterName, Nachname);
        }

        public (string Vorname, string ZweiterName, string Nachname) VollerNameWithNamedTuples()
        {
            var zweiterName = !string.IsNullOrEmpty(ZweiterName) ? $" {ZweiterName}" : "";
            return (Vorname, zweiterName, Nachname);
        }

        public Tuple<string, string, string> VollerNameMitPrehistoricTuples()
        {
            var zweiterName = !string.IsNullOrEmpty(ZweiterName) ? $" {ZweiterName}" : "";
            return Tuple.Create(Vorname, zweiterName, Nachname);
        }
    }
}
