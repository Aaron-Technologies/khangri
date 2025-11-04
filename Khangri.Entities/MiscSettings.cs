namespace Khangri.Entities
{
    public class MiscSettings
    {
        public string LocationsImagePath { get; init; }
        public string ImagesPath { get; init; }
        public string SightSeeingImagePath { get; init; }
        public string SliderImagePath { get; set; }
        public string RoomTypeImagePath { get; init; }
        public string QuestionExcelPath { get; init; }
        public string QuestionZipPath { get; init; }
        public string ShipTypeSectionSupportFilePath { get; init; }
        public string ImageFilenameFormat { get; init; }
        public string SliderFilenameFormat { get; init; }
        public string ReportImagesAndFiles { get; init; }
        public string TimeStampFormat { get; init; }
        public string MenuCacheKey { get; init; }
        public string EncryptionKey { get; init; }
        public string IV { get; init; }
        public string MandatorySection { get; init; }
    }
}
