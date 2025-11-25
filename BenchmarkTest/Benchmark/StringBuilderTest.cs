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

    /// <summary>
    /// Represents the result of processing a single character during numeric string parsing.
    /// </summary>
    private enum CharProcessResult
    {
        Append,     // Character should be appended
        Skip,       // Character should be skipped
        Break       // Stop processing (e.g., second decimal point encountered)
    }

    /// <summary>
    /// Tracks the state during numeric string parsing.
    /// </summary>
    private ref struct ParseState
    {
        public bool HasMinus;
        public bool HasDot;
        public int Length;

        /// <summary>
        /// Determines how to process a character during numeric string parsing.
        /// </summary>
        public CharProcessResult ProcessChar(char c)
        {
            if(char.IsDigit(c))
            {
                Length++;
                return CharProcessResult.Append;
            }
            
            if(c == '-' && Length == 0 && !HasMinus)
            {
                HasMinus = true;
                Length++;
                return CharProcessResult.Append;
            }
            
            if(c == '.' && !HasDot)
            {
                HasDot = true;
                Length++;
                return CharProcessResult.Append;
            }
            
            if(c == '.' && HasDot)
            {
                return CharProcessResult.Break;
            }
            
            return CharProcessResult.Skip;
        }

        /// <summary>
        /// Checks if the result should be empty (e.g., only a minus sign was parsed).
        /// </summary>
        public bool ShouldReturnEmpty(char firstChar)
        {
            return Length == 1 && firstChar == '-';
        }
    }

    [Benchmark]
    public string Default()
    {
        if(string.IsNullOrEmpty(Input))
        {
            return string.Empty;
        }

        var sb = new StringBuilder();
        var state = new ParseState();

        foreach(var c in Input)
        {
            var result = state.ProcessChar(c);
            if(result == CharProcessResult.Append)
            {
                sb.Append(c);
            }
            else if(result == CharProcessResult.Break)
            {
                break;
            }
        }

        if(sb.Length > 0 && state.ShouldReturnEmpty(sb[0]))
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
        var state = new ParseState();

        var span = Input.AsSpan();
        int length = span.Length;
        for(int idx = 0; idx < length; idx++)
        {
            char c = span[idx];
            var result = state.ProcessChar(c);
            if(result == CharProcessResult.Append)
            {
                sb.Append(c);
            }
            else if(result == CharProcessResult.Break)
            {
                break;
            }
        }

        if(sb.Length > 0 && state.ShouldReturnEmpty(sb[0]))
        {
            return string.Empty;
        }

        return sb.ToString();
    }

#if !NET48
    [Benchmark]
    public string RoopAsSpanAndStringHandler()
    {
        if(string.IsNullOrEmpty(Input))
        {
            return string.Empty;
        }

        var sb = new DefaultInterpolatedStringHandler(0,0);
        var state = new ParseState();
        char firstChar = '\0';

        var span = Input.AsSpan();
        int length = span.Length;
        for(int idx = 0; idx < length; idx++)
        {
            char c = span[idx];
            var result = state.ProcessChar(c);
            if(result == CharProcessResult.Append)
            {
                if(state.Length == 1)
                {
                    firstChar = c;
                }
                sb.AppendFormatted(c);
            }
            else if(result == CharProcessResult.Break)
            {
                break;
            }
        }

        if(state.ShouldReturnEmpty(firstChar))
        {
            return string.Empty;
        }

        return sb.ToString();
    }
#endif
}