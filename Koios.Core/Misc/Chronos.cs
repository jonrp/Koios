using System;
using System.Diagnostics;

namespace Koios.Core.Misc
{
    public class Chronos
    {
        private struct Snapshot
        {
            public readonly DateTime dtObserved;
            public readonly DateTime dtBase;
            public readonly long swObserved;
            public readonly long swFrequency;

            public Snapshot(DateTime dtObserved, DateTime dtBase, long swObserved, long swFrequency)
            {
                this.dtObserved = dtObserved;
                this.dtBase = dtBase;
                this.swObserved = swObserved;
                this.swFrequency = swFrequency;
            }
        }
        
        private readonly long synchPeriodSwTicks;

        public Stopwatch stopwatch;
        private Snapshot snapshot;

        public Chronos()
        {
            synchPeriodSwTicks = 10 * Stopwatch.Frequency;

            DateTime dtObserved = DateTime.UtcNow;
            stopwatch = Stopwatch.StartNew();
            snapshot = new Snapshot(dtObserved, dtObserved, stopwatch.ElapsedTicks, Stopwatch.Frequency);
        }

        public DateTime UtcNow
        {
            get
            {
                DateTime dtObserved = DateTime.UtcNow;
                long swObserved = stopwatch.ElapsedTicks;
                long et = swObserved - snapshot.swObserved;

                if (et < 0 || dtObserved.Ticks - snapshot.dtObserved.Ticks > TimeSpan.TicksPerMinute)
                {
                    snapshot = new Snapshot(dtObserved, dtObserved, stopwatch.ElapsedTicks, Stopwatch.Frequency);
                    return dtObserved;
                }
                else if (et < synchPeriodSwTicks)
                {
                    return snapshot.dtBase.AddTicks(et * TimeSpan.TicksPerSecond / snapshot.swFrequency);
                }
                else
                {
                    DateTime dtBase = snapshot.dtBase.AddTicks(et * TimeSpan.TicksPerSecond / snapshot.swFrequency);
                    snapshot = new Snapshot(dtObserved, dtBase, swObserved,
                        et * TimeSpan.TicksPerSecond * 2 / (dtObserved.Ticks - snapshot.dtObserved.Ticks + dtObserved.Ticks + dtObserved.Ticks - dtBase.Ticks - snapshot.dtObserved.Ticks));
                    return dtBase;
                }
            }
        }

        public long UtcTicks => UtcNow.Ticks;

        public DateTime Now => UtcNow.ToLocalTime();
    }
}
