using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwoTableAPI.Models
{
    public partial class PaymentDetails
    {
        public PaymentDetails()
        {
            PaymentHistory = new HashSet<PaymentHistory>();
        }

        [Key]
        [Column("PMId")]
        public int Pmid { get; set; }
        [Required]
        [StringLength(100)]
        public string CardOwnerName { get; set; }
        [Required]
        [StringLength(16)]
        public string CardNumber { get; set; }
        [Required]
        [StringLength(5)]
        public string ExpirationDate { get; set; }
        [Required]
        [Column("CVV")]
        [StringLength(3)]
        public string Cvv { get; set; }

        [ForeignKey("Pmid")]
        [InverseProperty("InversePm")]
        public PaymentDetails Pm { get; set; }
        [InverseProperty("Pm")]
        public PaymentDetails InversePm { get; set; }
        [InverseProperty("PaymentDetailIdForeignNavigation")]
        public ICollection<PaymentHistory> PaymentHistory { get; set; }
    }
}
