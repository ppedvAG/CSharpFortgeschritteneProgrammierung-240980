

namespace DelegatesEvents
{
    public class EventsDemo
    {
        public record SomeArgs(double Amount);

        public event EventHandler OnRainStarting;

        public event EventHandler<SomeArgs> OnRainEnding;

        public void StartSample()
        {
            // Wenn wir ein Event aufrufen uebergeben wir in der Regel den "sender"
            // welches der this Kontext ist und dann die EventArgs.
            OnRainStarting?.Invoke(this, EventArgs.Empty);

            const int twoSeconds = 2000;
            Thread.Sleep(twoSeconds);

            OnRainEnding?.Invoke(this, new SomeArgs(10));
        }

    }
}
