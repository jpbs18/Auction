INSERT INTO Auctions (Id, ReservePrice, Seller, Winner, SouldAmount, CurentHighBid, CreatedAt, UpdatedAt, AuctionEnd, Status)
VALUES
('11111111-1111-1111-1111-111111111111', 100, 'Alice', NULL, NULL, NULL, NOW(), NOW(), NOW() + interval '7 days', 'Live'),
('22222222-2222-2222-2222-222222222222', 200, 'Bob', NULL, NULL, NULL, NOW(), NOW(), NOW() + interval '10 days', 'Live');

INSERT INTO Items (Id, Make, Model, Year, Color, Mileage, ImageUrl, AuctionId)
VALUES
('aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa', 'Toyota', 'Corolla', 2015, 'Red', 50000, 'https://example.com/car1.jpg', '11111111-1111-1111-1111-111111111111'),
('bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb', 'Honda', 'Civic', 2018, 'Blue', 30000, 'https://example.com/car2.jpg', '22222222-2222-2222-2222-222222222222');

