using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Models
{
    class SupplyOrder
    {
        [Key]
        public int SupplyOrderId { get; set; }

        [Required]
        [MaxLength(50)]
        public string OrderNumber { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        public int WarehouseId { get; set; }
        public int SupplierId { get; set; }

        // Navigation Properties
        public virtual Warehouse Warehouse { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<SupplyOrderItem> SupplyOrderItems { get; set; } = new List<SupplyOrderItem>();

    }
}
