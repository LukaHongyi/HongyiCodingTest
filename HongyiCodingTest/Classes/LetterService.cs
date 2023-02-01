using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using HongyiCodingTest.Interfaces;
using HongyiCodingTest.Settings;


namespace HongyiCodingTest.Classes
{
    public class LetterService : ILetterService
    {
        
        public void ArchiveLetters(string inputFile, string resultFile)
        {
            // Get the current date (Central Standard Time) in default
            string curDate = GetCurDate();
            
            // Get the source Path and the target Path
            string admissionSourceFolder = Path.Combine(inputFile, DefaultSettings.AdmissionFolderName, curDate);
            string scholarshipSourceFolder = Path.Combine(inputFile, DefaultSettings.ScholarshipFolderName, curDate);
            string admissionArchiveFolder = Path.Combine(resultFile, DefaultSettings.AdmissionFolderName);
            string scholarshipArchiveFolder = Path.Combine(resultFile, DefaultSettings.ScholarshipFolderName);  
            
            
            // Check if Admission and Scholarship folders exist, otherwise create one.
            if (!Directory.Exists(admissionArchiveFolder))             　　              
                Directory.CreateDirectory(admissionArchiveFolder); 
            if (!Directory.Exists(scholarshipArchiveFolder))             　　              
                Directory.CreateDirectory(scholarshipArchiveFolder);
            
            // Check if today has any admission letter
            if (Directory.Exists(admissionSourceFolder))
            {
                // Check if the date folder has been created in archive folder
                if(!Directory.Exists(Path.Combine(admissionArchiveFolder, curDate)))
                    Directory.CreateDirectory(Path.Combine(admissionArchiveFolder, curDate)); 
                
                string[] admissionFiles = Directory.GetFiles(admissionSourceFolder);
                
                // Copy the files and overwrite destination files if they already exist.
                foreach (string s in admissionFiles)
                {
                    // Use static Path methods to extract only the file name from the path.
                    string fileName = Path.GetFileName(s);
                    string destFile = Path.Combine(admissionArchiveFolder, curDate, fileName);
                    File.Move(s, destFile);
                }
                
            }   
 
            // Check if today has any scholarship letter
            if (Directory.Exists(scholarshipSourceFolder))
            {
                // Check if the date folder has been created in archive folder
                if(!Directory.Exists(Path.Combine(scholarshipArchiveFolder, curDate)))
                    Directory.CreateDirectory(Path.Combine(scholarshipArchiveFolder, curDate)); 
                
                string[] scholarshipFiles = Directory.GetFiles(scholarshipSourceFolder);
                
                // Copy the files and overwrite destination files if they already exist.
                foreach (string s in scholarshipFiles)
                {
                    // Use static Path methods to extract only the file name from the path.
                    string fileName = Path.GetFileName(s);
                    string destFile = Path.Combine(scholarshipArchiveFolder, curDate, fileName);
                    File.Move(s, destFile);
                }
            }
            
            // Delete the input dated folder
            Directory.Delete(admissionSourceFolder);
            Directory.Delete(scholarshipSourceFolder);
            
        }
        
        public void CombineTwoLetters(string inputFile1, string inputFile2, string resultFile)
        {
            // Creat a list to hold all the student ids that need to be combined
            string[] combinedLetterIds = new string[] {};
            
            // Get the current date (Central Standard Time) in default
            string curDate = GetCurDate();
            
            // Get the source Path and the target Path
            string admissionSourceFolder = Path.Combine(inputFile1, curDate);
            string scholarshipSourceFolder = Path.Combine(inputFile2, curDate);
            string combinedLettersFolder = Path.Combine(resultFile, curDate);

            // Check if today's output folder exist or not.
            if (!Directory.Exists(combinedLettersFolder))             　　              
                Directory.CreateDirectory(combinedLettersFolder);
            
            // Get student ids that both in admission and scholarship folder
            if (Directory.Exists(admissionSourceFolder) && Directory.Exists(scholarshipSourceFolder))
            {
                string[] admissionFiles = Directory.GetFiles(admissionSourceFolder);
                string[] scholarshipFiles = Directory.GetFiles(scholarshipSourceFolder);
                // Create two sets to hold admission student ids and scholarship student ids
                var admissionStudents = GetAllStudentIDs(admissionFiles);
                var scholarshipStudents = GetAllStudentIDs(scholarshipFiles);
                // Get the intersection of those two sets
                var intersectSet = admissionStudents.Intersect(scholarshipStudents);
                combinedLetterIds = intersectSet.ToArray();
            }
            
            // Combine letters for all the student ids in the combine list
            foreach (var studentId in combinedLetterIds)
            {
                string admissionContent = File.ReadAllText(Path.Combine(admissionSourceFolder, 
                    DefaultSettings.AdmissionLetterPrefix + studentId + ".txt"));
                string scholarshipContent = File.ReadAllText(Path.Combine(scholarshipSourceFolder, 
                    DefaultSettings.ScholarshipLetterPrefix + studentId + ".txt"));
                // Simply merge their content
                File.WriteAllText(Path.Combine(combinedLettersFolder, 
                    DefaultSettings.CombinedLetterPrefix + studentId + ".txt"), admissionContent+scholarshipContent);
            }
            
            // Generate report file
            using (StreamWriter sw = File.CreateText(Path.Combine(combinedLettersFolder, curDate + "-report.txt")))
            {
                sw.WriteLine(GetCurDate2() + " Report");
                sw.WriteLine("-------------------------------------------------------");
                sw.WriteLine("");
                sw.WriteLine("Number of Combined Letters: " + combinedLetterIds.Length); 
                // List all the combined student IDs
                foreach (string s in combinedLetterIds)
                {
                    sw.WriteLine(s);
                }
            }
        }
        
        // Get the default timeZone's date with the formation "yyyyMMdd" 
        private string GetCurDate()
        {
            TimeZoneInfo targetZone = TimeZoneInfo.FindSystemTimeZoneById(DefaultSettings.TimeZone);
            string curDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, targetZone).ToString(DefaultSettings.DateFolderFormation1);
            return curDate;
        }

        // Get the default timeZone's date with the formation "yyyy/MM/dd" 
        private string GetCurDate2()
        {
            TimeZoneInfo targetZone = TimeZoneInfo.FindSystemTimeZoneById(DefaultSettings.TimeZone);
            string curDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, targetZone).ToString(DefaultSettings.DateFolderFormation2);
            return curDate;
        }

        // Get all student ID from the input folder's files
        private HashSet<string> GetAllStudentIDs(string[] files)
        {
            // Use set to avoid duplicate student id
            var studentIDs = new HashSet<string>();
            foreach (string s in files)
            {
                // Use static Path methods to extract only the file name from the path.
                string fileName = Path.GetFileName(s);
                Regex pattern = new Regex("\\d{8}");
                // if file name does not match the student ID formation
                if (!pattern.IsMatch(fileName))
                {
                    continue;
                }
                // Get the student ID and add into the set
                MatchCollection mc = pattern.Matches(fileName);
                studentIDs.Add(mc[0].Value);
            }
            return studentIDs;
        }
    }
}