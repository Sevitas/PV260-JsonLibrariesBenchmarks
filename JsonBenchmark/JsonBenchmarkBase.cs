using System;
using System.IO;
using System.Net.Http;

namespace JsonBenchmark
{
    public abstract class JsonBenchmarkBase
    {
        private const string TestFilesFolder = "TestFiles";
        private const string ChuckNorrisFile = "chucknorris.json";
        private const string FriendFile = "friend.json";
        protected string JsonSampleStringFriend;
        protected string JsonSampleString;

        protected JsonBenchmarkBase()
        {
            JsonSampleString = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, TestFilesFolder, ChuckNorrisFile));
            JsonSampleStringFriend = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, TestFilesFolder, FriendFile));
        }

        protected Stream CreateStream()
        {
            return File.OpenRead(TestFilesFolder + "/" + ChuckNorrisFile);
        }
    }
}
