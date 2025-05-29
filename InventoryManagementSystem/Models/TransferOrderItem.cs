using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Models
{
    class TransferOrderItem
    {
        [Key]
        public int TransferOrderItemId { get; set; }

        public int TransferOrderId { get; set; }
        public int ItemId { get; set; }
        public int SupplierId { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Quantity { get; set; }

        public DateTime? ProductionDate { get; set; }
        public DateTime? ExpiryDate { get; set; }

        // Navigation Properties
        public virtual TransferOrder TransferOrder { get; set; }
        public virtual Item Item { get; set; }
        public virtual Supplier Supplier { get; set; }

    }
}
