using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GameSystem
{
    /// <summary>
    /// Interaction logic for InventoryItemUpdate.xaml
    /// </summary>
    public partial class InventoryItemUpdate : Window
    {
        private int playerCharacterID;
        private string itemName;
        private int quantity;
        private int defaultAmount;
        private int quantityIncrease = 1;
        private CharacterInventoryManager _inventoryManager = new CharacterInventoryManager();
        private int inventoryItemID;
        private string itemType;
        private UpdateMode updateMode;

        public InventoryItemUpdate(int playerCharacterID, string itemName, int inventoryItemID,
                                    int quantity, int defaultAmount, string itemType, UpdateMode updateMode)
        {
            this.playerCharacterID = playerCharacterID;
            this.itemName = itemName;
            this.inventoryItemID = inventoryItemID;
            this.quantity = quantity;
            this.defaultAmount = defaultAmount;
            this.itemType = itemType;
            this.updateMode = updateMode;
            InitializeComponent();
        }

        public InventoryItemUpdate(int playerCharacterID, string itemName, int inventoryItemID,
            string itemType, UpdateMode updateMode)
        {
            this.playerCharacterID = playerCharacterID;
            this.itemName = itemName;
            this.inventoryItemID = inventoryItemID;
            this.quantity = 1;
            this.defaultAmount = 0;
            this.itemType = itemType;
            this.updateMode = updateMode;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (updateMode == UpdateMode.Delete)
            {
                defaultAmount = quantity;
                if (itemName.EndsWith("s"))
                {
                    txtDeleteInfo.Text = "How many " + itemName + "' would you like to delete?";
                }
                else
                {
                    txtDeleteInfo.Text = "How many " + itemName + "'s would you like to delete?";
                }
                btnDelete.Content = "Delete";
                txtQuantity.Text = quantity.ToString();
                btnIncrease.IsEnabled = false;
            }
            else
            {
                if (itemName.EndsWith("s"))
                {
                    txtDeleteInfo.Text = "How many " + itemName + "' would you like to add to your inventory?";
                }
                else
                {
                    txtDeleteInfo.Text = "How many " + itemName + "'s would you like to add to your inventory?";
                }
                btnDelete.Content = "Add";
                txtQuantity.Text = quantityIncrease.ToString();
                btnDecrease.IsEnabled = false;
            }

        }

        private void btnIncrease_Click(object sender, RoutedEventArgs e)
        {
            if (updateMode == UpdateMode.Delete)
            {
                if (quantity != defaultAmount)
                {
                    quantity++;
                    txtQuantity.Text = quantity.ToString();
                    if (quantity > 1)
                    {
                        btnDecrease.IsEnabled = true;
                    }
                }
                if (quantity == defaultAmount)
                {
                    btnIncrease.IsEnabled = false;
                }
            }
            else
            {
                quantityIncrease++;
                txtQuantity.Text = quantityIncrease.ToString();
                if (quantity != defaultAmount)
                {
                    btnDecrease.IsEnabled = true;
                }
            }

        }

        private void btnDecrease_Click(object sender, RoutedEventArgs e)
        {
            if (updateMode == UpdateMode.Delete)
            {
                if (quantity <= defaultAmount)
                {
                    quantity--;
                    txtQuantity.Text = quantity.ToString();
                    if (quantity < defaultAmount)
                    {
                        btnIncrease.IsEnabled = true;
                    }
                }
                if (quantity == 1)
                {
                    btnDecrease.IsEnabled = false;
                }
            }
            else
            {
                if (quantity != defaultAmount)
                {
                    quantityIncrease--;
                    txtQuantity.Text = quantityIncrease.ToString();
                }
                if (quantityIncrease == defaultAmount)
                {
                    btnDecrease.IsEnabled = false;
                }
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (updateMode == UpdateMode.Delete)
            {
                var newQuantity = defaultAmount - quantity;
                if (newQuantity == 0)
                {
                    if (itemType == "weapon")
                    {
                        _inventoryManager.DeleteWeapon(playerCharacterID, inventoryItemID);
                    }
                    else if (itemType == "equipment")
                    {
                        _inventoryManager.DeleteEquipment(playerCharacterID, inventoryItemID);
                    }
                    else
                    {
                        _inventoryManager.DeleteItem(playerCharacterID, inventoryItemID);
                    }
                }
                else
                {
                    if (itemType == "weapon")
                    {
                        _inventoryManager.UpdateWeapon(playerCharacterID, inventoryItemID, defaultAmount, newQuantity);
                    }
                    else if (itemType == "equipment")
                    {
                        _inventoryManager.UpdateEquipment(playerCharacterID, inventoryItemID, defaultAmount, newQuantity);
                    }
                    else
                    {
                        _inventoryManager.UpdateItem(playerCharacterID, inventoryItemID, defaultAmount, newQuantity);
                    }
                }
                if (quantity > 1)
                {
                    if (itemName.EndsWith("s"))
                    {
                        MessageBox.Show("Deleted " + quantity + " " + itemName + "'.");
                    }
                    else
                    {
                        MessageBox.Show("Deleted " + quantity + " " + itemName + "'s.");
                    }
                }
                else
                {
                    MessageBox.Show("Deleted " + quantity + " " + itemName + ".");
                }
            }
            else
            {
                var newQuantity = defaultAmount + quantityIncrease;
                if (defaultAmount == 0)
                {
                    if (itemType == "weapon")
                    {
                        _inventoryManager.CreateWeapon(playerCharacterID, inventoryItemID, newQuantity);
                    }
                    else if (itemType == "equipment")
                    {
                        _inventoryManager.CreateEquipment(playerCharacterID, inventoryItemID, newQuantity);
                    }
                    else
                    {
                        _inventoryManager.CreateItem(playerCharacterID, inventoryItemID, newQuantity);
                    }
                }
                else
                {
                    if (itemType == "weapon")
                    {
                        _inventoryManager.UpdateWeapon(playerCharacterID, inventoryItemID, defaultAmount, newQuantity);
                    }
                    else if (itemType == "equipment")
                    {
                        _inventoryManager.UpdateEquipment(playerCharacterID, inventoryItemID, defaultAmount, newQuantity);
                    }
                    else
                    {
                        _inventoryManager.UpdateItem(playerCharacterID, inventoryItemID, defaultAmount, newQuantity);
                    }
                }

                
                if (quantityIncrease > 1)
                {
                    if (itemName.EndsWith("s"))
                    {
                        MessageBox.Show("Added " + quantityIncrease + " " + itemName + "'.");
                    }
                    else
                    {
                        MessageBox.Show("Added " + quantityIncrease + " " + itemName + "'s.");
                    }
                }
                else
                {
                    MessageBox.Show("Added " + quantityIncrease + " " + itemName + ".");
                }
            }

            this.Close();

        }
    }
}
