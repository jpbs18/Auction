DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'status_enum') THEN
        CREATE TYPE status_enum AS ENUM ('Live', 'Finished', 'ReservedNotMet');
    END IF;
END$$;

CREATE TABLE IF NOT EXISTS Auctions (
    Id UUID PRIMARY KEY,
    ReservePrice INT NOT NULL,
    Seller VARCHAR(100),
    Winner VARCHAR(100),
    SouldAmount INT,
    CurentHighBid INT,
    CreatedAt TIMESTAMP NOT NULL DEFAULT NOW(),
    UpdatedAt TIMESTAMP NOT NULL DEFAULT NOW(),
    AuctionEnd TIMESTAMP NOT NULL,
    Status status_enum NOT NULL
);

CREATE TABLE IF NOT EXISTS Items (
    Id UUID PRIMARY KEY,
    Make VARCHAR(50),
    Model VARCHAR(50),
    Year INT,
    Color VARCHAR(50),
    Mileage INT,
    ImageUrl VARCHAR(255),
    AuctionId UUID REFERENCES Auctions(Id) ON DELETE CASCADE
);
