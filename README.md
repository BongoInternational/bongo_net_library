# bongo_net_library 
Bongo's Checkout .net Library

## Instructions
Example of use in C#:

1. We import the library to the project on "Reference".
2. Add  using "Vendor" (declare the reference in the classes).
3. Order Declaring variable in the same way.

```
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


  string urlLocation = client.go(order);
´´´
