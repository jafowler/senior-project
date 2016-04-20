// BenchmarkDLL.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "BenchmarkDLL.h"
#include <string>
#include <ctime>
using namespace std;
namespace Benchmark 
{
	void StartBenchmark(int time, string physicalDive, int packetSize)
	{
		clock_t startTime = clock();

		while ((startTime / CLOCKS_PER_SEC) <= time)
		{
			if (time*.07 >= (startTime / CLOCKS_PER_SEC))
			{
				//do reads
			}
			else
			{
				//do writes
			}
		}
	}
}
