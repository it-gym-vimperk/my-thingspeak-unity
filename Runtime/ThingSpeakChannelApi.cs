using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

namespace MyThingSpeak
{
    public class ThingSpeakChannelApi : MonoBehaviour
    {
        [SerializeField] private int channelIndex;
        [SerializeField] private string channelId;
        [SerializeField] private float requestInterval = 15;
        [SerializeField] private int resultsCount = 10;
        [SerializeField] private bool isVerbose;
        private string ThinkSpeakUrl() => $"https://api.thingspeak.com/channels/{Uri.EscapeDataString(channelId)}/feeds.json?results={resultsCount}";

        private void Awake()
        {
            StartCoroutine(KeepGettingData());
        }
 
        private IEnumerator KeepGettingData()
        {
            while (true)
            {
                yield return StartCoroutine(GetThingSpeakDataFromChannel(resultsCount));
                yield return new WaitForSeconds(requestInterval);
            }
        }
        
        private IEnumerator GetThingSpeakDataFromChannel(int resultsCount)
        {
            var url = ThinkSpeakUrl();
            Log($"Start fetching data from {url}");

            using var www = UnityWebRequest.Get(url);
            yield return www.SendWebRequest();
            
            if (www.result == UnityWebRequest.Result.ConnectionError ||
                www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                try
                {
                    Log($"Data fetched {www.downloadHandler.text}");
                    var tsData = JsonConvert.DeserializeObject<ThingSpeakChannelData>(www.downloadHandler.text);
                    if (tsData != null)
                    {
                        Log($"Data successfully updated");
                        GlobalData.Channels[channelIndex] = tsData;
                    }
                    else
                    {
                        Debug.LogError($"Error ThinkSpeak data is null, data remains untouched");
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Error during deserialization of ThingSpeak Response {ex.ToString()}");
                }
            }
        }

        private void Log(string log)
        {
            if (!isVerbose)
                return;
            
            Debug.Log($"ThingSpeakChannelApi: {log}");
        }
    }
}


