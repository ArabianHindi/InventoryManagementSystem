using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Models
{
    class Supplier
    {
        [Key]
        public int SupplierId { get; set; }

        [Required]
        [MaxLength(100)]
        public string SupplierName { get; set; }

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
        public virtual ICollection<SupplyOrder> SupplyOrders { get; set; } = new List<SupplyOrder>();
        public virtual ICollection<WarehouseInventory> WarehouseInventories { get; set; } = new List<WarehouseInventory>();
        public virtual ICollection<TransferOrderItem> TransferOrderItems { get; set; } = new List<TransferOrderItem>();

    }
}
