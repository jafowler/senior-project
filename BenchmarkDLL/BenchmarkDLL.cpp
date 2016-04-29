// BenchmarkDLL.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "BenchmarkDLL.h"
#include <ctime>
#include <stdio.h>
#include <stdlib.h>
#include <sstream>
#include <iostream>
#include <fstream>
#include <conio.h>
#include <Windows.h>
using namespace std;

void PrintToErrorLog()
{
	ofstream errorLog;
	errorLog.open("C:\\BenchmarkDLL\\errorlog.txt");
	errorLog << "testinggggg";
	errorLog.close();
	printf("This is a thing");

}
void StartBenchmark(unsigned long timer, char* physicalDrive, unsigned long packetSize, unsigned long readWriteRatio)
{
	double readPercent = readWriteRatio / 100.0;
	//create file handles
	ostringstream convert;
	ofstream readFile("C:\\BenchmarkDLL\\readTimes.txt");
	ofstream writeFile("C:\\BenchmarkDLL\\writeTimes.txt");
	ofstream errorLog("C:\\BenchmarkDLL\\errorlog.txt");

	// creating a buffer for read/write to the drive.
	LPVOID DataBuffer = VirtualAlloc(NULL, packetSize, MEM_COMMIT, PAGE_READWRITE);
	memset(DataBuffer, 0xCC, packetSize);

	DWORD dwBytesToWrite = packetSize;
	DWORD dwBytesWritten = 0;
	DWORD retVal = 0;
	errorLog << "Read Percent: " << readPercent << endl;

	

#pragma region  hDrive

	HANDLE hDrive = CreateFile(physicalDrive,
		GENERIC_WRITE | GENERIC_READ,
		FILE_SHARE_READ | FILE_SHARE_WRITE,
		NULL,
		OPEN_EXISTING,
		NULL,
		NULL);


	if (hDrive == INVALID_HANDLE_VALUE)
	{
		retVal = GetLastError();
		errorLog << (char*)retVal;
		system("pause");
		return;
	}

#pragma endregion Creates the handle for the storage device
	//start clocks for time
	LARGE_INTEGER frequency;
	LARGE_INTEGER currentTime;
	currentTime.QuadPart = 0;
	LARGE_INTEGER lastRecordedTime;
	double timeLeft = timer * 1000.0;
	double elapsedMs = 0;
	double totalElapsed = 0;
	QueryPerformanceFrequency(&frequency);
	QueryPerformanceCounter(&lastRecordedTime);


	while (totalElapsed < timeLeft)
	{
		if (totalElapsed <= timeLeft*0.7)
		{
			retVal = ReadFile(hDrive,
				DataBuffer,
				dwBytesToWrite,
				&dwBytesWritten,
				NULL);
			if (FALSE == retVal)
			{
				retVal = GetLastError();
				printf((char*)retVal);
				system("pause");
				return;
			}
			else
			{
				QueryPerformanceCounter(&currentTime);
				elapsedMs = ((currentTime.QuadPart - lastRecordedTime.QuadPart) * 1000.0) / frequency.QuadPart;
				readFile << packetSize <<" Read: " << elapsedMs << endl;
				lastRecordedTime = currentTime;
				totalElapsed += elapsedMs;
			}
		}
		else
		{
			if (WriteFile(hDrive, DataBuffer, dwBytesToWrite, &dwBytesWritten, NULL) == FALSE)
			{
				retVal = GetLastError();
				errorLog << retVal << endl;
				return;
			}
			else
			{
				//found these edge cases on the MSDN website
				if (dwBytesWritten != dwBytesToWrite)
				{
					// This is an error because a synchronous write that results in
					// success (WriteFile returns TRUE) should write all data as
					// requested. This would not necessarily be the case for
					// asynchronous writes.
					printf("Error: dwBytesWritten != dwBytesToWrite\n");
				}
				else
				{
					QueryPerformanceCounter(&currentTime);
					elapsedMs = ((currentTime.QuadPart - lastRecordedTime.QuadPart) * 1000.0) / frequency.QuadPart;
					writeFile << "128k Write: " << elapsedMs << endl;
					lastRecordedTime = currentTime;
					totalElapsed += elapsedMs;
				}
			}
		}
		
	}
	readFile.close();
	writeFile.close();
}



