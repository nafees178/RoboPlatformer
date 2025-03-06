using UnityEngine;
using Discord;

public class DiscordController : MonoBehaviour
{
    public long clientID = 123456789012345678; // Replace with your Discord App Client ID
    private Discord.Discord discord;

    void Start()
    {
        discord = new Discord.Discord(1347311615011328010, (ulong)Discord.CreateFlags.Default);
        UpdatePresence();
    }

    void Update()
    {
        discord.RunCallbacks(); // Keep Discord connection alive
    }

    private void UpdatePresence()
    {
        var activityManager = discord.GetActivityManager();
        var activity = new Activity
        {
            State = "Fighting Empiest", // Custom status
            Details = "Collecting Batterys", // Game details
            Timestamps =
            {
                Start = System.DateTimeOffset.Now.ToUnixTimeSeconds()
            },
            Assets =
            {
                LargeImage = "game_logo", // Image name from Discord Developer Portal
                LargeText = "Robot Roaming"
            }
        };

        activityManager.UpdateActivity(activity, (res) =>
        {
            if (res == Discord.Result.Ok)
                Debug.Log("Discord Rich Presence updated successfully!");
            else
                Debug.LogError("Failed to update Discord Rich Presence.");
        });
    }

    void OnApplicationQuit()
    {
        discord.Dispose();
    }
}
