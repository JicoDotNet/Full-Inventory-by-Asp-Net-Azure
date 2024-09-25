using DataAccess.Sql;
using JicoDotNet.Inventory.BusinessLayer.Common;
using JicoDotNet.Inventory.BusinessLayer.DTO.Class;
using JicoDotNet.Inventory.BusinessLayer.DTO.SP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class CustomerLogic : ConnectionString
    {
        public CustomerLogic(sCommonDto CommonObj) : base(CommonObj) { }

        #region Customer Type
        public string TypeSet(CustomerType customerType)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt = string.Empty;
            if (customerType.CustomerTypeId > 0)
                qt = "UPDATE";
            else
                qt = "INSERT";

            nameValuePairs nvp = new nameValuePairs
            {
                 
                 
                new nameValuePair("@CustomerTypeId", customerType.CustomerTypeId),
                new nameValuePair("@CustomerTypeName", customerType.CustomerTypeName),
                new nameValuePair("@Description", customerType.Description),
                new nameValuePair("@RequestId", CommonObj.RequestId),
                new nameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetCustomerType]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public string TypeDeactive(string customerTypeId)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt = "INACTIVE";

            nameValuePairs nvp = new nameValuePairs
            {
                new nameValuePair("@CustomerTypeId", customerTypeId),
                 
                new nameValuePair("@RequestId", CommonObj.RequestId),
                new nameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetCustomerType]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public List<CustomerType> TypeGet(bool? IsActive = null)
        {
            List<CustomerType> customerTypes = new SqlDBAccess(CommonObj.SqlConnectionString).GetData("[dbo].[spGetCustomerType]",
                new nameValuePairs
                {
                     
                     
                    new nameValuePair("@QueryType", "ALL")
                }).ToList<CustomerType>();
            if (IsActive != null)
            {
                if (IsActive.Value)
                    return customerTypes.Where(a => a.IsActive).ToList();
                if (!IsActive.Value)
                    return customerTypes.Where(a => !a.IsActive).ToList();
            }
            return customerTypes;
        }
        #endregion

        #region Customer
        public string Set(Customer customer)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);
            string qt = string.Empty;
            if (customer.CustomerId > 0)
                qt = "UPDATE";
            else
                qt = "INSERT";

            nameValuePairs nvp = new nameValuePairs
            {
                 
                 
                new nameValuePair("@CustomerId", customer.CustomerId),
                new nameValuePair("@CustomerTypeId", customer.CustomerTypeId),
                new nameValuePair("@CompanyName", customer.CompanyName),
                new nameValuePair("@CompanyType", customer.CompanyType),
                new nameValuePair("@StateCode", customer.StateCode),
                new nameValuePair("@IsGSTRegistered", customer.IsGSTRegistered),
                new nameValuePair("@GSTStateCode", customer.IsGSTRegistered ? (object)GenericLogic.GstStateCode(customer.GSTNumber) : DBNull.Value),
                new nameValuePair("@GSTNumber", customer.IsGSTRegistered ? (object)customer.GSTNumber?.ToUpper() : DBNull.Value),
                new nameValuePair("@PANNumber", customer.PANNumber?.ToUpper()),
                new nameValuePair("@ContactPerson", customer.ContactPerson),
                new nameValuePair("@Email", customer.Email),
                new nameValuePair("@Mobile", customer.Mobile),
                new nameValuePair("@IsRetailCustomer", customer.IsRetailCustomer),

                new nameValuePair("@RequestId", CommonObj.RequestId),
                new nameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetCustomer]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public string Deactive(string customerId)
        {
            _sqlDBAccess = new SqlDBAccess(CommonObj.SqlConnectionString);

            nameValuePairs nvp = new nameValuePairs
            {
                new nameValuePair("@CustomerId", customerId),
                 
                 
                new nameValuePair("@RequestId", CommonObj.RequestId),
                new nameValuePair("@QueryType", "INACTIVE")
            };

            string ReturnDS = _sqlDBAccess.InsertUpdateDeleteReturnObject("[dbo].[spSetCustomer]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public List<Customer> Get(bool? IsActive = null)
        {
            List<Customer> customers = new SqlDBAccess(CommonObj.SqlConnectionString).GetData("[dbo].[spGetCustomer]",
                new nameValuePairs
                {
                     
                     
                    new nameValuePair("@QueryType", "ALL")
                }).ToList<Customer>();
            if (IsActive != null)
            {
                if (IsActive.Value)
                    return customers.Where(a => a.IsActive).ToList();
                if (!IsActive.Value)
                    return customers.Where(a => !a.IsActive).ToList();
            }
            return customers;
        }
        public Customer Get(long CustomerId, bool? IsActive = null)
        {
            Customer customer = new SqlDBAccess(CommonObj.SqlConnectionString).GetData("[dbo].[spGetCustomer]",
                new nameValuePairs
                {
                     
                     
                    new nameValuePair("@CustomerId", CustomerId),
                    new nameValuePair("@QueryType", "SINGLE")
                }).FirstOrDefault<Customer>();
            if (IsActive != null)
            {
                return customer.IsActive == IsActive ? customer : null;
            }
            return customer;
        }
        public List<Customer> GetNonRetail(bool? IsActive = null)
        {
            List<Customer> customers = new SqlDBAccess(CommonObj.SqlConnectionString).GetData("[dbo].[spGetCustomer]",
                new nameValuePairs
                {
                     
                     
                    new nameValuePair("@QueryType", "NONRETAILALL")
                }).ToList<Customer>();
            if (IsActive != null)
            {
                if (IsActive.Value)
                    return customers.Where(a => a.IsActive).ToList();
                if (!IsActive.Value)
                    return customers.Where(a => !a.IsActive).ToList();
            }
            return customers;
        }
        public List<Customer> GetRetail(bool? IsActive = null)
        {
            List<Customer> customers = new SqlDBAccess(CommonObj.SqlConnectionString).GetData("[dbo].[spGetCustomer]",
                new nameValuePairs
                {
                     
                     
                    new nameValuePair("@QueryType", "RETAILALL")
                }).ToList<Customer>();
            if (IsActive != null)
            {
                if (IsActive.Value)
                    return customers.Where(a => a.IsActive).ToList();
                if (!IsActive.Value)
                    return customers.Where(a => !a.IsActive).ToList();
            }
            return customers;
        }
        #endregion
    }
}