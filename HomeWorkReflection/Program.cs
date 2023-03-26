using HomeWorkReflection;
using System.Diagnostics;

using System.Text.Json;


internal class Program
{
    private static void Main(string[] args)
    {

        var targetObject = F.Get();
        var stopwatch = new Stopwatch();
        int countCyclies = 1000;

        //Время, затраченное на сериализацию моим сериализатором
        stopwatch.Start();
        for (int i = 0; i < countCyclies; i++)
        {
            Serializer.Serialize<F>(targetObject);

        }
        stopwatch.Stop();
        Console.WriteLine($"My serializer: {stopwatch.ElapsedMilliseconds}");
        stopwatch.Reset();
        //Время, затраченное на вывод в консоль
        stopwatch.Start();
        for (int i = 0; i < countCyclies; i++)
        {
            Serializer.Serialize<F>(targetObject);
            using (StreamReader reader = new StreamReader("Save.csv"))
            {
                Console.WriteLine($"My serializer: \n{reader.ReadToEnd()}");

            }

        }
        stopwatch.Stop();
        Console.WriteLine($"My serializer, output to the console {stopwatch.ElapsedMilliseconds} ");
        stopwatch.Reset();
        //Время, затарченное на десериализацию моим сериализатором
        stopwatch.Start();

        for (int i = 0; i < countCyclies; i++)
        {
            F newObject = Serializer.Deserialize<F>();

        }
        stopwatch.Stop();
        Console.WriteLine($"My deserializer: {stopwatch.ElapsedMilliseconds}");
        stopwatch.Reset();
        //Время, затраченное на сериализацию JSON-сериализатором
        stopwatch.Start();

        for (int i = 0; i < countCyclies; i++)
        {
            var json = JsonSerializer.Serialize(targetObject);
            File.WriteAllText("json.json", json);

        }
        stopwatch.Stop();
        Console.WriteLine($"json ser: {stopwatch.ElapsedMilliseconds}");
        stopwatch.Reset();
        //Время, затраченное на десериализацию JSON-сериализатором
        stopwatch.Start();

        for (int i = 0; i < countCyclies; i++)
        {
            var after = JsonSerializer.Deserialize<F>(File.ReadAllText("json.json"));

        }
        stopwatch.Stop();
        Console.WriteLine($"json deser: {stopwatch.ElapsedMilliseconds}");
    }

}
