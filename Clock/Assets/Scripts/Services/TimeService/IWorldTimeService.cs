using System;

namespace MSuhinin.Clock
{
    public interface IWorldTimeService
    {
        void Initialize(string API_URL);
        DateTime UpdateTimeFromWeb();
    }
}