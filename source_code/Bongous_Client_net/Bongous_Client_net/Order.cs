using System;
using System.Collections;
using System.Web;
using System.Net;
using System.Xml;
using System.Text;
using System.IO;
using System.Web.Script;
using System.Web.Script.Serialization;
using System.Collections.Generic;


namespace Vendor
{
    public class Order
    {
        public string partnerKey;
        public string customerFirstname;
        public string customerLastname;
        public string customerCompany;
        public string customerCountry;
        public string customerState;
        public string customerStateCombo;
        public string customerAddressLine1;
        public string customerAddressLine2;
        public string customerCity;
        public string customerZip;
        public string customerPhone;
        public string customerEmail;
        public string customerTotalDomesticShippingCharge;
        public string orderCurrency;
        public string orderTransit;
        public string customOrder1;
        public string customOrder2;
        public string customOrder3;

        public string ship_first_name;
        public string ship_last_name;
        public string ship_company;
        public string ship_address_line_1;
        public string ship_address_line_2;
        public string ship_city;
        public string ship_state;     
        public string ship_zip;               	
 	    public string ship_country;
 	    public string ship_phone;
        public string ship_email;


        public ArrayList productId = new ArrayList();
        public ArrayList productName = new ArrayList();
        public ArrayList productQ = new ArrayList();
        public ArrayList productPrice = new ArrayList();
        public ArrayList productShipping = new ArrayList();
        public ArrayList productCustom1 = new ArrayList();
        public ArrayList productCustom2 = new ArrayList();
        public ArrayList productCustom3 = new ArrayList();
        public ArrayList productTransit = new ArrayList();
        public ArrayList productDistributionCountry = new ArrayList();
        public ArrayList productDistcountryZip = new ArrayList();
    }
}
