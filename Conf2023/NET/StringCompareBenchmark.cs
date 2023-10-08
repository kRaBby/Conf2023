using BenchmarkDotNet.Attributes;

namespace Conf2023.NET;

[MemoryDiagnoser(false)]
public class StringCompareBenchmark
{
    private string str1 = "String with various symbols. გამარჯობა";
    private string str2 = "String with various symbols. გამარჯობა";
    private string str3 = "String with other lenght. გამარჯობა";
    private string str4 = "String with same lenght.     გამარჯობა";

    private string str1Ascii = "String with various symbols.0123456789";
    private string str2Ascii = "String with various symbols.0123456789";
    private string str3Ascii = "String with other lenght.0123456789";
    private string str4Ascii = "String with same lenght.    0123456789";

    [Benchmark()]
    public void Operator()
    {
        var result = str1 == str2;
        result = str1 == str3;
        result = str1 == str4;
    }

    [Benchmark()]
    public void StringEquals()
    {
        var result = str1.Equals(str2);
        result = str1.Equals(str3);
        result = str1.Equals(str4);
    }

    [Benchmark()]
    public void StringEqualsToLower()
    {
        var result = str1.ToLower().Equals(str2.ToLower());
        result = str1.ToLower().Equals(str3.ToLower());
        result = str1.ToLower().Equals(str4.ToLower());
    }

    [Benchmark()]
    public void StringEqualsOrdinalIgnoreCase()
    {
        var result = str1.Equals(str2, StringComparison.OrdinalIgnoreCase);
        result = str1.Equals(str3, StringComparison.OrdinalIgnoreCase);
        result = str1.Equals(str4, StringComparison.OrdinalIgnoreCase);
    }

    [Benchmark()]
    public void StringEqualsCurrentCultureIgnoreCase()
    {
        var result = str1.Equals(str2, StringComparison.CurrentCultureIgnoreCase);
        result = str1.Equals(str3, StringComparison.CurrentCultureIgnoreCase);
        result = str1.Equals(str4, StringComparison.CurrentCultureIgnoreCase);
    }

    [Benchmark()]
    public void StringEqualsInvariantCultureIgnoreCase()
    {
        var result = str1.Equals(str2, StringComparison.InvariantCultureIgnoreCase);
        result = str1.Equals(str3, StringComparison.InvariantCultureIgnoreCase);
        result = str1.Equals(str4, StringComparison.InvariantCultureIgnoreCase);
    }

    [Benchmark()]
    public void AsciiStringEqualsOrdinalIgnoreCase()
    {
        var result = str1Ascii.Equals(str2Ascii, StringComparison.OrdinalIgnoreCase);
        result = str1Ascii.Equals(str3Ascii, StringComparison.OrdinalIgnoreCase);
        result = str1Ascii.Equals(str4Ascii, StringComparison.OrdinalIgnoreCase);
    }

    [Benchmark()]
    public void AsciiStringEqualsCurrentCultureIgnoreCase()
    {
        var result = str1Ascii.Equals(str2Ascii, StringComparison.CurrentCultureIgnoreCase);
        result = str1Ascii.Equals(str3Ascii, StringComparison.CurrentCultureIgnoreCase);
        result = str1Ascii.Equals(str4Ascii, StringComparison.CurrentCultureIgnoreCase);
    }

    [Benchmark()]
    public void AsciiStringEqualsInvariantCultureIgnoreCase()
    {
        var result = str1Ascii.Equals(str2Ascii, StringComparison.InvariantCultureIgnoreCase);
        result = str1Ascii.Equals(str3Ascii, StringComparison.InvariantCultureIgnoreCase);
        result = str1Ascii.Equals(str4Ascii, StringComparison.InvariantCultureIgnoreCase);
    }
}

    /*
    | Method                                      | Mean       | Error     | StdDev    | Allocated |
    |-------------------------------------------- |-----------:|----------:|----------:|----------:|
    | Operator                                    |   2.714 ns | 0.0362 ns | 0.0321 ns |         - |
    | StringEquals                                |   2.725 ns | 0.0176 ns | 0.0147 ns |         - |
    | StringEqualsToLower                         | 499.675 ns | 9.7259 ns | 9.0976 ns |     616 B |
    | StringEqualsOrdinalIgnoreCase               |   9.703 ns | 0.0652 ns | 0.0609 ns |         - |
    | StringEqualsCurrentCultureIgnoreCase        | 105.235 ns | 2.0437 ns | 2.1867 ns |         - |
    | StringEqualsInvariantCultureIgnoreCase      |  88.220 ns | 1.7812 ns | 1.6662 ns |         - |
    | AsciiStringEqualsOrdinalIgnoreCase          |   9.748 ns | 0.0352 ns | 0.0312 ns |         - |
    | AsciiStringEqualsCurrentCultureIgnoreCase   |  97.621 ns | 1.5431 ns | 1.4434 ns |         - |
    | AsciiStringEqualsInvariantCultureIgnoreCase |  87.285 ns | 1.1993 ns | 1.1218 ns |         - |
     */

    /* .Net 8.0
    | Method                                      | Mean       | Error     | StdDev     | Allocated |
    |-------------------------------------------- |-----------:|----------:|-----------:|----------:|
    | Operator                                    |   3.158 ns | 0.0730 ns |  0.0683 ns |         - |
    | StringEquals                                |   2.681 ns | 0.0495 ns |  0.0386 ns |         - |
    | StringEqualsToLower                         | 480.435 ns | 9.5661 ns | 13.4103 ns |     616 B |
    | StringEqualsOrdinalIgnoreCase               |   9.221 ns | 0.1157 ns |  0.1026 ns |         - |
    | StringEqualsCurrentCultureIgnoreCase        |  92.023 ns | 1.8307 ns |  1.9588 ns |         - |
    | StringEqualsInvariantCultureIgnoreCase      |  86.408 ns | 1.0314 ns |  0.9143 ns |         - |
    | AsciiStringEqualsOrdinalIgnoreCase          |   9.464 ns | 0.2123 ns |  0.2607 ns |         - |
    | AsciiStringEqualsCurrentCultureIgnoreCase   |  91.097 ns | 1.4329 ns |  1.3403 ns |         - |
    | AsciiStringEqualsInvariantCultureIgnoreCase |  87.027 ns | 1.4833 ns |  1.3874 ns |         - |
     */