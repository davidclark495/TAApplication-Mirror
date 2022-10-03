using System.ComponentModel.DataAnnotations;

namespace TAApplication.Models
{
    public class ModificationTracking
    {
        [ScaffoldColumn(false)]
        public DateTime CreationDate {get; set; }

        [ScaffoldColumn(false)]
        public DateTime ModificationDate { get; set; }

        [ScaffoldColumn(false)]
        public String CreatedBy { get; set; }

        [ScaffoldColumn(false)]
        public String ModifiedBy { get; set; }
    }
}
