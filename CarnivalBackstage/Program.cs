namespace CarnivalBackstage;

internal static class Program
{
    const int port = 6780;
    const int lobbyPort = 6781;
    const string idServerKey = ""; // breaks, empty works fine

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.WebHost.UseUrls("http://127.0.0.1:6776");
        builder.Logging.ClearProviders();
        builder.Services.AddControllers();

        var app = builder.Build();
        app.MapGet("/", () => "Hello World!");
        app.MapControllers();

        //0: version (2.1.2)
        //1: ipAndPorts (000.0.0.0:1234,000.0.0.0:1234,000.0.0.0:1234)
        //2: maintance
        //3: subCnt (count:int,- ???
        //4: N/A
        //5: COMA_Sys.Instance.tax
        //6: COMA_ServerManager.Instance.serverAddr_IAP
        //7: COMA_ServerManager.Instance.deliverSvrAddr
        //8: COMA_Sys.Instance.tipContent
        //9: COMA_Sys.Instance.bCanGame
        //10: COMA_Sys.Instance.bLockMarket
        //11: COMA_Sys.Instance.marketRefreshInterval
        //12: COMA_Sys.Instance.marketRefreshCrystal
        //13: COMA_ServerManager.Instance.dataCollectSvrAddr
        //14: COMA_ServerManager.Instance.idSvrAddr
        //15: COMA_ServerManager.Instance.idSvrKey
        //16: COMA_CommonOperation.Instance.bfriendRequireInverval
        //17: COMA_Fishing.Instance.nTime0
        //18: COMA_Fishing.Instance.nTime1
        //19: COMA_Fishing.Instance.nTime2
        //20: COMA_Package.unlockPrice
        //21: COMA_VideoController.Instance.nVideo (int)
        //22: ip_game
        //23: port_game (0000,0001,0003)
        //24: COMA_Sys.Instance.bRateActive
        app.MapGet("/game/config.txt", () => $"2.1.2|127.0.0.1:{lobbyPort}|1|0| |0|127.0.0.1:6777|127.0.0.1:6778" +
        $"|TestContent|0|1|500|0|http://127.0.0.1:{port}/dataCollect|http://127.0.0.1:{port}/idServer|{idServerKey}|1|1|2|3|0|0|195.7.7.200|6752|25");

        _ = new Lobby.Server(lobbyPort).Run();

        app.Run();
    }
}
