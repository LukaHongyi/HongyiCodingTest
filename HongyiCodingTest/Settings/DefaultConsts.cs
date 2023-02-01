namespace HongyiCodingTest.Settings
{
    //Default const saved here
    static class DefaultSettings
    {
        public const string AdmissionFolderName = "Admission";
        public const string ScholarshipFolderName = "Scholarship";
        public const string AdmissionLetterPrefix = AdmissionFolderName + "-";
        public const string ScholarshipLetterPrefix = ScholarshipFolderName + "-";
        public const string CombinedLetterPrefix = "CombinedLetter-";
        public const string TimeZone = "Central Standard Time";
        public const string DateFolderFormation1 = "yyyyMMdd";
        public const string DateFolderFormation2 = "yyyy/MM/dd";
        public const string InputPath = "../../testSystem/input";
        public const string InputAdmissionPath = "../../testSystem/input/" + AdmissionFolderName;
        public const string InputScholarshipPath = "../../testSystem/input/" + ScholarshipFolderName;
        public const string ArchivePath = "../../testSystem/Archive";
        public const string OutputPath = "../../testSystem/Output";
    }
}