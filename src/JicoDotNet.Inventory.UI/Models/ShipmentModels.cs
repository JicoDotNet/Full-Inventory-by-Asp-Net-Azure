using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JicoDotNet.Inventory.UI.Models
{
    public class ShipmentModels
    {
        public ShipmentType _shipmentType { get; set; }
        public List<ShipmentType> _shipmentTypes { get; set; }

        public List<Shipment> _shipments { get; set; }
        public List<SalesOrder> _salesOrders { get; set; }
        public SalesOrder _salesOrder { get; set; }
        public Dictionary<bool, string> _YesNo { get; set; }
        public List<WareHouse> _wareHouses { get; set; }
        public CompanyBasic _company { get; set; }
        public Company _companyAddress { get; set; }

        public Branch _branch { get; set; }

        public Shipment _shipment { get; set; }
        public List<ShipmentDetail>  _shipmentDetails { get; set; }
        public List<Stock> _stocks { get; set; }
        public List<Product> _products { get; set; }

        public List<Customer> _customers { get; set; }
        public List<SalesType> _salesTypes { get; set; }
        public Config _config { get; set; }
    }
}