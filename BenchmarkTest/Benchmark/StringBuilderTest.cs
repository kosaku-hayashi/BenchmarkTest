using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkApp;


[MemoryDiagnoser]
[ShortRunJob]
[MarkdownExporterAttribute.GitHub]
public class StringBuilderTest
{
    static string Input = "number212text345number212text345number212text345";

    static StringBuilderTest()
    {
        
    }

    //public StringBuilderTest(string input)
    //{
    //    this.Input = input;
    //}

    [Benchmark]
    public string Default()
    {
        if(string.IsNullOrEmpty(Input))
        {
            return string.Empty;
        }

        var sb = new StringBuilder();
        bool hasMinus = false;
        bool hasDot = false;

        foreach(var c in Input)
        {
            if(char.IsDigit(c))
            {
                sb.Append(c);
            }
            else if(c == '-' && sb.Length == 0 && !hasMinus)
            {
                sb.Append(c);
                hasMinus = true;
            }
            else if(c == '.' && !hasDot)
            {
                sb.Append(c);
                hasDot = true;
            }
            else
            {
                if(c == '.' && hasDot)
                {
                    break;
                }
            }
        }

        if(sb.Length == 1 && sb[0] == '-')
        {
            return string.Empty;
        }

        return sb.ToString();
    }

    [Benchmark]
    public string RoopAsSpan()
    {
        if(string.IsNullOrEmpty(Input))
        {
            return string.Empty;
        }

        var sb = new StringBuilder();
        bool hasMinus = false;
        bool hasDot = false;

        var span = Input.AsSpan();
        int length = span.Length;
        for(int idx = 0; idx < length; idx++)
        {
            char c = span[idx];
            if(char.IsDigit(c))
            {
                sb.Append(c);
            }
            else if(c == '-' && sb.Length == 0 && !hasMinus)
            {
                sb.Append(c);
                hasMinus = true;
            }
            else if(c == '.' && !hasDot)
            {
                sb.Append(c);
                hasDot = true;
            }
            else
            {
                if(c == '.' && hasDot)
                {
                    break;
                }
            }
        }

        if(sb.Length == 1 && sb[0] == '-')
        {
            return string.Empty;
        }

        return sb.ToString();
    }

    [Benchmark]
    public string RoopAsSpanAndStringHandler()
    {
        if(string.IsNullOrEmpty(Input))
        {
            return string.Empty;
        }

        var sb = new DefaultInterpolatedStringHandler(0,0);
        int sbLength = 0;
        bool hasMinus = false;
        bool hasDot = false;

        var span = Input.AsSpan();
        int length = span.Length;
        for(int idx = 0; idx < length; idx++)
        {
            char c = span[idx];
            if(char.IsDigit(c))
            {
                sb.AppendFormatted(c);
                sbLength++;
            }
            else if(c == '-' && sbLength == 0 && !hasMinus)
            {
                sb.AppendFormatted(c);
                sbLength++;
                hasMinus = true;
            }
            else if(c == '.' && !hasDot)
            {
                sb.AppendFormatted(c);
                sbLength++;
                hasDot = true;
            }
            else
            {
                if(c == '.' && hasDot)
                {
                    break;
                }
            }
        }

        if(sbLength == 1 && span[0] == '-')
        {
            return string.Empty;
        }

        return sb.ToString();
    }
}