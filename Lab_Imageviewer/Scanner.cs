using System.Collections.Concurrent;

namespace Lab_Images;

public class Scanner : Runnable
{
    public static readonly ConcurrentBag<string> ProcessedImages = new();
    public static readonly ConcurrentQueue<string> ImagePathQueue = new();

    public string ScanPath { get; private set; }

    public Scanner(string path)
    {
        ScanPath = path;
		CurrentTask = new Task(Run);
    }

	protected private override void Run()
	{
		while (Continue)
		{
			string[] pfade = Directory.GetFiles(ScanPath);
			foreach (string s in pfade)
			{
				if (ProcessedImages.Contains(s) || ImagePathQueue.Contains(s))
					continue;
				ImagePathQueue.Enqueue(s);
			}

			//Directory.GetFiles(ScanPath)
			//	.Where(e => !ProcessedImages.Contains(e) && !ImagePathQueue.Contains(e))
			//	.ToList()
			//	.ForEach(e => ImagePathQueue.Enqueue(e));

			Thread.Sleep(1000);
		}
	}
}