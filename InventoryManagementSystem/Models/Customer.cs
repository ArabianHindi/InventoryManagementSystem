using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Models
{
    class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [MaxLength(100)]
        public string CustomerName { get; set; }

        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [MaxLength(20)]
        public string Fax { get; set; }

        [MaxLength(20)]
        public string MobileNumber { get; set; }

        [MaxLength(100)]
        public string EmailAddress { get; set; }

        [MaxLength(100)]
        public string Website { get; set; }

        // Navigation Properties
        public virtual ICollection<DisbursementOrder> DisbursementOrders { get; set; } = new List<DisbursementOrder>();

    }
}
