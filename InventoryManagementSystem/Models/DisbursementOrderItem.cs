using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Models
{
    class DisbursementOrderItem
    {
        [Key]
        public int DisbursementOrderItemId { get; set; }

        public int DisbursementOrderId { get; set; }
        public int ItemId { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Quantity { get; set; }

        // Navigation Properties
        public virtual DisbursementOrder DisbursementOrder { get; set; }
        public virtual Item Item { get; set; }

    }
}
