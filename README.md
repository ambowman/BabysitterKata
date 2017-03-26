# BabysitterKata
I did this kata as an exercise in using test driven development. I wrote tests with skeleton methods that would fail first.
Then I wrote what was necessary for the test to pass. I built the application up in this manner.
I tried to commit frequently enough to show the progression. I started with this description:
Babysitter Kata
Background

This kata simulates a babysitter working and getting paid for one night. The rules are pretty straight forward.

The babysitter:

    starts no earlier than 5:00PM
    leaves no later than 4:00AM
    gets paid $12/hour from start-time to bedtime
    gets paid $8/hour from bedtime to midnight
    gets paid $16/hour from midnight to end of job
    gets paid for full hours (no fractional hours)

Feature

As a babysitter
In order to get paid for 1 night of work
I want to calculate my nightly charge

I chose to round up for partial hours and whenever possible I calculated the higher rate if there was a choice 
between rates for partial hours.

To Build and Run this console application:

The application can be downloaded or cloned and either of these two methods 
can be used to build and run it:
1. Go to the solution via Windows File Explorer, open the solution with Visual Studio 2017, build it and run it.
2. Using the Cross Tools Command Prompt for VS2017, navigate to the directory containing the Program.cs.
For me it is located at c:/Dev/Projects/BabysitterKata/BabysitterKat, but this will depend on where you cloned 
or downloaded it to. Next enter 'csc.exe Program.cs' into the command prompt, followed by 'Program.exe'. The
command prompt will show the start of the console application and you can use it as if you opened it in
Visual Studio.
