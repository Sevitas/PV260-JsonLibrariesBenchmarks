Your task is to write benchmarks for JSON serializers/deserializers.

Steps:
- [x] Try to run test and get familiar with benchmark framework.
- [x] Implement serialization benchmark for Newtonsoft.Json.
- [x] Prepare another test JSON data and use them in benchmarks.
- [x] Find out, how to do some performance optimalizations for Newtonsoft.Json and try it.
- [x] Implement serialization/deseralization benchmarks for another library and compare it with Newtonsoft.Json.
- [ ] Configure benchmarks to run on different platforms (.net framework, .net core).
- [ ] Refactor benchmarks for stream deserialization (not whole string JSON will be in memory, but will be read as stream and deserialized in stream fashion).
- [x] Pick another two JSON serializers/deserializers.
- [ ] Write powershell and FAKE or CAKE build script, copy HTML reports from build folder to artifact folder.
