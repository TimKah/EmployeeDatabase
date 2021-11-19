
CREATE TABLE employees (
	uuid UUID PRIMARY KEY,
    employeeid INT NOT NULL,
    employeename VARCHAR(128) NOT NULL,
    employeesalary INT NOT NULL,
    existencestartutc TIMESTAMP NOT NULL,
    existenceendutc TIMESTAMP NULL
);
