# Inventory Management System

This is a Windows Forms Inventory Management System built using .NET Framework 4.8 and Entity Framework Core 3.1 (Code First). The system supports functionalities such as managing warehouses, items, suppliers, customers, supply orders, disbursement orders, transfer orders, and generating reports.

---

## 📁 Project Structure

- **Forms/** – Windows Forms for data entry and reporting.
- **Models/** – Entity Framework entity classes representing database tables.
- **Migrations/** – Code-first EF Core migrations.
- **WarehouseDbContext.cs** – EF Core DbContext with model configurations.
- **App.config** – Configuration settings.
- **Program.cs** – Main entry point.

---

## 🧾 Forms Overview

### `Form1.cs` – Main Menu

This is the main navigation form, structured as a **MenuStrip** with three primary categories:

#### 1. **Masters**
Used to manage and create core entities:
- `WarehouseForm.cs` – Create/edit warehouses.
- `ItemsForm.cs` – Create/edit items.
- `SupplierForm.cs` – Create/edit suppliers.
- `CustomerForm.cs` – Create/edit customers.

#### 2. **Transactions**
Used to record item movements in the system:
- `SupplyOrderForm.cs` – Insert items into warehouse (from suppliers).
- `DismissOrderForm.cs` – Remove items from warehouse (to customers).
- `TransferForm.cs` – Transfer items between warehouses.

#### 3. **Reports**
Used to generate inventory and logistics reports:
- `WarehouseReportForm.cs` – Shows data and stock across all warehouses.
- `ItemReportForm.cs` – Shows detailed information about a specific item.
- `ItemMovementReportForm.cs` – Shows item flow (incoming/outgoing).
- `StockAgingReportForm.cs` – Displays aging report of stock batches.
- `ExpiringItemsReportForm.cs` – Highlights items nearing expiration.

---

## 📦 Models Overview

### `Item`
Represents an inventory item.

- `Id`, `Name`, `Description`, `Unit`, `Price`
- Navigation: SupplyOrderItems, DisbursementOrderItems, TransferOrderItems, WarehouseInventories

---

### `Customer`
Represents a customer.

- `Id`, `Name`, `Phone`, `Address`
- Navigation: DisbursementOrders

---

### `Supplier`
Represents a supplier.

- `Id`, `Name`, `Phone`, `Address`
- Navigation: SupplyOrders

---

### `Warehouse`
Represents a storage location.

- `Id`, `Name`, `Location`
- Navigation: SupplyOrders, DisbursementOrders, TransferOrdersFrom, TransferOrdersTo, WarehouseInventories

---

### `SupplyOrder`
Represents a supply transaction from a supplier.

- `Id`, `Date`, `SupplierId`, `WarehouseId`
- Navigation: Supplier, Warehouse, SupplyOrderItems

---

### `SupplyOrderItem`
Represents a line item in a supply order.

- `Id`, `SupplyOrderId`, `ItemId`, `Quantity`, `ProductionDate`, `ExpiryDate`

---

### `DisbursementOrder`
Represents an order to disburse items to a customer.

- `Id`, `Date`, `CustomerId`, `WarehouseId`
- Navigation: Customer, Warehouse, DisbursementOrderItems

---

### `DisbursementOrderItem`
Represents a line item in a disbursement order.

- `Id`, `DisbursementOrderId`, `ItemId`, `Quantity`

---

### `TransferOrder`
Represents the transfer of items between warehouses.

- `Id`, `Date`, `FromWarehouseId`, `ToWarehouseId`
- Navigation: FromWarehouse, ToWarehouse, TransferOrderItems

---

### `TransferOrderItem`
Represents a line item in a transfer order.

- `Id`, `TransferOrderId`, `ItemId`, `Quantity`

---

### `WarehouseInventory`
Tracks item quantities in warehouses.

- `Id`, `WarehouseId`, `ItemId`, `Quantity`, `ProductionDate`, `ExpiryDate`

---

## 🧠 Database Context

### `WarehouseDbContext.cs`
Entity Framework Core context managing all model `DbSet`s and configurations.

---

## ▶️ Getting Started

1. Open the solution in Visual Studio.
2. Make sure EF Core 3.1 is installed.
3. Run the application to manage inventory through forms and track all item flows.

---

## 📌 Notes

- Uses Code-First approach with EF Core.
- Designed with a modular and maintainable structure.
- Focuses on core inventory management functionality using local DB.
