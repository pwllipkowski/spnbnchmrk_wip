using BenchmarkDotNet.Running;

namespace SpanBenchmarking
{
  public class Program
  {
    public static void Main(string[] args)
    {
      BenchmarkRunner.Run(typeof(Program).Assembly);
    }
  }
}
