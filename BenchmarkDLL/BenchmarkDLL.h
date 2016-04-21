#pragma once
//BenchmarkDLL.h


#ifdef __cplusplus
extern "C" {
#endif

#ifdef WIN32
#define EXPORT __declspec(dllexport)
#else
#define EXPORT extern
#endif
	EXPORT void StartBenchmark(unsigned long time, char* physicalDrive, unsigned long packetSize);
	EXPORT void PrintToErrorLog();
#undef EXPORT

#ifdef __cplusplus
}
#endif