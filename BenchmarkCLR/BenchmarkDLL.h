#pragma once
//BenchmarkDLL.h
#include <ctime>
#include <stdio.h>
#include <stdlib.h>
#include <sstream>
#include <iostream>
#include <fstream>
using namespace std;

namespace Benchmark {
	public ref class MyBenchmark {
	public:
		void PrintToErrorLog()
		{
			ofstream errorLog;
			errorLog.open("errorlog.txt");
			errorLog << "testinggggg";
			errorLog.close();

		}
		void StartBenchmark(unsigned long time, char* physicalDrive, unsigned long packetSize)
		{
			//start clocks for time
			clock_t _time = clock();
			double firstTime;
			double secondTime;
			double differenceT;

			//create file handles
			ostringstream convert;
			ofstream readFile("readTimes.txt");
			ofstream writeFile("writeTimes.txt");
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
				printf((char*)retVal);
				system("pause");
				return;
			}

#pragma endregion Creates the handle for the storage device

			while ((_time / CLOCKS_PER_SEC) <= _time)
			{
				if (_time*0.7 >= (_time / CLOCKS_PER_SEC))
				{
					//do reads
					firstTime = _time;
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
					secondTime = _time;
					differenceT = secondTime - firstTime;
					convert << differenceT;
					readFile << "\n" + convert.str();
				}
				else
				{
					firstTime = _time;
#pragma region WriteFile

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
							//printf(("Wrote %d bytes to %s successfully.\n"), dwBytesWritten, myDrives[0].physicalLoc.c_str());
						}
					}

#pragma endregion Attempts to Write to the file, and outputs errors otherwise
					secondTime = _time;
					differenceT = secondTime - firstTime;
					convert << differenceT;
					writeFile << "\n" + convert.str();
				}
			}
			readFile.close();
			writeFile.close();
		}
	};
}