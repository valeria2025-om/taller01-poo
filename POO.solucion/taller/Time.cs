using System;
using System.Collections.Generic;

namespace taller
{
        public class Time
        {
            private int hours;
            private int minutes;
            private int seconds;
            private int milliseconds;

            // --- Constructores ---
            public Time() : this(0, 0, 0, 0) { }
         
            public Time(int h) : this(h, 0, 0, 0) { }

            public Time(int h, int m) : this(h, m, 0, 0) { }

            public Time(int h, int m, int s) : this(h, m, s, 0) { }

            public Time(int h, int m, int s, int ms)
            {
                if (h < 0 || h > 23)
                    throw new ArgumentOutOfRangeException(nameof(h), $"The hour: {h}, is not valid.");
                if (m < 0 || m > 59)
                    throw new ArgumentOutOfRangeException(nameof(m), $"The minutes: {m}, is not valid.");
                if (s < 0 || s > 59)
                    throw new ArgumentOutOfRangeException(nameof(s), $"The seconds: {s}, is not valid.");
                if (ms < 0 || ms > 999)
                    throw new ArgumentOutOfRangeException(nameof(ms), $"The milliseconds: {ms}, is not valid.");

                hours = h;
                minutes = m;
                seconds = s;
                milliseconds = ms;
            }

            // --- Métodos ---
            public override string ToString()
            {
                DateTime dt = new DateTime(1, 1, 1, hours, minutes, seconds, milliseconds);
                return dt.ToString("hh:mm:ss.fff tt");
            }

            public long ToMilliseconds()
            {
                return (long)hours * 3600000 + (long)minutes * 60000 + (long)seconds * 1000 + milliseconds;
            }

            public long ToSeconds()
            {
                return (long)hours * 3600 + (long)minutes * 60 + seconds;
            }

            public long ToMinutes()
            {
                return (long)hours * 60 + minutes;
            }

            public bool IsOtherDay(Time other)
            {
                long totalMs = this.ToMilliseconds() + other.ToMilliseconds();
                long oneDayMs = 24L * 60 * 60 * 1000;
                return totalMs >= oneDayMs;
            }

            public Time Add(Time other)
            {
                long totalMs = this.ToMilliseconds() + other.ToMilliseconds();
                long oneDayMs = 24L * 60 * 60 * 1000;
                totalMs %= oneDayMs; // Si pasa al otro día, reinicia

                int h = (int)(totalMs / 3600000); totalMs %= 3600000;
                int m = (int)(totalMs / 60000); totalMs %= 60000;
                int s = (int)(totalMs / 1000); totalMs %= 1000;
                int ms = (int)totalMs;

                return new Time(h, m, s, ms);
            }
        }

    }

