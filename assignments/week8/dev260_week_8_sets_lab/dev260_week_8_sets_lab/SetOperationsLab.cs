using System;
using System.Collections.Generic;
using System.Linq;

namespace dev260_week_8_sets_lab
{
    public class SetOperationsLab
    {
        private HashSet<string> uniqueEmails = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private Dictionary<string, HashSet<string>> userPermissions = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);
        private HashSet<string> enrolledNow = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private HashSet<string> enrolledLastQuarter = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private DateTime startTime = DateTime.Now;
        private int totalOperations = 0;

        // Initialization methods
        public void InitializeUniqueEmails(List<string> emails) => uniqueEmails = new HashSet<string>(emails, StringComparer.OrdinalIgnoreCase);
        public void InitializeUserPermissions(Dictionary<string, HashSet<string>> permissions) => userPermissions = permissions;
        public void InitializeEnrollmentData(HashSet<string> now, HashSet<string> last) { enrolledNow = now; enrolledLastQuarter = last; }

        // System stats
        public SystemStats GetSystemStats() => new SystemStats
        {
            TotalOperations = totalOperations,
            Uptime = DateTime.Now - startTime,
            UniqueEmailsCount = uniqueEmails.Count,
            UsersWithPermissionsCount = userPermissions.Count,
            ThisQuarterEnrollment = enrolledNow.Count,
            LastQuarterEnrollment = enrolledLastQuarter.Count
        };

        // TODO #1: DeduplicateEmails
        public int DeduplicateEmails(List<string> emails)
        {
            int originalCount = emails.Count;
            var deduped = new HashSet<string>(emails, StringComparer.OrdinalIgnoreCase);
            emails.Clear();
            emails.AddRange(deduped);
            int removed = originalCount - emails.Count;
            totalOperations++;
            return removed;
        }

        // TODO #2: HasPermission
        public bool HasPermission(string userId, string permission)
        {
            totalOperations++;
            return userPermissions.ContainsKey(userId) && userPermissions[userId].Contains(permission);
        }

        // TODO #3: AddPermissions
        public int AddPermissions(string userId, HashSet<string> newPermissions)
        {
            if (!userPermissions.ContainsKey(userId))
                userPermissions[userId] = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            int before = userPermissions[userId].Count;
            userPermissions[userId].UnionWith(newPermissions);
            int added = userPermissions[userId].Count - before;
            totalOperations++;
            return added;
        }

        // TODO #4: GetMissingPermissions
        public HashSet<string> GetMissingPermissions(string userId, HashSet<string> requiredPermissions)
        {
            var missing = new HashSet<string>(requiredPermissions, StringComparer.OrdinalIgnoreCase);
            if (userPermissions.ContainsKey(userId))
                missing.ExceptWith(userPermissions[userId]);
            totalOperations++;
            return missing;
        }

        // TODO #5: FindNewStudents
        public HashSet<string> FindNewStudents()
        {
            var newStudents = new HashSet<string>(enrolledNow);
            newStudents.ExceptWith(enrolledLastQuarter);
            totalOperations++;
            return newStudents;
        }

        // TODO #6: FindDroppedStudents
        public HashSet<string> FindDroppedStudents()
        {
            var dropped = new HashSet<string>(enrolledLastQuarter);
            dropped.ExceptWith(enrolledNow);
            totalOperations++;
            return dropped;
        }

        // TODO #7: FindContinuingStudents
        public HashSet<string> FindContinuingStudents()
        {
            var continuing = new HashSet<string>(enrolledNow);
            continuing.IntersectWith(enrolledLastQuarter);
            totalOperations++;
            return continuing;
        }

        // TODO #8: CalculateRetentionRate
        public double CalculateRetentionRate()
        {
            if (enrolledLastQuarter.Count == 0) return 0.0;
            var continuing = FindContinuingStudents();
            totalOperations++;
            return (double)continuing.Count / enrolledLastQuarter.Count * 100;
        }
		
		public void RunPerformanceDemo()
		{
			var emails = new List<string>();
			var emailSet = new HashSet<string>();

			for (int i = 0; i < 1_000_000; i++)
			{
				emails.Add("user" + i + "@mail.com");
				emailSet.Add("user" + i + "@mail.com");
			}

			var target = "user999999@mail.com";

			var sw = System.Diagnostics.Stopwatch.StartNew();
			bool listContains = emails.Contains(target);
			sw.Stop();
Console.WriteLine($"List Contains: {sw.Elapsed.TotalMilliseconds:F3} ms ({sw.Elapsed.TotalMilliseconds * 1000:F0} µs)");
			sw.Restart();
			bool setContains = emailSet.Contains(target);
			sw.Stop();
			Console.WriteLine($"HashSet Contains: {sw.Elapsed.TotalMilliseconds:F3} ms ({sw.Elapsed.TotalMilliseconds * 1000:F0} µs)");
		}

        // Extra helper for UI
        public Dictionary<string, HashSet<string>> GetUserPermissions() => userPermissions;
        public (HashSet<string>, HashSet<string>) GetEnrollmentData() => (enrolledNow, enrolledLastQuarter);
    }
}