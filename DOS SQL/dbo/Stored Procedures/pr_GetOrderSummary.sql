CREATE PROCEDURE [dbo].[pr_GetOrderSummary]

	@StartDate  DATETIME,
	@EndDate DATETIME,
	@EmployeeID int = NULL,
	@CustomerID nvarchar(5) = NULL
AS

SELECT 
	(SELECT CONCAT(TitleOfCourtesy + ' ', FirstName + ' ', LastName) FROM Employees WHERE EmployeeID = MAX(O.EmployeeID)) AS [EmployeeFullName],
	(SELECT CompanyName FROM Shippers WHERE ShipperID = MAX(O.ShipVia)) AS [Shipper CompanyName],
	MAX(C.CompanyName) AS [Customer CompanyName],
	COUNT(O.OrderID) AS [NumberOfOders],
	MAX(O.OrderDate) AS [Date],
	CONCAT('R', CAST(SUM(O.Freight) AS smallmoney)) AS [TotalFreightCost],
	(SELECT COUNT(ProductID) FROM [Order Details] WHERE OrderID = MAX(O.OrderID) GROUP BY OrderID) AS [NumberOfDifferentProducts],
	(SELECT CONCAT('R',CAST(SUM((UnitPrice * Quantity) * (1 - Discount)) AS smallmoney)) FROM [Order Details] WHERE OrderID = Max(O.OrderID) GROUP BY OrderID) AS [TotalOrderValue]
FROM Orders O
	JOIN Employees E ON O.EmployeeID = E.EmployeeID
	JOIN Customers C on O.CustomerID = C.CustomerID		
WHERE 
	(@CustomerID is null or O.CustomerID = @CustomerID) AND
	(@EmployeeID is null or O.EmployeeID = @EmployeeID) AND
	O.OrderDate >= @StartDate AND 
	O.OrderDate <= @EndDate
GROUP BY 
	O.OrderDate,
	O.EmployeeID,
	O.CustomerID,
	O.ShipName

