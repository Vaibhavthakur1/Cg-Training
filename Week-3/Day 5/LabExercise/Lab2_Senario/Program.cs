using System;
using System.Collections.Generic;

namespace Lab2Collections
{
    // 1. Text Editor Undo History
    public class TextEditorUndo
    {
        
        private readonly Stack<string> _undoHistory = new();

        public void RecordAction(string action)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(action);
            _undoHistory.Push(action);
        }

        public string? Undo()
        {
            return _undoHistory.TryPop(out var lastAction) ? lastAction : null;
        }
    }

    // 2. Customer Support Ticket Queue
    public class TicketQueue
    {
        private readonly Queue<string> _tickets = new();

        public void SubmitTicket(string ticketId)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(ticketId);
            _tickets.Enqueue(ticketId);
        }

        public string? ProcessNext()
        {
            return _tickets.TryDequeue(out var nextTicket) ? nextTicket : null;
        }
    }

    // 3. Unique Daily Active User (DAU) Tracker
    public class DailyActiveUserTracker
    {
       
        private readonly HashSet<int> _uniqueUserIds = new();

        public void RecordVisit(int userId)
        {
            _uniqueUserIds.Add(userId);
        }

        public int UniqueVisitorCount() => _uniqueUserIds.Count;
    }

    // 4. Music Playlist (Arbitrary Insertion/Removal)
    public class MusicPlaylist
    {
        
        private readonly LinkedList<string> _tracks = new();

        public void AddTrack(string song) => _tracks.AddLast(song);

        public bool InsertAfter(string afterSong, string newSong)
        {
            var targetNode = _tracks.Find(afterSong);
            if (targetNode is null) return false;

            _tracks.AddAfter(targetNode, newSong);
            return true;
        }

        public bool Remove(string song)
        {
            return _tracks.Remove(song);
        }

        public IEnumerable<string> GetTracks() => _tracks;
    }

    // Demonstration
    public class Program
    {
        public static void Main()
        {
            // 1. Stack Demo
            Console.WriteLine("--- 1. Undo Stack ---");
            var editor = new TextEditorUndo();
            editor.RecordAction("Typed 'Hello'");
            editor.RecordAction("Formatted Bold");
            Console.WriteLine($"Undo 1: {editor.Undo()}"); // Formatted Bold
            Console.WriteLine($"Undo 2: {editor.Undo()}"); // Typed 'Hello'
            Console.WriteLine($"Undo 3: {editor.Undo() ?? "None"}"); // None

            // 2. Queue Demo
            Console.WriteLine("\n--- 2. Ticket Queue ---");
            var queue = new TicketQueue();
            queue.SubmitTicket("TICKET-101");
            queue.SubmitTicket("TICKET-102");
            Console.WriteLine($"Processed: {queue.ProcessNext()}"); // TICKET-101
            Console.WriteLine($"Processed: {queue.ProcessNext()}"); // TICKET-102

            // 3. HashSet Demo
            Console.WriteLine("\n--- 3. DAU Tracker ---");
            var dau = new DailyActiveUserTracker();
            dau.RecordVisit(42);
            dau.RecordVisit(99);
            dau.RecordVisit(42); // Duplicate
            Console.WriteLine($"Unique DAU Count: {dau.UniqueVisitorCount()}"); // 2

            // 4. LinkedList Demo
            Console.WriteLine("\n--- 4. Music Playlist ---");
            var playlist = new MusicPlaylist();
            playlist.AddTrack("Song A");
            playlist.AddTrack("Song C");
            playlist.InsertAfter("Song A", "Song B"); // Insert B between A and C
            playlist.Remove("Song C");
            Console.WriteLine($"Playlist: {string.Join(" -> ", playlist.GetTracks())}"); // Song A -> Song B
        }
    }
}