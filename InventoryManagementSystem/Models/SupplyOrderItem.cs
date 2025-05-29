using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace InventoryManagementSystem.Models
{
    class SupplyOrderItem
    {
        [Key]
        public int SupplyOrderItemId { get; set; }

        public int SupplyOrderId { get; set; }
        public int ItemId { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Quantity { get; set; }

        public DateTime? ProductionDate { get; set; }
        public DateTime? ExpiryDate { get; set; }

        // Navigation Properties
        public virtual SupplyOrder SupplyOrder { get; set; }
        public virtual Item Item { get; set; }

    }
}
