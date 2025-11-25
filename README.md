[BenchmarkApp.ReflectionTest-report-github.md](https://github.com/user-attachments/files/23740619/BenchmarkApp.ReflectionTest-report-github.md)
```

BenchmarkDotNet v0.15.7, Windows 11 (10.0.26100.6899/24H2/2024Update/HudsonValley)
AMD Ryzen 9 5900X 3.70GHz, 1 CPU, 24 logical and 12 physical cores
.NET SDK 10.0.100
  [Host]             : .NET 8.0.22 (8.0.22, 8.0.2225.52707), X64 RyuJIT x86-64-v3
  .NET 10.0          : .NET 10.0.0 (10.0.0, 10.0.25.52411), X64 RyuJIT x86-64-v3
  .NET 6.0           : .NET 6.0.36 (6.0.36, 6.0.3624.51421), X64 RyuJIT x86-64-v3
  .NET 7.0           : .NET 7.0.20 (7.0.20, 7.0.2024.26716), X64 RyuJIT x86-64-v3
  .NET 8.0           : .NET 8.0.22 (8.0.22, 8.0.2225.52707), X64 RyuJIT x86-64-v3
  .NET 9.0           : .NET 9.0.11 (9.0.11, 9.0.1125.51716), X64 RyuJIT x86-64-v3
  .NET Framework 4.8 : .NET Framework 4.8.1 (4.8.9310.0), X64 RyuJIT VectorSize=256


```
| Method         | Job                | Runtime            | Mean        | Error     | StdDev     | Median      | Gen0   | Allocated |
|--------------- |------------------- |------------------- |------------:|----------:|-----------:|------------:|-------:|----------:|
| NonReflection  | .NET 10.0          | .NET 10.0          |   0.0019 ns | 0.0026 ns |  0.0021 ns |   0.0008 ns |      - |         - |
| Reflection     | .NET 10.0          | .NET 10.0          |  48.3733 ns | 0.8212 ns |  0.7682 ns |  48.5067 ns | 0.0033 |      56 B |
| Cache          | .NET 10.0          | .NET 10.0          |  24.8603 ns | 0.4499 ns |  0.3989 ns |  24.9160 ns | 0.0033 |      56 B |
| ExpressionTree | .NET 10.0          | .NET 10.0          |   3.9133 ns | 0.0979 ns |  0.1435 ns |   3.8784 ns | 0.0019 |      32 B |
| NonReflection  | .NET 6.0           | .NET 6.0           |   3.2571 ns | 0.0265 ns |  0.0248 ns |   3.2561 ns | 0.0019 |      32 B |
| Reflection     | .NET 6.0           | .NET 6.0           | 161.3548 ns | 3.2360 ns |  2.7022 ns | 162.2514 ns | 0.0033 |      56 B |
| Cache          | .NET 6.0           | .NET 6.0           | 117.0699 ns | 0.9473 ns |  0.8861 ns | 116.9589 ns | 0.0033 |      56 B |
| ExpressionTree | .NET 6.0           | .NET 6.0           |   4.5544 ns | 0.0286 ns |  0.0254 ns |   4.5570 ns | 0.0019 |      32 B |
| NonReflection  | .NET 7.0           | .NET 7.0           |   2.7875 ns | 0.0302 ns |  0.0268 ns |   2.7941 ns | 0.0019 |      32 B |
| Reflection     | .NET 7.0           | .NET 7.0           |  78.1456 ns | 0.3515 ns |  0.3288 ns |  78.2716 ns | 0.0033 |      56 B |
| Cache          | .NET 7.0           | .NET 7.0           |  44.6282 ns | 0.4656 ns |  0.4355 ns |  44.6141 ns | 0.0033 |      56 B |
| ExpressionTree | .NET 7.0           | .NET 7.0           |   4.2787 ns | 0.0695 ns |  0.0580 ns |   4.2683 ns | 0.0019 |      32 B |
| NonReflection  | .NET 8.0           | .NET 8.0           |   2.2815 ns | 0.0457 ns |  0.0428 ns |   2.2652 ns | 0.0019 |      32 B |
| Reflection     | .NET 8.0           | .NET 8.0           |  58.5331 ns | 0.6652 ns |  0.5555 ns |  58.5023 ns | 0.0033 |      56 B |
| Cache          | .NET 8.0           | .NET 8.0           |  28.9757 ns | 0.5920 ns |  0.8103 ns |  29.0562 ns | 0.0033 |      56 B |
| ExpressionTree | .NET 8.0           | .NET 8.0           |   4.4330 ns | 0.0495 ns |  0.0439 ns |   4.4254 ns | 0.0019 |      32 B |
| NonReflection  | .NET 9.0           | .NET 9.0           |   0.0000 ns | 0.0000 ns |  0.0000 ns |   0.0000 ns |      - |         - |
| Reflection     | .NET 9.0           | .NET 9.0           |  60.2602 ns | 0.6465 ns |  0.5399 ns |  60.0130 ns | 0.0033 |      56 B |
| Cache          | .NET 9.0           | .NET 9.0           |  28.4780 ns | 0.5296 ns |  0.4422 ns |  28.2722 ns | 0.0033 |      56 B |
| ExpressionTree | .NET 9.0           | .NET 9.0           |   4.1629 ns | 0.0967 ns |  0.0904 ns |   4.1694 ns | 0.0019 |      32 B |
| NonReflection  | .NET Framework 4.8 | .NET Framework 4.8 |   2.6812 ns | 0.0743 ns |  0.1112 ns |   2.6194 ns | 0.0051 |      32 B |
| Reflection     | .NET Framework 4.8 | .NET Framework 4.8 | 313.0986 ns | 1.2029 ns |  1.0663 ns | 313.0651 ns | 0.0291 |     185 B |
| Cache          | .NET Framework 4.8 | .NET Framework 4.8 | 259.7805 ns | 5.1445 ns | 10.0339 ns | 257.3801 ns | 0.0291 |     185 B |
| ExpressionTree | .NET Framework 4.8 | .NET Framework 4.8 |  12.7466 ns | 0.0538 ns |  0.0449 ns |  12.7282 ns | 0.0051 |      32 B |
