using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Models
{
    class DisbursementOrder
    {
        [Key]
        public int DisbursementOrderId { get; set; }

        [Required]
        [MaxLength(50)]
        public string OrderNumber { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        public int WarehouseId { get; set; }
        public int CustomerId { get; set; }

        // Navigation Properties
        public virtual Warehouse Warehouse { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<DisbursementOrderItem> DisbursementOrderItems { get; set; } = new List<DisbursementOrderItem>();

    }
}
