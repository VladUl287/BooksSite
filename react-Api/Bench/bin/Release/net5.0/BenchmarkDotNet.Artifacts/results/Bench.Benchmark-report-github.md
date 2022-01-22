``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1052 (21H1/May2021Update)
AMD A4-9125 RADEON R3, 4 COMPUTE CORES 2C+2G, 1 CPU, 2 logical and 2 physical cores
.NET SDK=6.0.101
  [Host]     : .NET 5.0.13 (5.0.1321.56516), X64 RyuJIT
  DefaultJob : .NET 5.0.13 (5.0.1321.56516), X64 RyuJIT


```
|              Method |     Mean |    Error |   StdDev |   Gen 0 |  Gen 1 |  Gen 2 | Allocated |
|-------------------- |---------:|---------:|---------:|--------:|-------:|-------:|----------:|
|            Generate | 36.93 μs | 0.177 μs | 0.157 μs | 23.0713 | 1.7090 | 0.1831 |     12 KB |
| Generate_Array_Pool | 36.79 μs | 0.303 μs | 0.253 μs | 23.0713 | 1.6479 | 0.1831 |     12 KB |
