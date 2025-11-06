
using System;

namespace QueueLab
{
    public class SupportTicket
    {
        public string TicketId { get; set; } = "";
        public string Description { get; set; } = "";
        public string Priority { get; set; } = "Normal";
        public DateTime SubmittedAt { get; set; }
        public string SubmittedBy { get; set; } = "User";

        public SupportTicket(string ticketId, string description, string priority = "Normal", string submittedBy = "User")
        {
            TicketId = ticketId;
            Description = description;
            Priority = priority;
            SubmittedBy = submittedBy;
            SubmittedAt = DateTime.Now;
        }

        public TimeSpan GetWaitTime()
        {
            return DateTime.Now - SubmittedAt;
        }

        public string GetFormattedWaitTime()
        {
            var waitTime = GetWaitTime();
            if (waitTime.TotalDays >= 1)
                return $"{waitTime.Days}d {waitTime.Hours}h {waitTime.Minutes}m";
            else if (waitTime.TotalHours >= 1)
                return $"{waitTime.Hours}h {waitTime.Minutes}m";
            else if (waitTime.TotalMinutes >= 1)
                return $"{waitTime.Minutes}m {waitTime.Seconds}s";
            else
                return $"{waitTime.Seconds}s";
        }

        public string GetPriorityEmoji()
        {
            return Priority.ToLower() switch
            {
                "urgent" => "🔴",
                "high" => "🟡",
                "normal" => "🟢",
                _ => "⚪"
            };
        }

        public override string ToString()
        {
            return $"{GetPriorityEmoji()} {TicketId} - {Description} (Wait: {GetFormattedWaitTime()})";
        }

        public string ToDetailedString()
        {
            return $"""
                   Ticket ID: {TicketId}
                   Description: {Description}
                   Priority: {GetPriorityEmoji()} {Priority}
                   Submitted By: {SubmittedBy}
                   Submitted At: {SubmittedAt:yyyy-MM-dd HH:mm:ss}
                   Wait Time: {GetFormattedWaitTime()}
                   """;
        }
    }
}
