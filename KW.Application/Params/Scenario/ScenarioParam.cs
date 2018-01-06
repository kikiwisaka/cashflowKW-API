using System.ComponentModel.DataAnnotations;
using System;

namespace KW.Application.Params
{
    public class ScenarioParam
    {
        [Required]
        public string NamaScenario { get; set; }
        public int LikehoodId { get; set; }
        public string[] ProjectId { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool? Status { get; set; }
        public bool? IsDefault { get; set; }
        public bool? IsUpdate { get; set; }


        public ScenarioParam() { }
    }
}
