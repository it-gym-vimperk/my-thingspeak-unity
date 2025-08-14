using UnityEngine;

namespace MyThingSpeak.Unity
{
    public static class ThinkSpeedDataExtension
    {
        public static int GetValuesCount(this ThingSpeakChannelData tsChannelData)
        {
            return tsChannelData.feeds.Length;
        }
        
        public static float GetLastValue(this ThingSpeakChannelData tsChannelData, int fieldIndex)
        {
            return GetValue(tsChannelData, fieldIndex, 1);
        }
        public static float GetValue(this ThingSpeakChannelData tsChannelData, int fieldIndex, int order)
        {
            var field = typeof(ThingSpeakChannel).GetField($"field{fieldIndex}");
            
            if (field == null)
            {
                 Debug.LogError($"No such field with index {fieldIndex} exists, use numbers <1-8>");
                 return 0;
            }
            if (field.GetValue(tsChannelData.channel) == null)
            {
                Debug.LogError($"Field with index {fieldIndex} is not enabled");
                return 0;
            }

            int index = tsChannelData.feeds.Length - order;
            if (index < 0 || index >= tsChannelData.feeds.Length)
            {
                Debug.LogError($"Field with index {fieldIndex} is out or range {1} - {tsChannelData.feeds.Length}");
                return 0;
            }
            field = typeof(ThingSpeakFeed).GetField($"field{fieldIndex}");
            float? floatValue = (float?)field.GetValue(tsChannelData.feeds[index]);
            return floatValue ?? 0;
        }
    }
}