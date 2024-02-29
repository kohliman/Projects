 SET SERVEROUTPUT ON
 
--Manmeet Singh Kohli
--991667681
--Database Management 

---q11.	Print the total amount, the average dollar value of service visits (parts and labour costs) and the number 
--of those visits for Mercedes and Jaguar car
--makes that are sold between August 2018 and December 2021 inclusive (5 marks).
--


--Ans1
select SUM(s.partscost + s.laborcost + (s.partscost + s.laborcost) * s.taxrate/100) AS Total_Cost
 ,AVG(s.partscost + s.laborcost)as Average_COST,COUNT((s.servinvno)) 
as "No of Visits" from ((SI.SERVINV s join SI.CAR c on s.custname=c.custname )
join SI.SaleINv sa on c.custname = sa.custname)
where c.carmake in ('MERCEDES' ,'JAGUAR')and sa.saledate BETWEEN TO_DATE('2018-08-01', 'YYYY-MM-DD') AND TO_DATE('2021-12-31', 'YYYY-MM-DD');



----q2	The S1 schema contains customers who have bought one or more vehicles. They can be classified using the following criteria:
--a.	Customers that have bought only one car (one-time buyer)
--b.	Customer that has bought two cars (two-time buyer)
--c.	Customers that have bought more than two cars (frequent buyers)
--Using a SINGLE SELECT statement, display a list of customers with their names and what type of buyer they are for all tho	se customers that have bought Mercedes or Acura cars. (5 marks)


--Ans2
       
WITH CarPurchases AS (
    SELECT 
        car.custname,
        COUNT(sale.saleinvno) as purchase_count
    FROM 
        si.car car
    JOIN 
        si.saleinv sale ON car.carserial = sale.carserial
    WHERE 
        car.carmake IN ('MERCEDES', 'ACURA')
    GROUP BY 
        car.custname
)

SELECT 
    cp.custname,
    CASE
        WHEN cp.purchase_count = 1 THEN 'one-time buyer'
        WHEN cp.purchase_count = 2 THEN 'Two-time buyer'
        ELSE 'Frequent buyer'
    END AS CategoryOfBuyer
FROM 
    CarPurchases cp
ORDER BY 
    cp.custname;




--Q3
--3.	Using SET operations, display a list 
--of customers that are interested in a car
--(prospect table) of the same make and model which they already own. (5 marks)

--Ans3


select c.custname as Names_0f_Customer from SI.prospect po join SI.car c on po.custname=c.custname where po.carmake=c.carmake And po.carmodel =c.carmodel
Intersect 
select p.custname as Names_Of_Customer from si.prospect p join si.car c on p.custname=c.custname where p.carmodel =c.carmodel and p.carmake=c.carmake;

---q4
--4.	Show a list of total amounts of money spend on the 
--labour cost of servicing Acura and Jaguar cars. Show the subtotals for each make and model (5 marks)
--


--Ams4
    select c.carmake ,c.carmodel,SUM(s.laborcost)as Total_labour_cost from SI.servinv s join SI.car c on s.custname=c.custname
    where c.carmake='JAGUAR' or c.carmake='ACURA'
    GROUP BY CUBE (c.carmake, c.carmodel)
    order by 1,2;


--SELECT SUM(s.laborcost),c.carmake, c.carmodel 
--FROM SI.servinv s 
--JOIN SI.car c ON s.custname = c.custname 
--WHERE c.carmake='MERCEDES' OR c.carmake='ACURA'
--GROUP BY ROLLUP(c.carmake, c.carmodel), s.servinvno;

----Q5
--5.Write a query using analytic functions that will show
--the serial number, the price of each Acura car as well as the cumulative sale price totals


--Ans5
--select c.carserial ,c.purchcost, SUM(si.carsaleprice) OVER(order by c.carserial)as Cummulaitve_cost 
--from SI.car c join SI.Saleinv si on c.custname =si.custname where c.carmake='ACURA' and c.purchcost is not null
--order by c.carserial;
--


    SELECT 
        car.carserial AS SerialNumber,
            car.carmake,
        sale.carsaleprice AS CarPrice,
        car.carlistprice,
        SUM(sale.carsaleprice) OVER (order by car.carserial) AS CumulativeSalePriceTotal
    FROM 
        si.car car
    JOIN 
        si.saleinv sale ON car.carserial = sale.carserial
    WHERE 
        car.carmake = 'ACURA'
    ORDER BY 
        car.carserial;
        



