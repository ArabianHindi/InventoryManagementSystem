using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Models
{
    class TransferOrder
    {
        [Key]
        public int TransferOrderId { get; set; }

        [Required]
        public DateTime TransferDate { get; set; }

        public int FromWarehouseId { get; set; }
        public int ToWarehouseId { get; set; }

        public string Notes { get; set; }

        // Navigation Properties
        [ForeignKey("FromWarehouseId")]
        public virtual Warehouse FromWarehouse { get; set; }

        [ForeignKey("ToWarehouseId")]
        public virtual Warehouse ToWarehouse { get; set; }

        public virtual ICollection<TransferOrderItem> TransferOrderItems { get; set; } = new List<TransferOrderItem>();

    }
}
