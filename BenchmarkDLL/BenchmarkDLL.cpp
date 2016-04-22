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
using namespace std;

void PrintToErrorLog()
{
	ofstream errorLog;
	errorLog.open("C:\\BenchmarkDLL\\errorlog.txt");
	errorLog << "testinggggg";
	errorLog.close();
	printf("This is a thing");

}
void StartBenchmark(unsigned long timer, char* physicalDrive, unsigned long packetSize)
{
	//create file handles
	ostringstream convert;
	ofstream readFile("C:\\BenchmarkDLL\\readTimes.txt");
	ofstream writeFile("C:\\BenchmarkDLL\\writeTimes.txt");
	ofstream errorLog("C:\\BenchmarkDLL\\errorlog.txt");
	errorLog << physicalDrive << "\n";

	// creating a buffer for writing to the drive.
	LPVOID DataBuffer = VirtualAlloc(NULL, packetSize, MEM_COMMIT, PAGE_READWRITE);
	memset(DataBuffer, 0xCC, packetSize);

	DWORD dwBytesToWrite = packetSize;
	DWORD dwBytesWritten = 0;
	DWORD retVal = 0;

	

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
	time_t start = time(NULL);
	time_t firstTime, end;
	time_t timeLeft = start+timer;
	time_t twoThirds = start+(timer*0.7);
	errorLog << time(NULL)+timer << endl;

	while ( time(NULL) <= timeLeft)
	{
		if (time(NULL) <= twoThirds)
		{
			//do reads
			firstTime = time(0);
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
				end = time(0);
				readFile << "\n 128k Read: " << end - start << endl;;
				timeLeft -= (end - start);
			}
		
		}
		else
		{
			
#pragma region WriteFile
			firstTime = time(0);
			retVal = WriteFile(hDrive,
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
					end = time(0);
					timeLeft -= (end - start);
					writeFile << "\n 128k Write: " << end - start << endl;
					//printf(("Wrote %d bytes to %s successfully.\n"), dwBytesWritten, myDrives[0].physicalLoc.c_str());
				}
			}

#pragma endregion Attempts to Write to the file, and outputs errors otherwise
			
						
		}
	}
	readFile.close();
	writeFile.close();
}



