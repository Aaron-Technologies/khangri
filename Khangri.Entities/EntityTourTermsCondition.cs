namespace Khangri.Entities
{
    public class EntityTourTermsCondition
    {
        public int TourTermsConditionId { get; set; }
        public int TourTypeId { get; set; }
        public string Accommodation { get; set; }
        public string Food { get; set; }
        public string TermsConditionsPermits { get; set; }
        public string MiscellaneousExpenses { get; set; }
        public string TermsConditionsGeneral { get; set; }
        public string CancellationPolicy { get; set; }
        public bool IsActive { get; set; }
    }
}
