#pragma once
//BenchmarkDLL.h

#ifdef BENCHMARKDLL_EXPORTS
#define BENCHMARKDLL_API __declspec(dllexport)
#else
#define BENCHMARKDLL_API __declspec(dllimport)
#endif

namespace Benchmark
{
	class MyBenchmark 
	{
		public:
			static BENCHMARKDLL_API void StartBenchmark(int time, string physicalDive, int packetSize);
	};
	
}