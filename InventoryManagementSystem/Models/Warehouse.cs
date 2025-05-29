using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Models
{
    class Warehouse
    {
        [Key]
        public int WarehouseId { get; set; }

        [Required]
        [MaxLength(100)]
        public string WarehouseName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [MaxLength(100)]
        public string PersonInCharge { get; set; }

        // Navigation Properties
        public virtual ICollection<SupplyOrder> SupplyOrders { get; set; } = new List<SupplyOrder>();
        public virtual ICollection<DisbursementOrder> DisbursementOrders { get; set; } = new List<DisbursementOrder>();
        public virtual ICollection<WarehouseInventory> WarehouseInventories { get; set; } = new List<WarehouseInventory>();
        public virtual ICollection<TransferOrder> TransferOrdersFrom { get; set; } = new List<TransferOrder>();
        public virtual ICollection<TransferOrder> TransferOrdersTo { get; set; } = new List<TransferOrder>();

    }
}
