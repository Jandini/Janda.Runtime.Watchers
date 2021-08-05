# Janda.Runtime.Watchers

[![.NET](https://github.com/Jandini/Janda.Runtime.Watchers/actions/workflows/dotnet.yml/badge.svg)](https://github.com/Jandini/Janda.Runtime.Watchers/actions/workflows/dotnet.yml)

Provides `[WatchMethod]` which allows to watch and record method execution and the parameters. 



Example:

```c#
[WatchMethod]
public int Run()
{
    _logger.LogInformation("Running {0} {1} {2}", Application.Name, Application.Version, _settings.Description);

    _methodWatcher.Verbose();

    Parse(new byte[512], "matt", 4);
    Parse(new byte[512], "matt", 4);
    Parse(new byte[51], "matt", 4);

    _logger.LogInformation("Running application");
    _methodWatcher.Report();

    return 128;
}


[WatchMethod]
public static int Parse(byte[] buffer, string param1, int param2)
{
    var abc = "abc";
    var logger = Application.GetLogger<ApplicationService>();

    logger.LogInformation("abc GetHashCode: {HashCode}", abc.GetHashCode());

    abc = "a";
    logger.LogInformation("abc GetHashCode: {HashCode}", abc.GetHashCode());
    abc = "abc";
    logger.LogInformation("abc GetHashCode: {HashCode}", abc.GetHashCode());

    return param1?.Length ?? -10 + param2;
}

```



Results:


```shell
Running Janda.Runtime.Watchers.Console 1.1.0 Console application
abc GetHashCode: 1283000916
abc GetHashCode: -420650113
abc GetHashCode: 1283000916
abc GetHashCode: 1283000916
abc GetHashCode: -420650113
abc GetHashCode: 1283000916
abc GetHashCode: 1283000916
abc GetHashCode: -420650113
abc GetHashCode: 1283000916
Running application
Method count: 2
Method names: ["Int32 Run()", "Int32 Parse(Byte[], System.String, Int32)"]
{"Name": "Run", "Calls": 0, "Values": 0} []
{"Name": "Parse", "Calls": 2, "Values": 4} [{"Name": "buffer", "Count": 2}, {"Name": "param1", "Count": 1}, {"Name": "param2", "Count": 1}]
```

