using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Models
{
    class WarehouseInventory
    {
        [Key]
        public int InventoryId { get; set; }

        public int WarehouseId { get; set; }
        public int ItemId { get; set; }
        public int SupplierId { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal CurrentQuantity { get; set; } = 0;

        public DateTime? ProductionDate { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public DateTime LastUpdated { get; set; } = DateTime.Now;

        // Navigation Properties
        public virtual Warehouse Warehouse { get; set; }
        public virtual Item Item { get; set; }
        public virtual Supplier Supplier { get; set; }

    }
}
