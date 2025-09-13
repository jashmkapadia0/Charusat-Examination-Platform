using System;
using System.Windows.Threading;

public static class ExamTimerManager
{
    public static DispatcherTimer Timer;
    public static TimeSpan TimeRemaining;
    public static event Action OnTimeUp;
    public static event Action OnTick;

    public static void Start(int totalMinutes)
    {
        TimeRemaining = TimeSpan.FromMinutes(totalMinutes);
        Timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        Timer.Tick += (s, e) =>
        {
            TimeRemaining = TimeRemaining.Subtract(TimeSpan.FromSeconds(1));
            OnTick?.Invoke();

            if (TimeRemaining <= TimeSpan.Zero)
            {
                Timer.Stop();
                OnTimeUp?.Invoke();
            }
        };
        Timer.Start();
    }

    public static void Stop()
    {
        Timer?.Stop();
    }
}

