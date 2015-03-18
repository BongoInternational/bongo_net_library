using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace Vendor
{
    public class Cli
    {
        static void Main(string[] args)
        {
            Hashtable settings = Cli.getSettings("settings.config");

            Order order = new Order();
            order.customerFirstname = "Christie";
            order.customerLastname = "Test";

            order.ship_first_name="Test";
            order.ship_last_name = "Test";
            order.ship_company = "Bongo";
            order.ship_address_line_1 = "Direccion";
            order.ship_address_line_2 = "Direccion";
            order.ship_city = "LI";
            order.ship_state = "LI";
            order.ship_zip = "Lima11";
            order.ship_country = "Peru";
            order.ship_phone="3875454";
            order.ship_email = "test@bongous.com";

            order.productId[0] = "testtigerx122";
            order.productName[0] = "Test Product";
            order.productPrice[0] = "1.00";
            order.productQ[0] = "1";


            Client client = new Client(
                settings["username"].ToString(),
                settings["password"].ToString(),
                settings["partnerKey"].ToString(),
                settings["urlBbs"].ToString(),
                settings["payLink"].ToString()
            );

            System.Console.Write("doing redirect\n");

            string urlLocation = client.go(order);

            // HttpContext.Current.Response.Redirect(urlLocation);

            System.Console.Write("\nredirect made successfully to " + urlLocation + "\n");
        }

        static Hashtable getSettings(string path)
        {
            Hashtable _ret = new Hashtable();
            if (File.Exists(path))
            {
                StreamReader reader = new StreamReader(
                    new FileStream(
                        path,
                        FileMode.Open,
                        FileAccess.Read,
                        FileShare.Read
                    )
                );
                XmlDocument doc = new XmlDocument();
                string xmlIn = reader.ReadToEnd();
                reader.Close();
                doc.LoadXml(xmlIn);
                foreach (XmlNode child in doc.ChildNodes)
                {
                    if (child.Name.Equals("Settings"))
                    {
                        foreach (XmlNode node in child.ChildNodes)
                        {
                            if (node.Name.Equals("add"))
                            {
                                _ret.Add(
                                    node.Attributes["key"].Value,
                                    node.Attributes["value"].Value
                                );
                            }
                        }
                    }
                }
            }

            return (_ret);
        }
    }
}