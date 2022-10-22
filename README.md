# A00466805_MCDA5510_DOTNET

What this program does?
1- It traverse the csv files present under sample data folder, clubs them into a single csv file.
2- The final csv file includes an additional Date column derived from the directories.
3- Everything is done using CSV Helper library
4- log4net is used for logging purpose
5- Program execution timestamp (read + write), count of skipped rows and total rows in the final csv file are primarily logged.
6- Directory not found and file not found exceptions are handled and logs the error. Directory not found test case is simulated by passing incorrect directory name (not "sample data")and can be located in the log file.
7 - app.config file consists of the logging configurations.

How to execute?
1- Clone and open the sln file in visual studio.
2- Set proper paths for read/write operations in DirectoryWalker class
3- If that doesn't work due to any issues then comment the main method of DirectoryWalker class and uncomment the main method of DirWalker class and execute DirWalker class.

I have executed both the classes and both are the same. You may check the results of both classes in the log file.