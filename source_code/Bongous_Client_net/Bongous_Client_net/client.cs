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
    public class Client
    {
        private string username;
        private string password;
        private string urlBbs;
        private string partnerKey;
        private string payLink;

        const bool RedirectRightAway = true;

        public Client(
            string username,
            string password,
            string partnerKey,
            string urlBbs,
            string payLink
        )
        {
            this.username = username;
            this.password = password;
            this.partnerKey = partnerKey;
            this.urlBbs = urlBbs;
            this.payLink = payLink;
        }

        public string go(Order order)
        {
            return string.Format(
                "Location: {0}/pay/{1}?TOKEN={2}",
                this.urlBbs,
                this.payLink,
                this.getTokenForOrder(order)
            );
        }

        public string getTokenForOrder(Order order)
        {
            // preparing payload
            string concatenatedBody = string.Empty;
            bool flagForFirst = true;
            Hashtable body = this.getOrderParams(order);
            foreach (DictionaryEntry part in body)
            {
                concatenatedBody = concatenatedBody + (flagForFirst ? string.Empty : "&") + part.Key.ToString() + "=" + part.Value.ToString();
                flagForFirst = false;
            }
            byte[] buffer = Encoding.GetEncoding("UTF-8").GetBytes(concatenatedBody);

            // creating request
            WebRequest request = WebRequest.Create(this.urlBbs + "/api/security/token");
            string authInfo = this.username + ":" + this.password;
            request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(authInfo));
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = buffer.Length;
            request.Method = "POST";

            // writes post payload
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(buffer, 0, buffer.Length);
            requestStream.Close();

            // reads response
            string json;
            var response = (HttpWebResponse)request.GetResponse();
            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                json = sr.ReadToEnd();
            }

            var serializer = new JavaScriptSerializer();
            var responseCollection = serializer.Deserialize<dynamic>(json);

            if (responseCollection.ContainsKey("error"))
            {
                if (responseCollection["error"] == 0)
                {
                    return responseCollection["token"];
                }
            }

            throw new Exception("Unable to get token");
        }

        public Hashtable getOrderParams(Order order)
        {
            order.partnerKey = this.partnerKey;
            Hashtable postValues = new Hashtable();
            postValues.Add("PARTNER_KEY", order.partnerKey);
            postValues.Add("CUST_FIRST_NAME", order.customerFirstname ?? string.Empty);
            postValues.Add("CUST_LAST_NAME", order.customerLastname ?? string.Empty);
            postValues.Add("CUST_COMPANY", order.customerCompany ?? string.Empty);
            postValues.Add("CUST_COUNTRY", order.customerCountry ?? string.Empty);
            postValues.Add("CUST_STATE", order.customerState ?? string.Empty);
            postValues.Add("CUST_STATE_COMBO", order.customerStateCombo ?? string.Empty);
            postValues.Add("CUST_ADDRESS_LINE_1", order.customerAddressLine1 ?? string.Empty);
            postValues.Add("CUST_ADDRESS_LINE_2", order.customerAddressLine2 ?? string.Empty);
            postValues.Add("CUST_CITY", order.customerCity ?? string.Empty);
            postValues.Add("CUST_ZIP", order.customerZip ?? string.Empty);
            postValues.Add("CUST_PHONE", order.customerPhone ?? string.Empty);
            postValues.Add("CUST_EMAIL", order.customerEmail ?? string.Empty);
            postValues.Add("TOTAL_DOMESTIC_SHIPPING_CHARGE", order.customerTotalDomesticShippingCharge ?? string.Empty);
            postValues.Add("ORDER_CURRENCY", order.orderCurrency ?? string.Empty);
            postValues.Add("ORDER_TRANSIT", order.orderTransit ?? string.Empty);
            postValues.Add("CUSTOM_ORDER_1", order.customOrder1 ?? string.Empty);
            postValues.Add("CUSTOM_ORDER_2", order.customOrder2 ?? string.Empty);
            postValues.Add("CUSTOM_ORDER_3", order.customOrder3 ?? string.Empty);
            
            postValues.Add("SHIP_FIRST_NAME",  order.ship_first_name ?? string.Empty);
            postValues.Add("SHIP_LAST_NAME",   order.ship_last_name ?? string.Empty);
            postValues.Add("SHIP_COMPANY",   order.ship_company ?? string.Empty);
            postValues.Add("SHIP_ADDRESS_LINE_1",   order.ship_address_line_1 ?? string.Empty);
            postValues.Add("SHIP_ADDRESS_LINE_2",  order.ship_address_line_2 ?? string.Empty);
            postValues.Add("SHIP_CITY",   order.ship_city ?? string.Empty);
            postValues.Add("SHIP_STATE",   order.ship_state ?? string.Empty);
            postValues.Add("SHIP_ZIP",  order.ship_zip ?? string.Empty);
            postValues.Add("SHIP_COUNTRY",  order.ship_country ?? string.Empty);
            postValues.Add("SHIP_PHONE", order.ship_phone ?? string.Empty);
            postValues.Add("SHIP_EMAIL",   order.ship_email ?? string.Empty);

            for (int i = 0; i < order.productId.Count; i++)
            {
                int productNumber = i + 1;
                postValues.Add("PRODUCT_ID_" + productNumber, order.productId[i] ?? string.Empty);
                postValues.Add("PRODUCT_NAME_" + productNumber, order.productName[i] ?? string.Empty);
                postValues.Add("PRODUCT_Q_" + productNumber, order.productQ[i] ?? string.Empty);
                postValues.Add("PRODUCT_PRICE_" + productNumber, order.productPrice[i] ?? string.Empty);
                postValues.Add("PRODUCT_SHIPPING_" + productNumber, order.productShipping[i] ?? string.Empty);
                postValues.Add("PRODUCT_CUSTOM_1_" + productNumber, order.productCustom1[i] ?? string.Empty);
                postValues.Add("PRODUCT_CUSTOM_2_" + productNumber, order.productCustom2[i] ?? string.Empty);
                postValues.Add("PRODUCT_CUSTOM_3_" + productNumber, order.productCustom3[i] ?? string.Empty);
                postValues.Add("PRODUCT_TRANSIT_" + productNumber, order.productTransit[i] ?? string.Empty);
                postValues.Add("PRODUCT_DISTRIBUTION_COUNTRY_" + productNumber, order.productDistributionCountry[i] ?? string.Empty);
                postValues.Add("PRODUCT_DISTCOUNTRY_ZIP_" + productNumber, order.productDistcountryZip[i] ?? string.Empty);
            }

            return postValues;
        }
    }
}