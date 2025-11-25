SELECT "Id",
       "Symbol",
       "CompanyName",
       "Purchase",
       "LastDiv",
       "Industry",
       "MarketCap"
FROM public."Stock"
LIMIT 1000;

INSERT INTO public."Stock"
("Symbol", "CompanyName", "Purchase", "LastDiv", "Industry", "MarketCap")
VALUES
('AAPL', 'Apple Inc.', 175.30, 0.24, 'Technology', 2900000000000),
('MSFT', 'Microsoft Corporation', 315.20, 0.68, 'Technology', 2700000000000),
('AMZN', 'Amazon.com Inc.', 128.40, 0.00, 'E-Commerce', 1400000000000),
('GOOGL', 'Alphabet Inc.', 132.10, 0.00, 'Technology', 1800000000000),
('TSLA', 'Tesla Inc.', 250.55, 0.00, 'Automotive', 800000000000),
('NVDA', 'NVIDIA Corporation', 450.75, 0.04, 'Semiconductors', 1100000000000);
