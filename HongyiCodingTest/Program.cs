using HongyiCodingTest.Classes;
using HongyiCodingTest.Interfaces;
using HongyiCodingTest.Settings;

namespace HongyiCodingTest
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            //Initialize a letterService and run the archive and combine function
            ILetterService letterService = new LetterService();
            letterService.CombineTwoLetters(DefaultSettings.InputAdmissionPath ,
                DefaultSettings.InputScholarshipPath, DefaultSettings.OutputPath);
            letterService.ArchiveLetters(DefaultSettings.InputPath, DefaultSettings.ArchivePath);
        }
    }
}