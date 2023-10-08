using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Primitives;

namespace Conf2023.NET;

[MemoryDiagnoser(false)]
public class StringBenchmark
{
    string String = "27848d66-90d5-4373-9244-2d5204a1faf0";

    [Benchmark]
    public void SubString()
    {
        var subString = String.Substring(9, 4);

        for (var i = 0; i < subString.Length; ++i)
        {
            //Console.WriteLine(subString[i]);
        }
    }


    [Benchmark]
    public void StringSegment()
    {
        var stringSegment = new StringSegment(String, 9, 4);

        for (var i = 0; i < stringSegment.Length; ++i)
        {
            //Console.WriteLine(stringSegment[i]);
        }
    }
}
/* .Net 6.0
| Method        | Mean     | Error     | StdDev    | Allocated |
|-------------- |---------:|----------:|----------:|----------:|
| SubString     | 7.822 ns | 0.1759 ns | 0.1728 ns |      32 B |
| StringSegment | 1.038 ns | 0.0437 ns | 0.0409 ns |         - |

 */

/* .Net 8.0
| Method        | Mean      | Error     | StdDev    | Allocated |
|-------------- |----------:|----------:|----------:|----------:|
| SubString     | 4.0783 ns | 0.1036 ns | 0.1383 ns |      32 B |
| StringSegment | 0.8255 ns | 0.0242 ns | 0.0215 ns |         - |
 */