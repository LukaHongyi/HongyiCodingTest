namespace HongyiCodingTest.Interfaces
{
    public interface ILetterService
    {
        void ArchiveLetters(string inputFile, string resultFile);
        void CombineTwoLetters(string inputFile1, string inputFile2, string resultFile);
    }
}