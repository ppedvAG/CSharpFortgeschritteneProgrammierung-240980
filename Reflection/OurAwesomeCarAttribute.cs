namespace Reflection
{
    public class OurAwesomeCarAttribute : Attribute
    {
        public string YourMood { get; }

        public int LuckyNumber { get; }

        public OurAwesomeCarAttribute(string mood, int luckyNumber)
        {
            YourMood = mood;
            LuckyNumber = luckyNumber;
        }
    }
}
