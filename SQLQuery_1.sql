use HR;

Select * from COUNTRIES where COUNTRY_NAME IN('Brazil', 'Belgium');
SELECT salary from EMPLOYEES where FIRST_NAME+LAST_NAME='AmitBanda';

SELECT DIFFERENCE(MAX_SALARY,MIN_SALARY) as DIFFERENCE from jobs where job_title='President';

SELECT * from JOBS;

select first_name,employee_id from EMPLOYEES where FIRST_NAME+LAST_NAME !='DavidBernstein' and FIRST_NAME+LAST_NAME !='BruceErnst';
SELECT * from EMPLOYEES where year(HIRE_DATE)='2007';

    select round(12.656,0);

    select datepart(year,sysdatetime());
select floor(3.7);

select len(concat(first_name,' ',last_name)) from EMPLOYEES
where first_name='Steven';
select ltrim('    Amandeep');
select concat(trim('   amandeep   '),'kaur')
select substring(first_name,1,5) from employees;

SELECT first_name , month(hire_date) as Month ,avg(salary) from EMPLOYEES where year(HIRE_DATE) BETWEEN '2006' and '2007' GROUP by first_name,month(HIRE_DATE);

SELECT
    YEAR(hire_date) AS Year,
    MONTH(hire_date) AS Month,
    COUNT(*) AS NumEmployees,
    AVG(salary) AS AvgSalary
FROM
    employees
WHERE
    YEAR(hire_date) BETWEEN 2006 AND 2007
GROUP BY
    YEAR(hire_date),
    MONTH(hire_date)
ORDER BY
    YEAR(hire_date),
    MONTH(hire_date);

