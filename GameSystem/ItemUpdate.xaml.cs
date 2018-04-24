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
    /// Interaction logic for ItemUpdate.xaml
    /// </summary>
    public partial class ItemUpdate : Window
    {
        private InventoryManager _inventoryManager = new InventoryManager();
        private int itemID;
        private string name;
        private string description;
        private string itemType;
        private int itemBoost;
        private float itemBoost1;
        private float itemBoost2;
        private ItemInformationMode updateMode;

        public ItemUpdate(string itemType, ItemInformationMode updateMode)
        {
            this.itemType = itemType;
            this.updateMode = updateMode;
            InitializeComponent();
        }

        public ItemUpdate(int itemID, string name, string description, int itemBoost, string itemType, ItemInformationMode updateMode)
        {
            this.itemID = itemID;
            this.name = name;
            this.description = description;
            this.itemBoost = itemBoost;
            this.itemType = itemType;
            this.updateMode = updateMode;
            InitializeComponent();
        }

        public ItemUpdate(int itemID, string name, string description, float itemBoost1, float itemBoost2, ItemInformationMode updateMode)
        {
            this.itemID = itemID;
            itemType = "Item";
            this.name = name;
            this.description = description;
            this.itemBoost1 = itemBoost1;
            this.itemBoost2 = itemBoost2;
            this.updateMode = updateMode;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            populateContent();
            txtItemID.IsEnabled = false;
            if (updateMode == ItemInformationMode.Add)
            {
                txtItemID.Text = "This number will be generated upon completion";
                if (itemType.Equals("Item"))
                {
                    lblItemBoost1.Content = "Attack Boost";
                    lblItemBoost2.Content = "Defense Boost";
                }
                else if (itemType.Equals("Weapon"))
                {
                    lblItemBoost1.Content = "Attack";
                    txtItemBoost2.Visibility = Visibility.Hidden;
                }
                else
                {
                    lblItemBoost1.Content = "Defense";
                    txtItemBoost2.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                txtName.Text = name;
                txtItemID.Text = itemID.ToString();
                txtDescription.Text = description;
                if (itemType.Equals("Item"))
                {
                    lblItemBoost1.Content = "Attack Boost";
                    lblItemBoost2.Content = "Defense Boost";
                    txtItemBoost1.Text = itemBoost1.ToString();
                    txtItemBoost2.Text = itemBoost2.ToString();
                }
                else
                {
                    txtItemBoost1.Text = itemBoost.ToString();
                    if (itemType.Equals("Weapon"))
                    {
                        lblItemBoost1.Content = "Attack";
                    }
                    else
                    {
                        lblItemBoost1.Content = "Defense";
                    }
                    txtItemBoost2.Visibility = Visibility.Hidden;
                }
            }
        }

        private void populateContent()
        {
            if (itemType.Equals("Weapon"))
            {
                lblItemID.Content = "Weapon ID";
            }
            else if (itemType.Equals("Equipment"))
            {
                lblItemID.Content = "Equipment ID";
            }
            else
            {
                lblItemID.Content = "Item ID";
            }
        }

        private void btnSaveEdit_Click(object sender, RoutedEventArgs e)
        {
            if (updateMode == ItemInformationMode.Add)
            {
                if (itemType.Equals("Item"))
                {
                    var result = testItemContent();
                    if (result == false)
                    {
                        return;
                    }
                    name = txtName.Text;
                    description = txtDescription.Text;
                    var attackBoost = float.Parse(txtItemBoost1.Text);
                    var defenseBoost = float.Parse(txtItemBoost2.Text);
                    _inventoryManager.CreateItem(name, description, attackBoost, defenseBoost);
                }
                else if (itemType.Equals("Weapon"))
                {
                    var result = testContent();
                    if (result == false)
                    {
                        return;
                    }
                    name = txtName.Text;
                    description = txtDescription.Text;
                    var attack = int.Parse(txtItemBoost1.Text);
                    _inventoryManager.CreateWeapon(name, description, attack);
                }
                else
                {
                    var result = testContent();
                    if (result == false)
                    {
                        return;
                    }
                    name = txtName.Text;
                    description = txtDescription.Text;
                    var defense = int.Parse(txtItemBoost1.Text);
                    _inventoryManager.CreateEquipment(name, description, defense);
                }
            }
            else
            {
                if (itemType.Equals("Item"))
                {
                    var result = testItemContent();
                    if (result == false)
                    {
                        return;
                    }
                    var oldName = name;
                    var newName = txtName.Text;
                    var oldDescription = description;
                    var newDescription = txtDescription.Text;
                    var oldAttackBoost = itemBoost1;
                    var newAttackBoost = float.Parse(txtItemBoost1.Text);
                    var oldDefenseBoost = itemBoost2;
                    var newDefenseBoost = float.Parse(txtItemBoost2.Text);
                    _inventoryManager.UpdateItem(itemID, oldName, newName, oldDescription, newDescription,
                        oldAttackBoost, newAttackBoost, oldDefenseBoost, newDefenseBoost);
                }
                else if (itemType.Equals("Weapon"))
                {
                    var result = testContent();
                    if (result == false)
                    {
                        return;
                    }
                    var oldName = name;
                    var newName = txtName.Text;
                    var oldDescription = description;
                    var newDescription = txtDescription.Text;
                    var oldAttack = itemBoost;
                    var newAttack = int.Parse(txtItemBoost1.Text);
                    _inventoryManager.UpdateWeapon(itemID, oldName, newName, oldDescription, newDescription, oldAttack, newAttack);
                }
                else
                {
                    var result = testContent();
                    if (result == false)
                    {
                        return;
                    }
                    var oldName = name;
                    var newName = txtName.Text;
                    var oldDescription = description;
                    var newDescription = txtDescription.Text;
                    var oldDefense = itemBoost;
                    var newDefense = int.Parse(txtItemBoost1.Text);
                    _inventoryManager.UpdateEquipment(itemID, oldName, newName, oldDescription, newDescription, oldDefense, newDefense);
                }
            }
            this.Close();
        }

        private bool testContent()
        {
            var result = true;
            while (result == true)
            {
                if (txtName.Text == "" || txtName == null)
                {
                    if (itemType.Equals("Weapon"))
                    {
                        result = false;
                        MessageBox.Show("Please enter a name for the weapon");
                    }
                    else
                    {
                        result = false;
                        MessageBox.Show("Please enter a name for the equipment");
                    }
                    txtName.Focus();
                    break;
                }
                if (txtDescription.Text == "" || txtDescription == null)
                {
                    if (itemType.Equals("Weapon"))
                    {
                        result = false;
                        MessageBox.Show("Please enter a description for the weapon");
                    }
                    else
                    {
                        result = false;
                        MessageBox.Show("Please enter a description for the equipment");
                    }
                    txtDescription.Focus();
                    break;
                }
                int number;
                var isNumber = int.TryParse(txtItemBoost1.Text, out number);
                if (isNumber == false)
                {
                    result = false;
                    if (itemType.Equals("Weapon"))
                    {
                        MessageBox.Show("Please enter a number for the weapon's attack");
                    }
                    else
                    {
                        MessageBox.Show("Please enter a number for the equipment's defense");
                    }
                    txtItemBoost1.Text = "";
                    txtItemBoost1.Focus();
                    break;
                }
                break;
            }
            return result;
        }

        private bool testItemContent()
        {
            var result = true;
            while (result == true)
            {


                if (txtName.Text == "" || txtName == null)
                {
                    result = false;
                    MessageBox.Show("Please enter a name for the item");
                    txtName.Focus();
                    break;
                }
                if (txtDescription.Text == "" || txtDescription == null)
                {
                    result = false;
                    MessageBox.Show("Please enter a description for the item");
                    txtDescription.Focus();
                    break;
                }
                float number;
                var isNumber = float.TryParse(txtItemBoost1.Text, out number);
                if (isNumber == false)
                {
                    result = false;
                    MessageBox.Show("Please enter a number for the item's attack boost ");
                    txtItemBoost1.Text = "";
                    txtItemBoost1.Focus();
                    break;
                }
                isNumber = float.TryParse(txtItemBoost2.Text, out number);
                if (isNumber == false)
                {
                    result = false;
                    MessageBox.Show("Please enter a number for the item's defense boost ");
                    txtItemBoost2.Text = "";
                    txtItemBoost2.Focus();
                    break;
                }
                break;
            }
            return result;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
