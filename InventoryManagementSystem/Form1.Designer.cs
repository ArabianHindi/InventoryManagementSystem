namespace InventoryManagementSystem
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mastersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.warehousesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.suppliersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transactionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.supplyOrdersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dismissOrdersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transferItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.warehouseReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itemReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.movementReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oldItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expiringItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mastersToolStripMenuItem,
            this.transactionsToolStripMenuItem,
            this.reportsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mastersToolStripMenuItem
            // 
            this.mastersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.warehousesToolStripMenuItem,
            this.itemsToolStripMenuItem,
            this.suppliersToolStripMenuItem,
            this.customersToolStripMenuItem});
            this.mastersToolStripMenuItem.Name = "mastersToolStripMenuItem";
            this.mastersToolStripMenuItem.Size = new System.Drawing.Size(74, 24);
            this.mastersToolStripMenuItem.Text = "Masters";
            // 
            // warehousesToolStripMenuItem
            // 
            this.warehousesToolStripMenuItem.Name = "warehousesToolStripMenuItem";
            this.warehousesToolStripMenuItem.Size = new System.Drawing.Size(171, 26);
            this.warehousesToolStripMenuItem.Text = "Warehouses";
            this.warehousesToolStripMenuItem.Click += new System.EventHandler(this.warehousesToolStripMenuItem_Click);
            // 
            // itemsToolStripMenuItem
            // 
            this.itemsToolStripMenuItem.Name = "itemsToolStripMenuItem";
            this.itemsToolStripMenuItem.Size = new System.Drawing.Size(171, 26);
            this.itemsToolStripMenuItem.Text = "Items";
            this.itemsToolStripMenuItem.Click += new System.EventHandler(this.itemsToolStripMenuItem_Click);
            // 
            // suppliersToolStripMenuItem
            // 
            this.suppliersToolStripMenuItem.Name = "suppliersToolStripMenuItem";
            this.suppliersToolStripMenuItem.Size = new System.Drawing.Size(171, 26);
            this.suppliersToolStripMenuItem.Text = "Suppliers";
            this.suppliersToolStripMenuItem.Click += new System.EventHandler(this.suppliersToolStripMenuItem_Click);
            // 
            // customersToolStripMenuItem
            // 
            this.customersToolStripMenuItem.Name = "customersToolStripMenuItem";
            this.customersToolStripMenuItem.Size = new System.Drawing.Size(171, 26);
            this.customersToolStripMenuItem.Text = "Customers";
            this.customersToolStripMenuItem.Click += new System.EventHandler(this.customersToolStripMenuItem_Click);
            // 
            // transactionsToolStripMenuItem
            // 
            this.transactionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.supplyOrdersToolStripMenuItem,
            this.dismissOrdersToolStripMenuItem,
            this.transferItemsToolStripMenuItem});
            this.transactionsToolStripMenuItem.Name = "transactionsToolStripMenuItem";
            this.transactionsToolStripMenuItem.Size = new System.Drawing.Size(104, 24);
            this.transactionsToolStripMenuItem.Text = "Transactions";
            // 
            // supplyOrdersToolStripMenuItem
            // 
            this.supplyOrdersToolStripMenuItem.Name = "supplyOrdersToolStripMenuItem";
            this.supplyOrdersToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.supplyOrdersToolStripMenuItem.Text = "Supply Orders";
            this.supplyOrdersToolStripMenuItem.Click += new System.EventHandler(this.supplyOrdersToolStripMenuItem_Click);
            // 
            // dismissOrdersToolStripMenuItem
            // 
            this.dismissOrdersToolStripMenuItem.Name = "dismissOrdersToolStripMenuItem";
            this.dismissOrdersToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.dismissOrdersToolStripMenuItem.Text = "Dismiss Orders";
            this.dismissOrdersToolStripMenuItem.Click += new System.EventHandler(this.dismissOrdersToolStripMenuItem_Click);
            // 
            // transferItemsToolStripMenuItem
            // 
            this.transferItemsToolStripMenuItem.Name = "transferItemsToolStripMenuItem";
            this.transferItemsToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.transferItemsToolStripMenuItem.Text = "Transfer Items";
            // 
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.warehouseReportToolStripMenuItem,
            this.itemReportToolStripMenuItem,
            this.movementReportToolStripMenuItem,
            this.oldItemsToolStripMenuItem,
            this.expiringItemsToolStripMenuItem});
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            this.reportsToolStripMenuItem.Size = new System.Drawing.Size(74, 24);
            this.reportsToolStripMenuItem.Text = "Reports";
            // 
            // warehouseReportToolStripMenuItem
            // 
            this.warehouseReportToolStripMenuItem.Name = "warehouseReportToolStripMenuItem";
            this.warehouseReportToolStripMenuItem.Size = new System.Drawing.Size(214, 26);
            this.warehouseReportToolStripMenuItem.Text = "Warehouse Report";
            // 
            // itemReportToolStripMenuItem
            // 
            this.itemReportToolStripMenuItem.Name = "itemReportToolStripMenuItem";
            this.itemReportToolStripMenuItem.Size = new System.Drawing.Size(214, 26);
            this.itemReportToolStripMenuItem.Text = "Item Report";
            // 
            // movementReportToolStripMenuItem
            // 
            this.movementReportToolStripMenuItem.Name = "movementReportToolStripMenuItem";
            this.movementReportToolStripMenuItem.Size = new System.Drawing.Size(214, 26);
            this.movementReportToolStripMenuItem.Text = "Movement Report";
            // 
            // oldItemsToolStripMenuItem
            // 
            this.oldItemsToolStripMenuItem.Name = "oldItemsToolStripMenuItem";
            this.oldItemsToolStripMenuItem.Size = new System.Drawing.Size(214, 26);
            this.oldItemsToolStripMenuItem.Text = "Old Items";
            // 
            // expiringItemsToolStripMenuItem
            // 
            this.expiringItemsToolStripMenuItem.Name = "expiringItemsToolStripMenuItem";
            this.expiringItemsToolStripMenuItem.Size = new System.Drawing.Size(214, 26);
            this.expiringItemsToolStripMenuItem.Text = "Expiring Items";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mastersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem warehousesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem itemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem suppliersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transactionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem supplyOrdersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dismissOrdersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transferItemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem warehouseReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem itemReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem movementReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oldItemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem expiringItemsToolStripMenuItem;
    }
}

