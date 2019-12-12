using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwoTableAPI.Models
{
    public partial class PaymentHistory
    {
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(5)]
        public string Date { get; set; }
        public double Amount { get; set; }
        [Column("PaymentDetail_ID_Foreign")]
        public int PaymentDetailIdForeign { get; set; }

        [ForeignKey("PaymentDetailIdForeign")]
        [InverseProperty("PaymentHistory")]
        public PaymentDetails PaymentDetailIdForeignNavigation { get; set; }
    }
}
