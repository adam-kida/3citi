﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace _3citi.Services
{
    public interface IRoutesDayDataStore<T>
    {
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);

        Task InitializeAsync();
    }
}
