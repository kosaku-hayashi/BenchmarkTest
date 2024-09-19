
using BenchmarkDotNet.Running;

namespace BenchmarkApp;


class Program
{
    static void Main()
    {
        BenchmarkRunner.Run<ReflectionTest>();
        //BenchmarkRunner.Run<StringBuilderTest>();
    }
}

//private static readonly (string Input, string ExpectedOutput)[] testCases = new[]
//{
//    ("number123", "123"),
//    ("number012text345", "012345"),
//    ("16000", "16000"),
//    ("hello world", ""),
//    ("hello world 1 2 3", "123"),
//    ("-1200", "-1200"),
//    ("-3200hello", "-3200"),
//    ("-hello 1200", "-1200"),
//    ("ABC 1200.87 DEF", "1200.87"),
//    ("3.14.2", "3.14"),
//    ("", "")
//};

//int passCount = 0;
//int failCount = 0;

//        foreach(var (input, expected) in testCases)
//        {
//            var test = new StringBuilderTest(input);

//string defaultResult = test.Default();
//string roopAsSpanResult = test.RoopAsSpan();
//string roopAsSpanAndStructBuilderResult = test.RoopAsSpanAndStructBuilder();

//bool defaultPass = defaultResult == expected;
//bool roopAsSpanPass = roopAsSpanResult == expected;
//bool roopAsSpanAndStructBuilderPass = roopAsSpanAndStructBuilderResult == expected;

//bool allPass = defaultPass && roopAsSpanPass && roopAsSpanAndStructBuilderPass;

//            if(allPass)
//            {
//                passCount++;
//                Console.WriteLine($"[PASS] Input: \"{input}\" | Expected: \"{expected}\"");
//            }
//            else
//{
//    failCount++;
//    Console.WriteLine($"[FAIL] Input: \"{input}\" | Expected: \"{expected}\"");
//    if(!defaultPass)
//    {
//        Console.WriteLine($"\tDefault() returned: \"{defaultResult}\"");
//    }
//    if(!roopAsSpanPass)
//    {
//        Console.WriteLine($"\tRoopAsSpan() returned: \"{roopAsSpanResult}\"");
//    }
//    if(!roopAsSpanAndStructBuilderPass)
//    {
//        Console.WriteLine($"\tRoopAsSpanAndStructBuilder() returned: \"{roopAsSpanAndStructBuilderResult}\"");
//    }
//}
//        }

//        Console.WriteLine($"\nTotal Tests: {testCases.Length}, Passed: {passCount}, Failed: {failCount}");

//if(failCount > 0)
//{
//    Environment.Exit(1);
//}
//else
//{
//    Environment.Exit(0);
//}