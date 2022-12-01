﻿namespace ASLET.Models;

public class RoomModel
{
    // ID counter used to assign IDs automatically
    private static int _nextRoomId = 0;

    // Initializes room data and assign ID to room
    public RoomModel(string name, bool lab, int numberOfSeats, string uniqueId = "")
    {
        UniqueId = uniqueId;
        Id = _nextRoomId++;
        Name = name;
        Lab = lab;
        NumberOfSeats = numberOfSeats;
    }

    public string UniqueId { get; set; }
    
    // Returns room ID - automatically assigned
    public int Id { get; set; }

    // Returns name
    public string Name { get; set; }

    // Returns TRUE if room has computers otherwise it returns FALSE
    public bool Lab { get; set; }

    // Returns number of seats in room
    public int NumberOfSeats { get; set; }

    // Restarts ID assigments
    public static void RestartIDs() { _nextRoomId = 0; }
}