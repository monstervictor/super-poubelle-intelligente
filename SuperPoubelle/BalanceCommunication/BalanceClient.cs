using Refit;
using System.IO;
using System.Net;

namespace SuperPoubelle.BalanceCommunication
{
    public class BalanceClient
    {
        private const string FileName = "raspberryPiIP.txt";
        private BalanceClient() { }

        private static IBalanceRestClient _instance;
        public static IBalanceRestClient Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                var path = AppDomain.CurrentDomain.BaseDirectory;
                var file = Path.Combine(path, FileName);
                if (!File.Exists(file))
                    throw new FileNotFoundException($"Please create the file {file} with an IP Address");
                var ip = File.ReadAllText(file);
                if (!IPAddress.TryParse(ip, out _))
                    throw new InvalidDataException($"Please set a valid IPAddress in {FileName}");
                _instance = RestService.For<IBalanceRestClient>($"http://{ip}:5003");
                return _instance;
            }
        }
    }
}
