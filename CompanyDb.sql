create table Employees
(
    EmployeeId serial primary key,
	FirstName varchar(100),
	LastName varchar(100),
	DepartmentId int references Departments(DepartmentId),
	Position varchar(200),
	HireDate date
);

create table Departments
(
    DepartmentId serial primary key,
	DepartmentName varchar(300)
);

create table Salaries
(
    SalaryId serial primary key,
	EmployeeId int references Employees(EmployeeId),
    Amount numeric,
	PayrollDate date
);



