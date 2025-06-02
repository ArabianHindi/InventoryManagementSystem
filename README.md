# Inventory Management System

This is a Windows Forms Inventory Management System built using .NET Framework 4.8 and Entity Framework Core 3.1 (Code First). The system supports functionalities such as managing warehouses, items, suppliers, customers, supply orders, disbursement orders, transfer orders, and various reports.

## Project Structure

The project contains the following major components:

- **Forms/**: UI forms for data entry and reporting.
- **Models/**: Entity Framework models representing the database schema.
- **Migrations/**: Code-first migration files.
- **WarehouseDbContext.cs**: Database context managing the entity sets and relationships.

---

## Models Overview

### `Item`
Represents an inventory item.

- `Id`
- `Name`
- `Description`
- `Unit`
- `Price`
- Navigation: `SupplyOrderItems`, `DisbursementOrderItems`, `TransferOrderItems`, `WarehouseInventories`

---

### `Customer`
Represents a customer receiving disbursed items.

- `Id`
- `Name`
- `Phone`
- `Address`
- Navigation: `DisbursementOrders`

---

### `Supplier`
Represents a supplier providing items.

- `Id`
- `Name`
- `Phone`
- `Address`
- Navigation: `SupplyOrders`

---

### `Warehouse`
Represents a warehouse storing items.

- `Id`
- `Name`
- `Location`
- Navigation: `SupplyOrders`, `DisbursementOrders`, `TransferOrdersFrom`, `TransferOrdersTo`, `WarehouseInventories`

---

### `SupplyOrder`
Represents a supply order made to a supplier.

- `Id`
- `Date`
- `SupplierId`
- `WarehouseId`
- Navigation: `Supplier`, `Warehouse`, `SupplyOrderItems`

---

### `SupplyOrderItem`
Represents an item in a supply order.

- `Id`
- `SupplyOrderId`
- `ItemId`
- `Quantity`
- `ProductionDate`
- `ExpiryDate`
- Navigation: `SupplyOrder`, `Item`

---

### `DisbursementOrder`
Represents an order to disburse items to a customer.

- `Id`
- `Date`
- `CustomerId`
- `WarehouseId`
- Navigation: `Customer`, `Warehouse`, `DisbursementOrderItems`

---

### `DisbursementOrderItem`
Represents an item in a disbursement order.

- `Id`
- `DisbursementOrderId`
- `ItemId`
- `Quantity`
- Navigation: `DisbursementOrder`, `Item`

---

### `TransferOrder`
Represents a transfer of items between warehouses.

- `Id`
- `Date`
- `FromWarehouseId`
- `ToWarehouseId`
- Navigation: `FromWarehouse`, `ToWarehouse`, `TransferOrderItems`

---

### `TransferOrderItem`
Represents an item in a transfer order.

- `Id`
- `TransferOrderId`
- `ItemId`
- `Quantity`
- Navigation: `TransferOrder`, `Item`

---

### `WarehouseInventory`
Represents the quantity of an item in a specific warehouse.

- `Id`
- `WarehouseId`
- `ItemId`
- `Quantity`
- `ProductionDate`
- `ExpiryDate`
- Navigation: `Warehouse`, `Item`

---

## Database Context

### `WarehouseDbContext`
Manages all the DbSets for the models above and configures relationships using Fluent API where needed.

---

## Getting Started

To run the project:

1. Open the solution in Visual Studio.
2. Ensure Entity Framework Core 3.1 is installed.
3. Run the application to interact with the forms and manage inventory data.

---

## Notes

- Designed for educational and demonstration purposes.
- CRUD operations and basic validations are handled through Windows Forms.
- Uses Code First Migrations for database setup and updates.

---

## License

This project is open source and available under the MIT License.
