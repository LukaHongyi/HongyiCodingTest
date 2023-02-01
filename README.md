# HongyiXu Coding Exercise

## How to run this program?

Simply run the main function. You can also set the default value of the file path in `HongyiCodingTest.Settings.DefaultConsts.cs`. The default test path is `../../testSystem`（which is at the root of the project).

## Test cases

When testing, there are some mocked files in the test System. After running this program, the program will automatically archive and combine this day's input admission and scholarship letters (the default time zone is `Central Standard Time`, which can also be changed in settings).

### Case 1 (when no letter on that day)

If there is no file on a certain day, there will be no dated folder in the Input folder. In this case, no file will be archived and will not have a dated folder in the Archive folder as well. However, there will be a report in the Output folder as usual, the number of combined letters will be 0.

<img src="HongyiCodingTest\ScreenShots\NoletterReport.png" style="zoom:33%;" />

### Case2 (only Admission letters or only Scholarship letters)

If there are only Admission letters or Scholarship letters, the program will archive any existing letters and generate a 0 combined letter report.

### Case3 (Admission letters and Scholarship letters)

The program will handle well with archiving all of the existing letters into the Archive Folder and combining all the same student ID letters. Finally, the program will generate a report showing all combined student IDs and the total number of combined letters.

<img src="HongyiCodingTest\ScreenShots\CombineResult.png" style="zoom:33%;" />

<img src="HongyiCodingTest\ScreenShots\CombineReport.png" style="zoom:33%;" />