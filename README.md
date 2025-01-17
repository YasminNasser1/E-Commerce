
# E-Commerce API Documentation

## Overview

This documentation provides details on the API endpoints available for managing the shopping cart and orders in the e-commerce application. The API is built using ASP.NET Web API and allows for managing cart operations and handling orders.

## API Endpoints

### Product Management

#### Add Product

**URL**: AddProducts
**METHOD**:'POST'
 **Description**: Adds a New product .
 **Body**
{
    "Name": "Sample Product",
    "Description": "This is an Sample product.",
    "PictureUrl": "http://example.com/picture.jpg",
    "Price": 10000.00,
    "Rate": 1,
    "Target": 1,  // assuming Target is an enum, replace with appropriate value
    "Color": "Blue",
    "Size": "L",
    "Discount": 0.20,
    "ProductBrandId": 1,
    "ProductTypeyId": 1
}

- **Responses**:
  - `200 OK`: Product added successfully.


///////////////////////////////////////////
#### Get Product by ID

- **URL**: `GetProductByID/ProductId`
- **Method**: `GET`
- **Description**: Retrieves specified Product.
- **Parameters**:
  - `ProductID` : The ID of the Product.
- **Responses**:
  - `200 OK`:
  - {
    "Carts": [],
    "Orders": [],
    "Name": "Sample Product",
    "Description": "This is an Sample product.",
    "PictureUrl": "http://example.com/picture.jpg",
    "Price": 10000,
    "Rate": 1,
    "Target": 1,
    "Color": "Blue",
    "Size": "L",
    "Discount": 0.2,
    "ProductBrandId": 1,
    "ProductBrand": null,
    "ProductTypeyId": 1,
    "ProductType": null,
    "Id": 4
}.
  - `404 Not Found`: No orders found for the specified user.
  - `500 Internal Server Error`: Error retrieving orders.


////////////////////////////////////////////////////////////////

######## Get All Products
**URL**: /products
**METHOD**:'Get'
 **Description**:Get All products .
 
- **Responses**:
  - `200 OK`:
  - [
    {
        "Carts": [],
        "Orders": [],
        "Name": "Sample Product",
        "Description": "This is an Sample product.",
        "PictureUrl": "http://example.com/picture.jpg",
        "Price": 10000,
        "Rate": 1,
        "Target": 1,
        "Color": "Blue",
        "Size": "L",
        "Discount": 0.2,
        "ProductBrandId": 1,
        "ProductBrand": null,
        "ProductTypeyId": 1,
        "ProductType": null,
        "Id": 3
    },
    {
        "Carts": [],
        "Orders": [],
        "Name": "Sample Product",
        "Description": "This is an Sample product.",
        "PictureUrl": "http://example.com/picture.jpg",
        "Price": 10000,
        "Rate": 1,
        "Target": 1,
        "Color": "Blue",
        "Size": "L",
        "Discount": 0.2,
        "ProductBrandId": 1,
        "ProductBrand": null,
        "ProductTypeyId": 1,
        "ProductType": null,
        "Id": 4
    }
    
    /////////////////////////////////////////////////////////////


#### Delete Product 

- **URL**: `Deletproduct/ProductId`
- **Method**: `DELETE`
- **Description**: Removes a product 
- **Parameters**:
  - `productId` (int): The ID of the product.
- **Responses**:
  - `200 OK`: Product removed successfully.
  /////////////////////////////////////
##### GetAllBrands

- **URL**:/Brands
- **Method**: `GET`
- **Description**: GETAllBrands 

- **Responses**:
    {
        "Name": "Zara",
        "products": [],
        "Id": 1
    },
    {
        "Name": "Defacto",
        "products": [],
        "Id": 2
    },
    {
        "Name": "LC",
        "products": [],
        "Id": 3
    }


/////////////////////////////////////////


#### Update Product

- **URL**: `/UpdateProducts/ProductID`
- **Method**: `PUT`
- **Description**: Updates the product .
**Body**
  {
    "Name": "Yasmine Product",
    "Description": "This is an example product.",
    "PictureUrl": "http://example.com/picture.jpg",
    "Price": 99.99,
    "Rate": 5,
    "Target": 1,  // assuming Target is an enum, replace with appropriate value
    "Color": "Red",
    "Size": "M",
    "Discount": 0.10,
    "ProductBrandId": 1,
    "ProductTypeyId": 1,
    "id":2

}
- **Responses**:
  - `200 OK`: Cart updated successfully.
  - `400 Bad Request`: Invalid quantity or error updating the cart.

////////////////////////////////////////////////////////////
###########GetAllProductByTypeID
/products?typeId=1
- **URL**: `/products?typeId={productId}`
- **Method**: `GET`


/////////////////////////////////
###########GetAllProductByBrandID

- **URL**: `/products?BrandId={productId}`
- **Method**: `GET`
- 
////////////////////////////////////////




### Cart Management

#### Add Product to Cart

- **URL**: `AddProductToCart/{userId}/{productId}`
- **Method**: `POST`
- **Description**: Adds a product to the user's cart with a specified quantity.
- **Param**: 
  ```json
  {
    "quantity": 2
  }
  ```
- **Parameters**:
  - `userId` (long): The ID of the user.
  - `productId` (int): The ID of the product.
  - `quantity` (int): The quantity of the product to add.
- **Responses**:
  - `200 OK`: Product added to cart successfully.
  - `400 Bad Request`: Invalid quantity or error adding product to the cart.

#### Update Cart

- **URL**: `UpdateCart/{cartId}/{productId}`
- **Method**: `PUT`
- **Description**: Updates the quantity of a product in the cart.
- - **Param**:  
  ```json
  {
    "quantity": 3
  }
  ```
- **Parameters**:
  - `cartId` (int): The ID of the cart.
  - `productId` (int): The ID of the product.
  - `quantity` (int): The new quantity of the product.
- **Responses**:
  - `200 OK`: Cart updated successfully.
  - `400 Bad Request`: Invalid quantity or error updating the cart.

#### Delete Product from Cart

- **URL**: `DeleteFromCart/{cartId}/{productId}`
- **Method**: `DELETE`
- **Description**: Removes a product from the user's cart.
- **Parameters**:
  - `cartId` (int): The ID of the cart.
  - `productId` (int): The ID of the product.
- **Responses**:
  - `200 OK`: Product removed from cart successfully.
  - `400 Bad Request`: Error removing product from the cart.

#### Checkout

- **URL**: `Checkout/{userId}`
- **Method**: `POST`
- **Description**: Checks out the user's cart and creates an order.
- - **Param**:  
  ```json
  {
    "address": "123 Main St, Anytown, USA"
  }
  ```
- **Parameters**:
  - `userId` (long): The ID of the user.
  - `address` (string): The shipping address for the order.
- **Responses**:
  - `200 OK`: Order placed successfully.
  - `400 Bad Request`: Error during checkout.

### Order Management

#### Get Orders by User

- **URL**: `/api/orders/{userId}`
- **Method**: `GET`
- **Description**: Retrieves all orders for a specified user.
- **Parameters**:
  - `userId` (long): The ID of the user.
- **Responses**:
  - `200 OK`: List of orders for the user.
  - `404 Not Found`: No orders found for the specified user.
  - `500 Internal Server Error`: Error retrieving orders.

#### Cancel Order

- **URL**: `/api/orders/cancel/{userId}/{OrderId}`
- **Method**: `PUT`
- **Description**: Cancels a specific order if its status is 'Pending'.
- **Parameters**:
  - `userId` (long): The ID of the user.
  - `OrderId` (int): The ID of the order to be canceled.
- **Responses**:
  - `200 OK`: Order canceled successfully.
  - `400 Bad Request`: Order cannot be canceled or does not belong to the user.
  - `500 Internal Server Error`: Error canceling the order.

## Error Handling

- **400 Bad Request**: The request was invalid or missing parameters.
- **404 Not Found**: The requested resource was not found.
- **500 Internal Server Error**: An error occurred on the server while processing the request.

## Logging and Debugging

Errors and exceptions are logged to the console. In a production environment, consider implementing a more robust logging mechanism.

## Authentication

still in dev

