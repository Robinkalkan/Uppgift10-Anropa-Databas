SELECT 
    p.ProductName AS ProductName, 
    p.UnitPrice AS Price, 
    c.CategoryName AS CategoryName
FROM Products p
JOIN Categories c ON p.CategoryID = c.CategoryID
ORDER BY c.CategoryName, p.ProductName;

SELECT 
    c.CompanyName AS CustomerName, 
    COUNT(o.OrderID) AS OrderCount
FROM Customers c
LEFT JOIN Orders o ON c.CustomerID = o.CustomerID
GROUP BY c.CompanyName
ORDER BY OrderCount DESC;

SELECT 
    e.EmployeeID AS EmployeeID,
    e.FirstName AS FirstName,
    e.LastName AS LastName,
    t.TerritoryDescription AS TerritoryDescription
FROM Employees e
JOIN EmployeeTerritories et ON e.EmployeeID = et.EmployeeID
JOIN Territories t ON et.TerritoryID = t.TerritoryID;