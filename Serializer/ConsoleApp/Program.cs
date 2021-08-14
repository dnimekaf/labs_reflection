using System;
using System.Diagnostics;
using ConsoleApp.Assets;
using Newtonsoft.Json;
using Serializer.Library.Service;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var serializer = new CsvSerializer();
            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i < 100000; i++)
            {
                var serialized = serializer.Serialize(new F().Get());
                var f = serializer.Deserialize<F>(serialized);
            }
            sw.Stop();
            var withoutConsole = sw.ElapsedMilliseconds;
            
            sw.Restart();
            for (var i = 0; i < 100000; i++)
            {
                var serialized = serializer.Serialize(new F().Get());
                var f = serializer.Deserialize<F>(serialized);
                Console.WriteLine($"F: {serialized}");
            }
            sw.Stop();
            Console.WriteLine($"My serializer elapsed milliseconds: {withoutConsole}");
            Console.WriteLine($"My serializer (with console) elapsed milliseconds: {sw.ElapsedMilliseconds}");
            Console.WriteLine($"Console print time in milliseconds: {sw.ElapsedMilliseconds - withoutConsole}");
            
            sw.Restart();
            for (var i = 0; i < 100000; i++)
            {
                var serialized = JsonConvert.SerializeObject(new F().Get());
                var f = JsonConvert.DeserializeObject<F>(serialized);
            }
            sw.Stop();
            Console.WriteLine($"JsonConvert serializer elapsed milliseconds: {sw.ElapsedMilliseconds}");
            Console.ReadKey();
        }
    }
}