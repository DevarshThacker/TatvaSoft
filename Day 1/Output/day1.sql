-- Create New Table
CREATE TABLE customer1 (
   customer_id serial PRIMARY KEY,
   first_name character varying(100) NOT NULL,
   last_name character varying(100) NOT NULL,
   email character varying(255) UNIQUE NOT NULL,
   created_date timestamp with time zone NOT NULL DEFAULT now(),
   updated_date timestamp with time zone
);

-- Drop Table
DROP TABLE IF EXISTS customer;

-- Add New Column
ALTER TABLE customer ADD COLUMN active boolean;

-- Drop New Column
ALTER TABLE customer DROP COLUMN active;

-- After Delete Column - Select Statement
SELECT * from customer

-- Rename Existing Column
ALTER TABLE customer RENAME COLUMN email_address TO email;

-- After Rename Column - Select Statement
SELECT * from customer1
ALTER TABLE customer RENAME COLUMN email_address TO email;

-- Rename table name
ALTER TABLE customer RENAME TO users;

ALTER TABLE users RENAME TO customer;

-- Create New Orders Table
CREATE TABLE orders2 (
    order_id SERIAL PRIMARY KEY,
    customer_id INTEGER NOT NULL REFERENCES customer1(customer_id),
    order_date timestamp with time zone NOT NULL DEFAULT now(),
    order_number CHARACTER VARYING(50) NOT NULL,
    order_amount DECIMAL(10,2) NOT NULL
);

-- Insert Single Customer Record
INSERT INTO customer1(first_name, last_name, email, created_date, updated_date)
	VALUES ('Hiren','Patel','hiren.parejiya@tatvasoft.com',now(),NULL);

-- Insert Multiple Customer Records	
INSERT INTO customer1 (first_name, last_name, email, created_date, updated_date) VALUES
  ('John', 'Doe', 'johndoe@example.com', NOW(), NULL),
  ('Alice', 'Smith', 'alicesmith@example.com', NOW(), NULL),
  ('Bob', 'Johnson', 'bjohnson@example.com', NOW(), NULL),
  ('Emma', 'Brown', 'emmabrown@example.com', NOW(), NULL),
  ('Michael', 'Lee', 'michaellee@example.com', NOW(), NULL),
  ('Sarah', 'Wilson', 'sarahwilson@example.com', NOW(), NULL),
  ('David', 'Clark', 'davidclark@example.com', NOW(), NULL),
  ('Olivia', 'Martinez', 'oliviamartinez@example.com', NOW(), NULL),
  ('James', 'Garcia', 'jamesgarcia@example.com', NOW(), NULL),
  ('Sophia', 'Lopez', 'sophialopez@example.com', NOW(), NULL),
  ('Jennifer', 'Davis', 'jennifer.davis@example.com', NOW(), NULL),
  ('Jennie', 'Terry', 'jennie.terry@example.com', NOW(), NULL),
  ('JENNY', 'SMITH', 'jenny.smith@example.com', NOW(), NULL),
  ('Hiren', 'Patel', 'hirenpatel@example.com', NOW(), NULL);

--Insert Customer Order Records
INSERT INTO orders2 (customer_id, order_date, order_number, order_amount) VALUES
  (1, '2024-01-01', 'ORD001', 50.00),
  (2, '2024-01-01', 'ORD002', 35.75),
  (3, '2024-01-01', 'ORD003', 100.00),
  (4, '2024-01-01', 'ORD004', 30.25),
  (5, '2024-01-01', 'ORD005', 90.75),
  (6, '2024-01-01', 'ORD006', 25.50),
  (7, '2024-01-01', 'ORD007', 60.00),
  (8, '2024-01-01', 'ORD008', 42.00),
  (9, '2024-01-01', 'ORD009', 120.25),
  (10,'2024-01-01', 'ORD010', 85.00),
  (1, '2024-01-02', 'ORD011', 55.00),
  (1, '2024-01-03', 'ORD012', 80.25),
  (2, '2024-01-03', 'ORD013', 70.00),
  (3, '2024-01-04', 'ORD014', 45.00),
  (1, '2024-01-05', 'ORD015', 95.50),
  (2, '2024-01-05', 'ORD016', 27.50),
  (2, '2024-01-07', 'ORD017', 65.75),
  (2, '2024-01-10', 'ORD018', 75.50);

-- Select Statement
SELECT customer_id, first_name, last_name, email, created_date, updated_date FROM customer1;

-- Update Statement
UPDATE customer1
SET first_name='Hiren',
last_name='Parejiya', 
email='hiren.parejiya@tatvasoft.com'
WHERE customer_id = 1;

-- Delete Statement
DELETE FROM customer1
WHERE customer_id = 11;

SELECT first_name FROM customer1;

SELECT
   first_name,
   last_name,
   email
FROM
   customer1;

SELECT * FROM customer1;

SELECT
	first_name,
	last_name
FROM
	customer1
ORDER BY
	first_name ASC;

SELECT
       first_name,
       last_name
FROM
       customer1
ORDER BY
       last_name DESC;

SELECT
	customer_id,
	first_name,
	last_name
FROM
	customer1
ORDER BY
	first_name ASC,
	last_name DESC;

SELECT
	last_name,
	first_name
FROM
	customer1
WHERE
	first_name = 'Hiren';


SELECT
	customer_id,
	first_name,
	last_name
FROM
	customer1
WHERE
	first_name = 'Hiren' AND last_name = 'Parejiya';


SELECT
	customer_id,
	first_name,
	last_name
FROM
	customer1
WHERE first_name IN ('John','David','James');

SELECT
	first_name,
    last_name
FROM
	customer1
WHERE
	first_name LIKE '%EN%';

SELECT * FROM orders2 as o
INNER JOIN customer1 as c
ON o.customer_id = c.customer_id

select * FROM customer1 as c
LEFT JOIN orders2 as o
ON c.customer_id = o.customer_id

SELECT
	c.customer_id,
	c.first_name,
	c.last_name,
	c.email,
	COUNT (o.order_id) AS "NoOrders",
	SUM(o.order_amount) AS "Total"
FROM customer1 as c
INNER JOIN orders2 as o
	ON c.customer_id = o.customer_id
GROUP BY c.customer_id

SELECT
	c.customer_id,
	c.first_name,
	c.last_name,
	c.email,
	COUNT (o.order_id) AS "No_Orders",
	SUM(o.order_amount) AS "Total"
FROM customer1 as c
INNER JOIN orders2 as o
	ON c.customer_id = o.customer_id
GROUP BY c.customer_id
HAVING COUNT (o.order_id) > 1

SELECT * from orders2
where customer_id IN (select customer_id from customer1 )

SELECT
    customer_id,
	first_name,
	last_name,
	email
FROM
	customer1
WHERE
	EXISTS (
		SELECT
			1
		FROM
			orders2
		WHERE
			orders2.customer_id = customer1.customer_id
	);