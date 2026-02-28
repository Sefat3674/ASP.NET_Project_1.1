using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Domain.Entities
{
    public class SalarySlipDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string MonthName {  get; set; } = string.Empty;
        public int SalaryMonth { get; set; }
        public int SalaryYear { get; set; }

        public decimal BasicSalary { get; set; }
        public decimal HouseRentAllowance { get; set; }
        public decimal MedicalAllowance { get; set; }
        public decimal OtherAllowance { get; set; }

        public decimal TotalBonus { get; set; }
        public decimal TotalDeduction { get; set; }
        public decimal NetSalary { get; set; }

        public string BonusDetails { get; set; }
        public string DeductionDetails { get; set; }

    }
}