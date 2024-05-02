// See https://aka.ms/new-console-template for more information
using System.Device.Gpio;
using Iot.Device.Hx711;

internal class Program
{
    const int ClockPin1 = 6;
    const int DataPin1 = 5;
    const int ClockPin2 = 3;
    const int DataPin2 = 2;
    const int ClockPin3 = 24;
    const int DataPin3 = 23;

    public static List<Hx711> Balances {get; } = new List<Hx711>();
    private static async Task Main(string[] args)
    {
        Console.WriteLine("Hello World!");
        var restServer = new RestServer();
        using var controller = new GpioController();
        Balances.Add(CreateHx711(DataPin1, ClockPin1, controller));
        Balances.Add(CreateHx711(DataPin2, ClockPin2, controller));
        //Balances.Add(CreateHx711(DataPin3, ClockPin3, controller));
        Console.WriteLine("Starting REST Server");
        await restServer.Start();
        while (false)
        {
            for(int i = 0; i<Balances.Count; i++){
            Console.WriteLine($"Balance: {i} Weight in grams: {Balances[i].GetWeight().Grams}");
            }
            await Task.Delay(500);
        }
        await restServer.StopAsync();
        foreach(Hx711 balance in Balances){
            balance.PowerDown();
    }
    }

    private static Hx711 CreateHx711(int dataPin, int clockPin, GpioController controller)
    {
        var hx711 = new Hx711(dataPin, clockPin, gpioController: controller);
        Console.WriteLine($"Powering up Hx711 DataPin:{dataPin} ClockPin:{clockPin}");
        hx711.PowerUp();
        hx711.SetCalibration(UnitsNet.Mass.FromGrams(100));
        return hx711;
    }

}