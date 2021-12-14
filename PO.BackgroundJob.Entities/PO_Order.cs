using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO.BackgroundJob.Entities
{
    public class PO_Order
    {
        public class POOrderRequestModel
        {
            public long Id { get; set; }
            public long? POGoodsTypeId { get; set; }
            public long? POVendorId { get; set; }
            public long? SupplierId { get; set; }
            public long? POStuffTypeId { get; set; }
            public long? PlantId { get; set; }
            public int POStatusId { get; set; }
            [MaxLength(10)]
            public string FlowRequest { get; set; }
            [MaxLength(10)]
            public string TypeRequest { get; set; }
            [MaxLength(10)]
            public string WarehouseReceived { get; set; }
            public DateTime OrderTime { get; set; }
            public DateTime? OrderTimeTo { get; set; }
            [MaxLength(50)]
            public string OrderBy { get; set; }

            [MaxLength(50)]
            public string CreatedBy { get; set; }
            public double? SumOrder { get; set; }
            [MaxLength(500)]
            public string Images { get; set; }
            public int? CreatedType { get; set; }
            public bool? OrderIsSale { get; set; }
            public bool? OrderIsBefore { get; set; }
            [MaxLength(50)]
            public string CosCenter { get; set; }
            public byte? SupplyType { get; set; }
            #region Extent protery check valid
            [Required]
            [Range(1, 3)]
            public int SystemOrderId { get; set; }
            #endregion
            #region Extent property AMS
            public int? TypeProductOrderId { get; set; }
            public int? RequestTypeId { get; set; }
            public int? WarehouseId { get; set; }
            public string Email { get; set; }
            public DateTime? DateOfIssue { get; set; }
            #endregion Extent property AMS


            [MaxLength(50)]
            public string OrderCode { get; set; }
            public bool? IsSAP { get; set; }

            #region Extent property AMS 
            #endregion Extent property AMS
            //public List<POOrderMaterialMapsViewModel> POOrderMaterialMaps { get; set; }
            //public List<ListDetailsId> DetailsIdDelete { get; set; }
        }
    }
}
