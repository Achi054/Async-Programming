namespace AsyncProgramming
{
    internal class BadAndGoodPractice
    {
        #region Best Practices
        async Task<string> BestPractice()
        {
            return await ReadFromGoogle();
        }

        ValueTask<int[]> GetIds() => ValueTask.FromResult(new int[4] { 1, 3, 5, 7 });

        #region Bad Practices
        async Task<string> ReadFromGoogle()
        {
            using HttpClient httpClient = new();

            try
            {
                // Bad
                return httpClient.GetStringAsync("http://google.com").Wait();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        async Task<IEnumerable<string>> GetData()
        {
            List<string> data = new();

            // Bad
            var dataIds = await GetDataIds();

            foreach (var id in dataIds)
            {
                // Bad
                var info = await GetDataInfo(id);
                data.Add(info);
            }

            return data;
        }

        async Task<int[]> GetDataIds()
        {
            return await GetIds();
        }

        Task<string> GetGoogleData()
        {
            // Bad
            try
            {
                return new HttpClient().GetStringAsync("http://google.com");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Bad
        async Task<string> GetGoogleDataRecurring()
        {
            string data = string.Empty;

            if (data is not null)
                return data;

            try
            {
                data = await new HttpClient().GetStringAsync("http://google.com");

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        class AppInitializer
        {
            public AppInitializer()
            {
                // Bad
                InitializeData();
            }

            async Task<string> InitializeData()
            {
                using HttpClient httpClient = new HttpClient();

                return await httpClient.GetStringAsync("http://google.com");
            }
        }
        #endregion

        #region Good Practices
        async Task<string> ReadFromGoogle()
        {
            using HttpClient httpClient = new();

            try
            {
                // Good
                return await httpClient.GetStringAsync("http://google.com");

                // For synchrony
                //return httpClient.GetStringAsync("http://google.com").GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        async Task<IEnumerable<string>> GetData()
        {
            List<string> data = new();

            // Good
            var dataIds = await GetDataIds().ConfigureAwait(false);

            foreach (var id in dataIds)
            {
                var info = await GetDataInfo(id).ConfigureAwait(false);
                data.Add(info);
            }

            return data;
        }

        Task<int[]> GetDataIds()
        {
            return GetIds();
        }

        async Task<string> GetGoogleData()
        {
            try
            {
                return await new HttpClient().GetStringAsync("http://google.com");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        async ValueTask<string> GetGoogleDataRecurring()
        {
            string data = string.Empty;

            // Good
            if (data is not null)
                return data;

            try
            {
                data = await new HttpClient().GetStringAsync("http://google.com");

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        class AppInitializer
        {
            public AppInitializer()
            {
                // Good
                InitializeData().SafeFireAndForget();
            }

            async Task<string> InitializeData()
            {
                using HttpClient httpClient = new HttpClient();

                return await httpClient.GetStringAsync("http://google.com");
            }
        }

        static class AsyncSafehelper
        {
            static async void SafeFireAndForget(this Task task, bool continueThread = true, Action<Exception> OnException)
            {
                try
                {
                    await task.ConfigureAwait(continueThread);
                }
                catch (Exception ex) when (OnException is not null)
                {
                    OnException(ex);
                }
            }
        }
        #endregion
        #endregion
    }
}
