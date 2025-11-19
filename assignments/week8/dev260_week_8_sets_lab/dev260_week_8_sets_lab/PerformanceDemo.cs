using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace dev260_week_8_sets_lab
{
    class PerformanceDemo
    {
        public static void RunPerformanceDemo()
        {
            var emails = new List<string>();
            var emailSet = new HashSet<string>();

            for (int i = 0; i < 1_000_000; i++)
            {
                string email = "user" + i + "@mail.com";
                emails.Add(email);
                emailSet.Add(email);
            }

            var target = "user999999@mail.com";

            var sw = Stopwatch.StartNew();
            bool listContains = emails.Contains(target);
            sw.Stop();
            Console.WriteLine($"List Contains: {sw.Elapsed.TotalMilliseconds:F3} ms ({sw.Elapsed.TotalMilliseconds * 1000:F0} µs)");

            sw.Restart();
            bool setContains = emailSet.Contains(target);
            sw.Stop();
            Console.WriteLine($"HashSet Contains: {sw.Elapsed.TotalMilliseconds:F3} ms ({sw.Elapsed.TotalMilliseconds * 1000:F0} µs)");
        }
    }
}