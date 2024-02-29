--Manmeet Singh Kohli
--991667681
--Zainab 


--•	Write a select statement that returns Product name, Customer first name, customer last name, Channel description, and 
--amount sold for all the sales. Make sure to returns
--sales even if there is no information on the customer, product, or channel



select prod_name as "Product Name" ,cust_first_name as "First name" ,cust_last_name as "Last Name" ,channel_desc as "Channel Description" ,s.amount_sold
from SH.products p left join Sh.sales s 
on p.prod_id =s.prod_id 
right join sh.customers c on s.cust_id=c.cust_id 
right join SH.channels  l on s.channel_id=l.channel_id;




--•	Write a select statement that returns channel ids that can be found in both sales and costs tables. do not repeat the redundant channel ids


select DISTINCT s.channel_id from SH.Sales s
INTERSECT
select DISTINCT c.channel_id from SH.Costs c


--•	Write a select statement that return the customer id, first name, last name, and customer income level for the customers who 
--live in the a city named 
--'Farmington'. Make sure to change income level   'K: 250,000 - 299,999' to 'KV: 250,000 - 299,999’ in the result set


select cust_id,cust_first_name,cust_last_name ,cust_income_level,   
CASE 
WHEN cust_income_level= 'K: 250,000 - 299,999'
THEN 'KV: 250,000 - 299,999'
ELSE cust_income_level
END As cust_income_level
From SH.customers where cust_city='Farmington';


--•	Write a select statement (only one statement) that returns the sum 
--of the sales amount sold for all the customers, for customers in each province, and for customers in each city


select cust_state_province,cust_city,SUM(amount_sold) from SH.sales s join SH.Customers c on s.cust_id=c.cust_id
GROUP BY
ROLLUP(cust_state_province ,cust_city)
;