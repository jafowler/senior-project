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
	EXPORT void StartBenchmark(unsigned long timer, char* physicalDrive, unsigned long packetSize, unsigned long readWriteRatio);
	EXPORT void PrintToErrorLog();
#undef EXPORT

#ifdef __cplusplus
}
#endif