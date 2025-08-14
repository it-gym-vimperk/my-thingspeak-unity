using System;
using System.Collections.Generic;

namespace MyThingSpeak
{
    public class ThingSpeakChannel
    {
        public int id;
        public string name;
        public float latitude;
        public float longitude;
        public string field1;
        public string field2;
        public string field3;
        public string field4;
        public string field5;
        public string field6;
        public string field7;
        public string field8;
        public DateTime created_at;
        public DateTime updated_at;
    }

    public class ThingSpeakFeed
    {
        public DateTime created_at;
        public int entry_id;
        public float? field1;
        public float? field2;
        public float? field3;
        public float? field4;
        public float? field5;
        public float? field6;
        public float? field7;
        public float? field8;
    }

    public class ThingSpeakChannelData
    {
        public ThingSpeakChannel channel;
        public ThingSpeakFeed[] feeds;
    }

    public static class GlobalData
    {
        public static bool Exists(int channelIndex) => Channels.ContainsKey(channelIndex);

        public static ThingSpeakChannelData GetChannelData(int channelIndex) =>
            Exists(channelIndex) ? Channels[channelIndex] : null;

        public static Dictionary<int, ThingSpeakChannelData> Channels = new Dictionary<int, ThingSpeakChannelData>();
    }
}