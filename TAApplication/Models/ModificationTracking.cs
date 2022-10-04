using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace TAApplication.Models
{
    public class ModificationTracking
    {
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Creation Date")]
        //[DisplayFormat(DateFormat.GeneralDate.ToString("MM/dd/yyyy"))]
        //[DisplayFormat(NullDisplayText = "", DataFormatString = "")] // TODO DateFormat or DateFormatString?
        [ScaffoldColumn(false)]
        public DateTime CreationDate {get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Modification Date")]
        [ScaffoldColumn(false)]
        public DateTime ModificationDate { get; set; }

        [Required]
        [StringLength(150)]
        [Display(Name = "Created by")]
        [ScaffoldColumn(false)]
        public String CreatedBy { get; set; }

        [Required]
        [StringLength(150)]
        [Display(Name = "Modified by")]
        [ScaffoldColumn(false)]
        public String ModifiedBy { get; set; }
    }
}
