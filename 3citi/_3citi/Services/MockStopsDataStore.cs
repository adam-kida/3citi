﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using _3citi.Models;

using Xamarin.Forms;

using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using PCLStorage;

[assembly: Dependency(typeof(_3citi.Services.MockStopsDataStore))]
namespace _3citi.Services
{
    public class MockStopsDataStore : IStopDataStore<Stop>
    {
        bool isInitialized;
        List<Stop> stops;

        public async Task<IEnumerable<Stop>> GetItemsAsync(bool forceRefresh = false)
        {
            await InitializeAsync();

            return await Task.FromResult(stops);
        }

        public async Task InitializeAsync()
        {
            if (isInitialized)
                return;

            stops = new List<Stop>();
            var client = new HttpClient();
            var jsonStopsUrl = "http://91.244.248.19/dataset/c24aa637-3619-4dc2-a171-a23eec8f2172/resource/cd4c08b5-460e-40db-b920-ab9fc93c1a92/download/stops.json";
            var jsonStops = await client.GetStringAsync(jsonStopsUrl);
            Dictionary<string, StopTop> jStopsObject = JsonConvert.DeserializeObject<Dictionary<string, StopTop>>(jsonStops);
            StopTop DayStops = new StopTop();

            jStopsObject.TryGetValue(DateTime.Now.AddDays(0).Date.ToString("yyyy-MM-dd"), out DayStops);

            foreach (Stop item in DayStops.stops)
            {
                stops.Add(item);
            }

            isInitialized = true;
        }        
    }
}