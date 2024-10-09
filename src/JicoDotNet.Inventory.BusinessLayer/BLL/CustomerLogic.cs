using DataAccess.Sql;
using JicoDotNet.Authentication.Interfaces;
using JicoDotNet.Inventory.Core.Common;
using JicoDotNet.Inventory.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace JicoDotNet.Inventory.BusinessLayer.BLL
{
    public class CustomerLogic : DBManager
    {
        public CustomerLogic(ICommonLogicHelper CommonObj) : base(CommonObj) { }

        #region Customer Type
        public string TypeSet(CustomerType customerType)
        {
            _sqlDBAccess = new SqlDBAccess(CommonLogicObj.SqlConnectionString);
            string qt = string.Empty;
            if (customerType.CustomerTypeId > 0)
                qt = "UPDATE";
            else
                qt = "INSERT";

            NameValuePairs nvp = new NameValuePairs
            {


                new NameValuePair("@CustomerTypeId", customerType.CustomerTypeId),
                new NameValuePair("@CustomerTypeName", customerType.CustomerTypeName),
                new NameValuePair("@Description", customerType.Description),
                new NameValuePair("@RequestId", CommonLogicObj.RequestId),
                new NameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.DataManipulation(CommonLogicObj.SqlSchema + ".[spSetCustomerType]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public string TypeDeactive(string customerTypeId)
        {
            _sqlDBAccess = new SqlDBAccess(CommonLogicObj.SqlConnectionString);
            string qt = "INACTIVE";

            NameValuePairs nvp = new NameValuePairs
            {
                new NameValuePair("@CustomerTypeId", customerTypeId),

                new NameValuePair("@RequestId", CommonLogicObj.RequestId),
                new NameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.DataManipulation(CommonLogicObj.SqlSchema + ".[spSetCustomerType]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public List<CustomerType> TypeGet(bool? IsActive = null)
        {
            List<CustomerType> customerTypes = new SqlDBAccess(CommonLogicObj.SqlConnectionString).GetData(CommonLogicObj.SqlSchema + ".[spGetCustomerType]",
                new NameValuePairs
                {


                    new NameValuePair("@QueryType", "ALL")
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
            _sqlDBAccess = new SqlDBAccess(CommonLogicObj.SqlConnectionString);
            string qt = string.Empty;
            if (customer.CustomerId > 0)
                qt = "UPDATE";
            else
                qt = "INSERT";

            NameValuePairs nvp = new NameValuePairs
            {


                new NameValuePair("@CustomerId", customer.CustomerId),
                new NameValuePair("@CustomerTypeId", customer.CustomerTypeId),
                new NameValuePair("@CompanyName", customer.CompanyName),
                new NameValuePair("@CompanyType", customer.CompanyType),
                new NameValuePair("@StateCode", customer.StateCode),
                new NameValuePair("@IsGSTRegistered", customer.IsGSTRegistered),
                new NameValuePair("@GSTStateCode", customer.IsGSTRegistered ? (object)GenericLogic.GstStateCode(customer.GSTNumber) : DBNull.Value),
                new NameValuePair("@GSTNumber", customer.IsGSTRegistered ? (object)customer.GSTNumber?.ToUpper() : DBNull.Value),
                new NameValuePair("@PANNumber", customer.PANNumber?.ToUpper()),
                new NameValuePair("@ContactPerson", customer.ContactPerson),
                new NameValuePair("@Email", customer.Email),
                new NameValuePair("@Mobile", customer.Mobile),
                new NameValuePair("@IsRetailCustomer", customer.IsRetailCustomer),

                new NameValuePair("@RequestId", CommonLogicObj.RequestId),
                new NameValuePair("@QueryType", qt)
            };

            string ReturnDS = _sqlDBAccess.DataManipulation(CommonLogicObj.SqlSchema + ".[spSetCustomer]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public string Deactive(string customerId)
        {
            _sqlDBAccess = new SqlDBAccess(CommonLogicObj.SqlConnectionString);

            NameValuePairs nvp = new NameValuePairs
            {
                new NameValuePair("@CustomerId", customerId),


                new NameValuePair("@RequestId", CommonLogicObj.RequestId),
                new NameValuePair("@QueryType", "INACTIVE")
            };

            string ReturnDS = _sqlDBAccess.DataManipulation(CommonLogicObj.SqlSchema + ".[spSetCustomer]", nvp, "@OutParam").ToString();
            return ReturnDS;
        }

        public List<Customer> Get(bool? IsActive = null)
        {
            List<Customer> customers = new SqlDBAccess(CommonLogicObj.SqlConnectionString).GetData(CommonLogicObj.SqlSchema + ".[spGetCustomer]",
                new NameValuePairs
                {


                    new NameValuePair("@QueryType", "ALL")
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
            Customer customer = new SqlDBAccess(CommonLogicObj.SqlConnectionString).GetData(CommonLogicObj.SqlSchema + ".[spGetCustomer]",
                new NameValuePairs
                {


                    new NameValuePair("@CustomerId", CustomerId),
                    new NameValuePair("@QueryType", "SINGLE")
                }).FirstOrDefault<Customer>();
            if (IsActive != null)
            {
                return customer.IsActive == IsActive ? customer : null;
            }
            return customer;
        }
        public List<Customer> GetNonRetail(bool? IsActive = null)
        {
            List<Customer> customers = new SqlDBAccess(CommonLogicObj.SqlConnectionString).GetData(CommonLogicObj.SqlSchema + ".[spGetCustomer]",
                new NameValuePairs
                {


                    new NameValuePair("@QueryType", "NONRETAILALL")
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
            List<Customer> customers = new SqlDBAccess(CommonLogicObj.SqlConnectionString).GetData(CommonLogicObj.SqlSchema + ".[spGetCustomer]",
                new NameValuePairs
                {


                    new NameValuePair("@QueryType", "RETAILALL")
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