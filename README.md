# indg.grip.trader

1. POST /api/v{version}/users - Anonymous. Add user with password to db;
2. POST /api/v{version}/auth/signin - Authentication by login and password. Returns bearer token;
3. POST /api/v{version}/products - Need to authorize. Add product for sale;
4. GET /api/v{version}/products - Anonymous. Get all available for sale products;
5. PUT /api/v{version}/products/buy/{productId} - Need to authorize. Buy product;
6. PUT /api/v{version}/products/send/{productId}/{shippingNumber} - Need to authorize. Saller can set  that product was shipped with shippinh number;
7. GET /api/v{version}/products/saled - Need to authorize. Get all products which user salled;
8. GET /api/v{version}/products/shipped - Need to authorize. Get all products which user sent;
9. GET /api/v{version}/products/bought - Need to authorize. Get all products which user bought;
