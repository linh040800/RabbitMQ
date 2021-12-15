using System;

namespace PO.BackgroundJob.Entities
{
    public class PO_OrdersDto
    {
        public string Id { get; set; }
        public string POGoodsTypeId { get; set; }
        public string POVendorId { get; set; }
        public string SupplierId { get; set; }
        public string POStuffTypeId { get; set; }
        public string PlantId { get; set; }
        public int POStatusId { get; set; }
        public string FlowRequest { get; set; }
        public string TypeRequest { get; set; }
        public string WarehouseReceived { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime? OrderTimeTo { get; set; }
        public string OrderBy { get; set; }

        public string CreatedBy { get; set; }
        public double? SumOrder { get; set; }
        public string Images { get; set; }
        public int? CreatedType { get; set; }
        public bool? OrderIsSale { get; set; }
        public bool? OrderIsBefore { get; set; }
        public string CosCenter { get; set; }
        public byte? SupplyType { get; set; }
        #region Extent protery check valid
        public int SystemOrderId { get; set; }
        #endregion
        #region Extent property AMS
        public int? TypeProductOrderId { get; set; }
        public int? RequestTypeId { get; set; }
        public int? WarehouseId { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfIssue { get; set; }
        #endregion Extent property AMS

        public string OrderCode { get; set; }
        public bool? IsSAP { get; set; }

        #region Extent property AMS 
        #endregion Extent property AMS
    }
}
