using BenchmarkDotNet.Attributes;
using System.Text;

namespace SpanBenchmarking
{
  [MemoryDiagnoser]
  public class SpanBenchmarking
  {
    #region Test data and setup

    public string Test = string.Empty;

    public string TestBig = string.Empty;

    [GlobalSetup]
    public void Setup()
    {
      Test = "100|250|450|-5|600|115|10|9412|";

      var seed = 1057823;
      var random = new Random(seed);

      var sb = new StringBuilder();
      for (var i = 0; i < 10000; ++i)
      {
        var nextVal = random.Next(32768) - 32768 / 2;
        sb.Append(nextVal).Append('|');
      }
      TestBig = sb.ToString();
    }

    #endregion

    #region Methods

    #region GetMin

    public string GetMinSplit(string input)
    {
      var splits = input.Split('|', StringSplitOptions.RemoveEmptyEntries);
      int? minValue = null;
      string minValueStr = null;

      for (var i = 0; i < splits.Length; i++)
      {
        var value = int.Parse(splits[i]);
        if (minValue.HasValue == false || value < minValue)
        {
          minValue = value;
          minValueStr = splits[i];
        }
      }

      return minValueStr;
    }

    public string GetMinSubstring(string input)
    {
      var start = 0;
      int? minValue = null;
      string minValueStr = null;

      for (var i = 0; i < input.Length; i++)
      {
        if (input[i] == '|')
        {
          var length = i - start;
          var substring = input.Substring(start, length);
          var value = int.Parse(substring);

          if (minValue.HasValue == false || value < minValue)
          {
            minValue = value;
            minValueStr = substring;
          }
          start = i + 1;
        }
      }

      return minValueStr;
    }

    public ReadOnlySpan<char> GetMinSpan(string input)
    {
      var start = 0;
      ReadOnlySpan<char> span = input;

      int? minValue = null;
      ReadOnlySpan<char> minSlice = null;

      for (var i = 0; i < span.Length; i++)
      {
        if (span[i] == '|')
        {
          var length = i - start;
          var slice = span.Slice(start, length);
          var value = int.Parse(slice);

          if (minValue.HasValue == false || value < minValue)
          {
            minValue = value;
            minSlice = slice;
          }

          start = i + 1;
        }
      }

      return minSlice;
    }

    #endregion

    #region GetSum

    public static int GetSumSplit(string input)
    {
      var sum = 0;
      var splits = input.Split('|', StringSplitOptions.RemoveEmptyEntries);

      for (var i = 0; i < splits.Length; i++)
      {
        var value = int.Parse(splits[i]);
        sum += value;
      }

      return sum;
    }

    public static int GetSumSubstring(string input)
    {
      var sum = 0;
      var start = 0;

      for (var i = 0; i < input.Length; i++)
      {
        if (input[i] == '|')
        {
          var length = i - start;
          var substring = input.Substring(start, length);
          var value = int.Parse(substring);
          sum += value;
          start = i + 1;
        }
      }

      return sum;
    }

    public static int GetSumSpan(string input)
    {
      var sum = 0;
      var start = 0;
      ReadOnlySpan<char> span = input;

      for (var i = 0; i < span.Length; ++i)
      {
        if (span[i] == '|')
        {
          var length = i - start;
          var slice = span.Slice(start, length);
          var value = int.Parse(slice);
          sum += value;
          start = i + 1;
        }
      }

      return sum;
    }

    #endregion

    #region GetMin



    #endregion

    #endregion

    #region Benchmarks

    #region GetMin

    [Benchmark]
    public string GetMinSplit() => GetMinSplit(Test);

    [Benchmark]
    public string GetMinSubstring() => GetMinSubstring(Test);

    [Benchmark]
    public ReadOnlySpan<char> GetMinSpan() => GetMinSpan(Test);

    [Benchmark]
    public string GetMinSplitBig() => GetMinSplit(TestBig);

    [Benchmark]
    public string GetMinSubstringBig() => GetMinSubstring(TestBig);

    [Benchmark]
    public ReadOnlySpan<char> GetMinSpanBig() => GetMinSpan(TestBig);

    #endregion

    #region GetSum

    [Benchmark]
    public int GetSumSplit() => GetSumSplit(Test);

    [Benchmark]
    public int GetSumSubstring() => GetSumSubstring(Test);

    [Benchmark]
    public int GetSumSpan() => GetSumSpan(Test);

    [Benchmark]
    public int GetSumSplitBig() => GetSumSplit(TestBig);

    [Benchmark]
    public int GetSumSubstringBig() => GetSumSubstring(TestBig);

    [Benchmark]
    public int GetSumSpanBig() => GetSumSpan(TestBig);

    #endregion

    #region GetMax

    public string GetMaxSplit(string input)
    {
      var splits = input.Split('|', StringSplitOptions.RemoveEmptyEntries);
      int? maxValue = null;
      string maxValueStr = null;

      for (var i = 0; i < splits.Length; i++)
      {
        var value = int.Parse(splits[i]);
        if (maxValue.HasValue == false || value > maxValue)
        {
          maxValue = value;
          maxValueStr = splits[i];
        }
      }

      return maxValueStr;
    }
    
    public string GetMaxSubstring(string input)
    {
      var start = 0;
      int? maxValue = null;
      string maxValueStr = null;

      for (var i = 0; i < input.Length; i++)
      {
        if (input[i] == '|')
        {
          var length = i - start;
          var substring = input.Substring(start, length);
          var value = int.Parse(substring);

          if (maxValue.HasValue == false || value > maxValue)
          {
            maxValue = value;
            maxValueStr = substring;
          }
          start = i + 1;
        }
      }

      return maxValueStr;
    }

    public ReadOnlySpan<char> GetMaxSpan(string input)
    {
      var start = 0;
      ReadOnlySpan<char> span = input;

      int? maxValue = null;
      ReadOnlySpan<char> maxSlice = null;

      for (var i = 0; i < span.Length; i++)
      {
        if (span[i] == '|')
        {
          var length = i - start;
          var slice = span.Slice(start, length);
          var value = int.Parse(slice);

          if (maxValue.HasValue == false || value > maxValue)
          {
            maxValue = value;
            maxSlice = slice;
          }

          start = i + 1;
        }
      }

      return maxSlice;
    }

    #endregion

    #endregion

    //[Benchmark]
    //public string GetOutputSpanFromSpan()
    //{
    //  var maxSlice = GetMaxSpan(Test);
    //  var result = BuildOutputSpan(maxSlice);
    //  return result;
    //}

    //[Benchmark]
    //public string GetOutputSpanFromSubstring()
    //{
    //  var substring = GetMaxSubstring(Test);
    //  var result = BuildOutputSpan(substring);
    //  return result;
    //}

    //[Benchmark]
    //public string GetOutputFromSubstring()
    //{
    //  var substring = GetMaxSubstring(Test);
    //  var result = BuildOutput(substring);
    //  return result;
    //}

    //public Span<char> BuildOutputSpanReturnSpan(ReadOnlySpan<char> input)
    //{
    //  var count = input.Length;
    //  var bufferIndex = 0;
    //  Span<char> buffer = stackalloc char[count * count + (count - 1)];

    //  for (var i = 0; i < count; ++i)
    //  {
    //    for (var j = 0; j < count; ++j)
    //    {
    //      buffer[bufferIndex] = input[j];
    //      bufferIndex++;
    //    }
    //    if (i < count - 1)
    //    {
    //      buffer[bufferIndex] = '-';
    //      bufferIndex++;
    //    }
    //  }

    //  Span<char> span = buffer;
    //  var slice = span.Slice(0, span.Length);
    //  return slice;
    //}

    //public string BuildOutputSpan(ReadOnlySpan<char> input)
    //{
    //  var count = input.Length;
    //  var bufferIndex = 0;
    //  Span<char> buffer = stackalloc char[count * count + (count - 1)];

    //  for(var i = 0; i < count; ++i)
    //  {
    //    for(var j = 0; j < count; ++j)
    //    {
    //      buffer[bufferIndex] = input[j];
    //      bufferIndex++;
    //    }
    //    if(i < count -1)
    //    {
    //      buffer[bufferIndex] = '-';
    //      bufferIndex++;
    //    }
    //  }
    //  var result = buffer.ToString();
    //  return result;
    //}

    //public string BuildOutput(string input)
    //{
    //  var count = input.Length;
    //  var capacity = input.Length * input.Length + input.Length - 1;
    //  var sb = new StringBuilder(capacity);
    //  for (var i = 0; i < count; ++i)
    //  {
    //    sb.Append(input);
    //    if (i < count - 1)
    //    {
    //      sb.Append('-');
    //    }
    //  }
    //  var result = sb.ToString();
    //  return result;
    //}




  }
}
