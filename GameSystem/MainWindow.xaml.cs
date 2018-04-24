using DataObjects;
using GameSystem;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserManager _userManager = new UserManager();
        private CharacterManager _characterManager = new CharacterManager();
        private CharacterInventoryManager _characterInventoryManager = new CharacterInventoryManager();
        private InventoryManager _inventoryManager = new InventoryManager();
        private StatManager _statManager = new StatManager();
        private List<Character> _characterList;
        private User _user;
        private Character _characterSelected;
        private CharacterInventory _characterInventory;
        private int fileNumber;
        private Stat stats;
        private int _pointsRemaining;
        private int maxPoints;
        private int txtStrengthIncrement;
        private int txtStaminaIncrement;
        private int txtDexterityIncrement;
        private int txtIntelligenceIncrement;
        private string itemType;


        private const int MIN_PASSWORD_LENGTH = 6; // business rule
        private const int MIN_USERNAME_LENGTH = 8; // forced by naming rules
        private const int MAX_USERNAME_LENGTH = 16; // forced by db field length
        private const int STAT_DEFAULT_VALUES = 10; // Stat points start at 10 each 





        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnLogout.Visibility = Visibility.Hidden;
            btnEditProfile.Visibility = Visibility.Hidden;
            this.btnLogin.IsDefault = true;
            tabsetMain.SelectedItem = tabMain;
            txtUsername.Focus();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

            if (_user != null) // someone is logged in
            {
                // log out
                logout();
                return;

            }

            // accept the input
            var username = txtUsername.Text;

            if (username.Contains(" "))
            {
                username = username.Replace(" ", "_");
            }
            var password = txtPassword.Password;

            // check for missing or invalid data
            if (username.Length < MIN_USERNAME_LENGTH ||
                username.Length > MAX_USERNAME_LENGTH)
            {
                MessageBox.Show("Invalid Username", "Login Failed!",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);

                clearLogin();

            }
            if (password.Length <= MIN_PASSWORD_LENGTH)
            {
                MessageBox.Show("Invalid Password", "Login Failed!", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                clearLogin();

                return;
            }


            // before checking for the user token, we need to use a try block
            try
            {
                _user = _userManager.AuthenticateUser(username, password);
                loadInventory();

                if (_user.GameEditor == false)
                {
                    loadCharacters();
                    tabsetMain.SelectedItem = tabCharacterSelect;
                    btnLogout.Visibility = Visibility.Visible;
                    btnEditProfile.Visibility = Visibility.Visible;
                }
                else
                {
                    var result = MessageBox.Show("Would you like to edit the game", "Edit Game?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        tabWeaponInventory.Visibility = Visibility.Hidden;
                        tabEquipmentInventory.Visibility = Visibility.Hidden;
                        tabItemInventory.Visibility = Visibility.Hidden;
                        tabCharacterInformation.Visibility = Visibility.Hidden;
                        tabsetMain.SelectedItem = tabGame;
                        tabsetGame.SelectedItem = tabWeaponsList;
                        btnLogout.Visibility = Visibility.Visible;
                        btnEditProfile.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        loadCharacters();
                        tabsetMain.SelectedItem = tabCharacterSelect;
                        btnLogout.Visibility = Visibility.Visible;
                        btnEditProfile.Visibility = Visibility.Visible;
                    }

                }

            }
            catch (Exception ex) // nowhere to throw an exception at the presentation layer
            {
                string message = ex.Message;

                if (ex.InnerException != null)
                {
                    message += "/n/n" + ex.InnerException.Message;
                }

                MessageBox.Show(message, "Login Failed!",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                clearLogin();
            }
        }

        /*
         * Get all of the characters made by the currently logged in user
         * If no users exist, show only images for new characteds
         */
        private void loadCharacters()
        {
            enableSelection();
            resetImages();
            _characterList = _characterManager.RetrieveCharacterList(_user.GameUserID);

            chkSelectPlayer1.Content = "New Character";
            chkSelectPlayer2.Content = "New Character";
            chkSelectPlayer3.Content = "New Character";

            foreach (Character character in _characterList)
            {

                if (character.PlayerSlot == 1)
                {
                    imgCharacter1.Source = new BitmapImage(new Uri(@"Resources\" + character.PlayerImage + ".png", UriKind.Relative));
                    chkSelectPlayer1.Content = character.PlayerName;
                }
                else if (character.PlayerSlot == 2)
                {
                    imgCharacter2.Source = new BitmapImage(new Uri(@"Resources\" + character.PlayerImage + ".png", UriKind.Relative));
                    chkSelectPlayer2.Content = character.PlayerName;
                }
                else
                {
                    imgCharacter3.Source = new BitmapImage(new Uri(@"Resources\" + character.PlayerImage + ".png", UriKind.Relative));
                    chkSelectPlayer3.Content = character.PlayerName;
                }
            }
        }

        /*
         * Set the select character menu to show only new character images
         */
        private void resetImages()
        {
            imgCharacter1.Source = new BitmapImage(new Uri(@"Resources\new_character.png", UriKind.Relative));
            imgCharacter2.Source = new BitmapImage(new Uri(@"Resources\new_character.png", UriKind.Relative));
            imgCharacter3.Source = new BitmapImage(new Uri(@"Resources\new_character.png", UriKind.Relative));
        }

        /*
         * Reset the login if any errors occurred when trying to log in
         */
        private void clearLogin()
        {
            this.btnLogin.IsDefault = true;
            txtUsername.Text = "";
            txtPassword.Password = "";
            txtUsername.Focus();
        }

        /*
         * Set the user to null and allow a new user to sign in
         */
        private void logout()
        {
            _user = null;
            enableSelection();
            tabsetMain.SelectedItem = tabMain;
            this.btnLogin.IsDefault = true;

            clearLogin();
        }

        /*
         * Open the menu for a new player to create an account
         */
        private void btnCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            var frmCreateAccount = new CreateAccountWindow(_userManager);
            frmCreateAccount.ShowDialog();
        }

        private void btnEditProfile_Click(object sender, RoutedEventArgs e)
        {

            var frmUpdateProfile = new UpdateProfile(_user);
            frmUpdateProfile.ShowDialog();
            if (frmUpdateProfile.DialogResult == true)
            {
                btnLogout.Visibility = Visibility.Hidden;
                btnEditProfile.Visibility = Visibility.Hidden;
                logout();
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            if (tabsetMain.SelectedItem == tabCharacterSelect)
            {
                btnLogout.Visibility = Visibility.Hidden;
                btnEditProfile.Visibility = Visibility.Hidden;
                logout();
            }
            else
            {
                var frmLogoutMessage = new LogoutMessageWindow();
                frmLogoutMessage.ShowDialog();
                // Three values returned 0, 1, 2 
                // 0 closes window and does nothing
                // 1 closes window and logs user out
                // 2 closes window and allows the user to switch characters
                int result = frmLogoutMessage.returnValue;
                if (result == 1)
                {
                    btnLogout.Visibility = Visibility.Hidden;
                    btnEditProfile.Visibility = Visibility.Hidden;
                    logout();
                }
                else if (result == 2)
                {
                    loadCharacters();
                    tabsetMain.SelectedItem = tabCharacterSelect;
                    if (_user.GameEditor == true)
                    {
                        tabWeaponInventory.Visibility = Visibility.Visible;
                        tabEquipmentInventory.Visibility = Visibility.Visible;
                        tabItemInventory.Visibility = Visibility.Visible;
                        tabCharacterInformation.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private void chkSelectPlayer1_Checked(object sender, RoutedEventArgs e)
        {
            if (chkSelectPlayer1.IsEnabled == false)
            {
                chkSelectPlayer1.IsChecked = false;
            }
            else
            {
                imgCharacter2.IsEnabled = false;
                imgCharacter3.IsEnabled = false;
                chkSelectPlayer2.IsEnabled = false;
                chkSelectPlayer3.IsEnabled = false;
                fileNumber = 1;
                if (chkSelectPlayer1.Content.Equals("New Character"))
                {
                    _characterSelected = null;
                }
                else
                {
                    foreach (var character in _characterList)
                    {
                        if (character.PlayerSlot == 1)
                        {
                            _characterSelected = character;
                        }
                    }
                }
            }
        }

        private void chkSelectPlayer2_Checked(object sender, RoutedEventArgs e)
        {
            if (chkSelectPlayer2.IsEnabled == false)
            {
                chkSelectPlayer2.IsChecked = false;
            }
            else
            {
                imgCharacter1.IsEnabled = false;
                imgCharacter3.IsEnabled = false;
                chkSelectPlayer1.IsEnabled = false;
                chkSelectPlayer3.IsEnabled = false;
                fileNumber = 2;
                if (chkSelectPlayer2.Content.Equals("New Character"))
                {
                    _characterSelected = null;
                }
                else
                {
                    foreach (var character in _characterList)
                    {
                        if (character.PlayerSlot == 2)
                        {
                            _characterSelected = character;
                        }
                    }
                }
            }
        }

        private void chkSelectPlayer3_Checked(object sender, RoutedEventArgs e)
        {
            if (chkSelectPlayer3.IsEnabled == false)
            {
                chkSelectPlayer3.IsChecked = false;
            }
            else
            {
                imgCharacter1.IsEnabled = false;
                imgCharacter2.IsEnabled = false;
                chkSelectPlayer1.IsEnabled = false;
                chkSelectPlayer2.IsEnabled = false;
                fileNumber = 3;

                if (chkSelectPlayer3.Content.Equals("New Character"))
                {
                    _characterSelected = null;
                }
                else
                {
                    foreach (var character in _characterList)
                    {
                        if (character.PlayerSlot == 3)
                        {
                            _characterSelected = character;
                        }
                    }
                }
            }
        }

        private void chkSelectPlayer1_Unchecked(object sender, RoutedEventArgs e)
        {
            enableSelection();
        }

        private void chkSelectPlayer2_Unchecked(object sender, RoutedEventArgs e)
        {
            enableSelection();
        }

        private void chkSelectPlayer3_Unchecked(object sender, RoutedEventArgs e)
        {
            enableSelection();
        }

        private void enableSelection()
        {
            _characterSelected = null;
            chkSelectPlayer1.IsChecked = false;
            chkSelectPlayer2.IsChecked = false;
            chkSelectPlayer3.IsChecked = false;
            chkSelectPlayer1.IsEnabled = true;
            chkSelectPlayer2.IsEnabled = true;
            chkSelectPlayer3.IsEnabled = true;
            imgCharacter1.IsEnabled = true;
            imgCharacter2.IsEnabled = true;
            imgCharacter3.IsEnabled = true;
            tabCharacterInformation.Visibility = Visibility.Visible;
            tabWeaponInventory.Visibility = Visibility.Visible;
            tabEquipmentInventory.Visibility = Visibility.Visible;
            tabItemInventory.Visibility = Visibility.Visible;
        }

        private void imgCharacter1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (chkSelectPlayer1.IsChecked == true)
            {
                enableSelection();
            }
            else
            {
                imgCharacter2.IsEnabled = false;
                imgCharacter3.IsEnabled = false;
                chkSelectPlayer1.IsChecked = true;
                chkSelectPlayer2.IsEnabled = false;
                chkSelectPlayer3.IsEnabled = false;
                fileNumber = 1;
            }
        }

        private void imgCharacter2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (chkSelectPlayer2.IsChecked == true)
            {
                enableSelection();
            }
            else
            {
                imgCharacter1.IsEnabled = false;
                imgCharacter3.IsEnabled = false;
                chkSelectPlayer2.IsChecked = true;
                chkSelectPlayer1.IsEnabled = false;
                chkSelectPlayer3.IsEnabled = false;
                fileNumber = 2;
            }
        }

        private void imgCharacter3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (chkSelectPlayer3.IsChecked == true)
            {
                enableSelection();
            }
            else
            {
                imgCharacter1.IsEnabled = false;
                imgCharacter2.IsEnabled = false;
                chkSelectPlayer3.IsChecked = true;
                chkSelectPlayer1.IsEnabled = false;
                chkSelectPlayer2.IsEnabled = false;
                fileNumber = 3;
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (chkSelectPlayer1.IsChecked == false && chkSelectPlayer2.IsChecked == false && chkSelectPlayer3.IsChecked == false)
            {
                MessageBox.Show("Please select a player to continue");
            }
            else if ((chkSelectPlayer1.IsChecked == true && !chkSelectPlayer1.Content.Equals("New Character")) || (chkSelectPlayer2.IsChecked == true && !chkSelectPlayer2.Content.Equals("New Character")) ||
                (chkSelectPlayer3.IsChecked == true && !chkSelectPlayer3.Content.Equals("New Character")))
            {
                MessageBox.Show("Please select an unused slot to continue.");
                chkSelectPlayer1.IsChecked = false;
                chkSelectPlayer2.IsChecked = false;
                chkSelectPlayer3.IsChecked = false;
                chkSelectPlayer1.IsEnabled = true;
                chkSelectPlayer2.IsEnabled = true;
                chkSelectPlayer3.IsEnabled = true;
                return;
            }
            else
            {
                var characterGeneratorForm = new CharacterCreatorWindow(_userManager, fileNumber, _user.GameUserID);
                characterGeneratorForm.ShowDialog();
                if (characterGeneratorForm.IsActive == false)
                {
                    loadCharacters();
                }
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            if (chkSelectPlayer1.IsChecked == false && chkSelectPlayer2.IsChecked == false && chkSelectPlayer3.IsChecked == false)
            {
                MessageBox.Show("Please select a player to continue");
            }
            else if ((chkSelectPlayer1.IsChecked == true && chkSelectPlayer1.Content.Equals("New Character")) || (chkSelectPlayer2.IsChecked == true && chkSelectPlayer2.Content.Equals("New Character")) ||
                (chkSelectPlayer3.IsChecked == true && chkSelectPlayer3.Content.Equals("New Character")))
            {
                MessageBox.Show("Please select a created character to continue.");
                chkSelectPlayer1.IsChecked = false;
                chkSelectPlayer2.IsChecked = false;
                chkSelectPlayer3.IsChecked = false;
                chkSelectPlayer1.IsEnabled = true;
                chkSelectPlayer2.IsEnabled = true;
                chkSelectPlayer3.IsEnabled = true;
                return;
            }
            else
            {
                refreshInventoryList();
                tabsetMain.SelectedItem = tabGame;
                tabsetGame.SelectedItem = tabCharacterInformation;
                populateCharacterInformation();
                tabCharacterInformation.Focus();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (chkSelectPlayer1.IsChecked == false && chkSelectPlayer2.IsChecked == false && chkSelectPlayer3.IsChecked == false)
            {
                MessageBox.Show("Please select a player to continue");
            }
            else if ((chkSelectPlayer1.IsChecked == true && chkSelectPlayer1.Content.Equals("New Character")) || (chkSelectPlayer2.IsChecked == true && chkSelectPlayer2.Content.Equals("New Character")) ||
                (chkSelectPlayer1.IsChecked == true && chkSelectPlayer1.Content.Equals("New Character")))
            {
                MessageBox.Show("This character has not been created yet");
                chkSelectPlayer1.IsChecked = false;
                chkSelectPlayer2.IsChecked = false;
                chkSelectPlayer3.IsChecked = false;
                chkSelectPlayer1.IsEnabled = true;
                chkSelectPlayer2.IsEnabled = true;
                chkSelectPlayer3.IsEnabled = true;
                return;
            }
            else
            {
                var result = MessageBox.Show("Are you sure you wish to delete this character?", "Delete Character?", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.Yes)
                {
                    _characterManager.DeleteCharacter(_characterSelected.PlayerCharacterID);
                    MessageBox.Show("Character Deleted.", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    loadCharacters();
                }
                else
                {
                    MessageBox.Show("Character will not be deleted.", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void refreshInventoryList()
        {
            try
            {
                var equipmentInventory = _characterInventoryManager.RetrieveCharacterEquipmentList(_characterSelected.PlayerCharacterID);
                var weaponInventory = _characterInventoryManager.RetrieveCharacterWeaponList(_characterSelected.PlayerCharacterID);
                var itemInventory = _characterInventoryManager.RetrieveCharacterItemsList(_characterSelected.PlayerCharacterID);
                _characterInventory = new CharacterInventory(weaponInventory, equipmentInventory, itemInventory);
                if (_characterInventory.Equipment.Count == 0)
                {
                    dgEquipment.Visibility = Visibility.Hidden;
                    txtEquipmentInfo.SetValue(Grid.ColumnSpanProperty, 2);
                    txtEquipmentInfo.Text = "There is no equipment in your inventory";
                }
                else
                {
                    dgEquipment.Visibility = Visibility.Visible;
                    txtEquipmentInfo.SetValue(Grid.ColumnSpanProperty, 1);
                    txtEquipmentInfo.Text = "Selecting an item will display the description here";
                    dgEquipment.ItemsSource = _characterInventory.Equipment;
                }
                if (_characterInventory.Weapons.Count == 0)
                {
                    dgWeapons.Visibility = Visibility.Hidden;
                    txtWeaponInfo.SetValue(Grid.ColumnSpanProperty, 2);
                    txtWeaponInfo.Text = "There are no weapons in your inventory";
                }
                else
                {
                    dgWeapons.Visibility = Visibility.Visible;
                    txtWeaponInfo.SetValue(Grid.ColumnSpanProperty, 1);
                    txtWeaponInfo.Text = "Selecting an item will display the description here";
                    dgWeapons.ItemsSource = _characterInventory.Weapons;
                }
                if (_characterInventory.Items.Count == 0)
                {
                    dgItems.Visibility = Visibility.Hidden;
                    txtItemInfo.SetValue(Grid.ColumnSpanProperty, 2);
                    txtItemInfo.Text = "There are no items in your inventory";
                }
                else
                {
                    dgItems.Visibility = Visibility.Visible;
                    txtItemInfo.SetValue(Grid.ColumnSpanProperty, 1);
                    txtItemInfo.Text = "Selecting an item will display the description here";
                    dgItems.ItemsSource = _characterInventory.Items;
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message + "\n\n" + ex.InnerException.Message;
                MessageBox.Show(message, "Data Retrieval Error!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }

        private void tabCharacterInformation_GotFocus(object sender, RoutedEventArgs e)
        {

            if (!(e.OriginalSource is TabItem)) return;

            populateCharacterInformation();
            if (_pointsRemaining != 0)
            {
                enableGameEditorSelections();
                btnReset.Visibility = Visibility.Hidden;
                btnSave.Visibility = Visibility.Visible;
                setStatChanges();
            }

        }

        private void populateCharacterInformation()
        {
            stats = _statManager.RetrieveStats(_characterSelected.StatID);
            imgCharacter.Source = new BitmapImage(new Uri(@"Resources\" + _characterSelected.PlayerImage + ".png", UriKind.Relative));
            txtName.Text = _characterSelected.PlayerName;
            txtRace.Text = _characterSelected.PlayerRace;
            txtClass.Text = _characterSelected.PlayerClass;
            txtExperience.Text = stats.Experience.ToString();
            txtStrength.Text = stats.Strength.ToString();
            txtStamina.Text = stats.Stamina.ToString();
            txtDexterity.Text = stats.Dexterity.ToString();
            txtIntelligence.Text = stats.Intelligence.ToString();
            _pointsRemaining = stats.PointsRemaining;
            maxPoints = stats.PointsRemaining;
            txtPointsRemaining.Text = stats.PointsRemaining.ToString();
            cboClass.Visibility = Visibility.Hidden;
            btnSave.Visibility = Visibility.Hidden;

            if (stats.PointsRemaining == 0)
            {
                disableButtons();
            }
            else
            {
                enableGameEditorSelections();
                btnReset.Visibility = Visibility.Hidden;
                btnSave.Visibility = Visibility.Visible;
                enableButtons();
                btnStrengthDecrease.IsEnabled = false;
                btnStaminaDecrease.IsEnabled = false;
                btnDexterityDecrease.IsEnabled = false;
                btnIntelligenceDecrease.IsEnabled = false;
            }
        }

        private void disableButtons()
        {
            btnStrengthIncrease.IsEnabled = false;
            btnStrengthDecrease.IsEnabled = false;
            btnStaminaIncrease.IsEnabled = false;
            btnStaminaDecrease.IsEnabled = false;
            btnDexterityIncrease.IsEnabled = false;
            btnDexterityDecrease.IsEnabled = false;
            btnIntelligenceIncrease.IsEnabled = false;
            btnIntelligenceDecrease.IsEnabled = false;
        }

        private void enableButtons()
        {
            setStatChanges();
            btnStrengthIncrease.IsEnabled = true;
            btnStaminaIncrease.IsEnabled = true;
            btnDexterityIncrease.IsEnabled = true;
            btnIntelligenceIncrease.IsEnabled = true;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            _statManager.ResetStats(_characterSelected.StatID, stats.Strength, stats.Stamina, stats.Dexterity, stats.Intelligence);
            populateCharacterInformation();
            enableButtons();
            enableGameEditorSelections();
            btnSave.Visibility = Visibility.Visible;
            btnStrengthDecrease.IsEnabled = false;
            btnStaminaDecrease.IsEnabled = false;
            btnDexterityDecrease.IsEnabled = false;
            btnIntelligenceDecrease.IsEnabled = false;
            btnStrengthIncrease.IsEnabled = true;
            btnStaminaIncrease.IsEnabled = true;
            btnDexterityIncrease.IsEnabled = true;
            btnIntelligenceIncrease.IsEnabled = true;
        }

        private void enableGameEditorSelections()
        {
            if (_user.GameEditor == true)
            {
                cboClass.Items.Clear();
                cboClass.Visibility = Visibility.Visible;
                cboClass.Items.Insert(0, "Choose a Class");
                cboClass.Items.Insert(1, "warrior");
                cboClass.Items.Insert(2, "rogue");
                cboClass.Items.Insert(3, "mage");
                cboClass.SelectedIndex = 0;
            }
        }

        private void setStatChanges()
        {
            // Set the default values then check individual bonuses
            txtStrengthIncrement = 1;
            txtStaminaIncrement = 1;
            txtDexterityIncrement = 1;
            txtIntelligenceIncrement = 1;

            if (_characterSelected.PlayerRace.Equals("human"))
            {
                if (_characterSelected.PlayerClass.Equals("warrior"))
                {
                    txtStrengthIncrement = 2;
                    txtStaminaIncrement = 2;
                }
                else if (_characterSelected.PlayerClass.Equals("rogue"))
                {
                    txtDexterityIncrement = 3;
                }
                else if (_characterSelected.PlayerClass.Equals("mage"))
                {
                    txtIntelligenceIncrement = 4;
                }
            }
            else if (_characterSelected.PlayerRace.Equals("zombie"))
            {
                if (_characterSelected.PlayerClass.Equals("warrior"))
                {
                    txtStrengthIncrement = 2;
                    txtStaminaIncrement = 4;
                }
                else if (_characterSelected.PlayerClass.Equals("rogue"))
                {
                    txtDexterityIncrement = 2;
                }
                else if (_characterSelected.PlayerClass.Equals("mage"))
                {
                    txtIntelligenceIncrement = 2;
                }
            }
            else if (_characterSelected.PlayerRace.Equals("vampire"))
            {
                if (_characterSelected.PlayerClass.Equals("warrior"))
                {
                    txtStrengthIncrement = 4;
                    txtStaminaIncrement = 2;
                }
                else if (_characterSelected.PlayerClass.Equals("rogue"))
                {
                    txtDexterityIncrement = 4;
                }
                else if (_characterSelected.PlayerClass.Equals("mage"))
                {
                    txtIntelligenceIncrement = 3;
                }
            }
            else if (_characterSelected.PlayerRace.Equals("centaur"))
            {
                if (_characterSelected.PlayerClass.Equals("warrior"))
                {
                    txtStrengthIncrement = 3;
                    txtStaminaIncrement = 3;
                }
                else if (_characterSelected.PlayerClass.Equals("rogue"))
                {
                    txtDexterityIncrement = 2;
                }
                else if (_characterSelected.PlayerClass.Equals("mage"))
                {
                    txtIntelligenceIncrement = 2;
                }
            }
        }

        private void btnStrengthIncrease_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(txtPointsRemaining.Text) != 0)
            {
                _pointsRemaining--;
                txtStrength.Text = (int.Parse(txtStrength.Text) + txtStrengthIncrement).ToString();
                txtPointsRemaining.Text = _pointsRemaining.ToString();
                btnStrengthDecrease.IsEnabled = true;
            }
            if (_pointsRemaining == 0)
            {
                btnStrengthIncrease.IsEnabled = false;
                btnStaminaIncrease.IsEnabled = false;
                btnDexterityIncrease.IsEnabled = false;
                btnIntelligenceIncrease.IsEnabled = false;
            }
        }

        private void cboClass_DropDownClosed(object sender, EventArgs e)
        {
            if (_pointsRemaining != maxPoints)
            {
                txtStrength.Text = STAT_DEFAULT_VALUES.ToString();
                txtStamina.Text = STAT_DEFAULT_VALUES.ToString();
                txtDexterity.Text = STAT_DEFAULT_VALUES.ToString();
                txtIntelligence.Text = STAT_DEFAULT_VALUES.ToString();
                _pointsRemaining = maxPoints;
                txtPointsRemaining.Text = _pointsRemaining.ToString();
                enableButtons();
                btnStrengthDecrease.IsEnabled = false;
                btnStaminaDecrease.IsEnabled = false;
                btnDexterityDecrease.IsEnabled = false;
                btnIntelligenceDecrease.IsEnabled = false;
            }
            _characterSelected.PlayerClass = cboClass.SelectedValue.ToString();
            setStatChanges();
        }

        private void btnStrengthDecrease_Click(object sender, RoutedEventArgs e)
        {
            if (_pointsRemaining != maxPoints)
            {
                _pointsRemaining++;
                txtStrength.Text = (int.Parse(txtStrength.Text) - txtStrengthIncrement).ToString();
                txtPointsRemaining.Text = _pointsRemaining.ToString();
                btnStrengthDecrease.IsEnabled = true;
                btnStrengthIncrease.IsEnabled = true;
                btnStaminaIncrease.IsEnabled = true;
                btnDexterityIncrease.IsEnabled = true;
                btnIntelligenceIncrease.IsEnabled = true;
            }
            if (_pointsRemaining == maxPoints)
            {
                btnStrengthDecrease.IsEnabled = false;
                btnStaminaDecrease.IsEnabled = false;
                btnDexterityDecrease.IsEnabled = false;
                btnIntelligenceDecrease.IsEnabled = false;
            }
            if (int.Parse(txtStrength.Text) == STAT_DEFAULT_VALUES)
            {
                btnStrengthDecrease.IsEnabled = false;
            }
        }

        private void btnStaminaIncrease_Click(object sender, RoutedEventArgs e)
        {
            if (_pointsRemaining != 0)
            {
                _pointsRemaining--;
                txtStamina.Text = (int.Parse(txtStamina.Text) + txtStaminaIncrement).ToString();
                txtPointsRemaining.Text = _pointsRemaining.ToString();
                btnStaminaDecrease.IsEnabled = true;
            }
            if (_pointsRemaining == 0)
            {
                btnStrengthIncrease.IsEnabled = false;
                btnStaminaIncrease.IsEnabled = false;
                btnDexterityIncrease.IsEnabled = false;
                btnIntelligenceIncrease.IsEnabled = false;
            }
        }

        private void btnStaminaDecrease_Click(object sender, RoutedEventArgs e)
        {
            if (_pointsRemaining != maxPoints)
            {
                _pointsRemaining++;
                txtStamina.Text = (int.Parse(txtStamina.Text) - txtStaminaIncrement).ToString();
                txtPointsRemaining.Text = _pointsRemaining.ToString();
                btnStaminaDecrease.IsEnabled = true;
                btnStrengthIncrease.IsEnabled = true;
                btnStaminaIncrease.IsEnabled = true;
                btnDexterityIncrease.IsEnabled = true;
                btnIntelligenceIncrease.IsEnabled = true;
            }
            if (_pointsRemaining == maxPoints)
            {
                btnStrengthDecrease.IsEnabled = false;
                btnStaminaDecrease.IsEnabled = false;
                btnDexterityDecrease.IsEnabled = false;
                btnIntelligenceDecrease.IsEnabled = false;
            }
            if (int.Parse(txtStamina.Text) == STAT_DEFAULT_VALUES)
            {
                btnStaminaDecrease.IsEnabled = false;
            }
        }

        private void btnDexterityIncrease_Click(object sender, RoutedEventArgs e)
        {
            if (_pointsRemaining != 0)
            {
                _pointsRemaining--;
                txtDexterity.Text = (int.Parse(txtDexterity.Text) + txtDexterityIncrement).ToString();
                txtPointsRemaining.Text = _pointsRemaining.ToString();
                btnDexterityDecrease.IsEnabled = true;
            }
            if (_pointsRemaining == 0)
            {
                btnStrengthIncrease.IsEnabled = false;
                btnStaminaIncrease.IsEnabled = false;
                btnDexterityIncrease.IsEnabled = false;
                btnIntelligenceIncrease.IsEnabled = false;
            }
        }

        private void btnDexterityDecrease_Click(object sender, RoutedEventArgs e)
        {
            if (_pointsRemaining != maxPoints)
            {
                _pointsRemaining++;
                txtDexterity.Text = (int.Parse(txtDexterity.Text) - txtDexterityIncrement).ToString();
                txtPointsRemaining.Text = _pointsRemaining.ToString();
                btnDexterityDecrease.IsEnabled = true;
                btnStrengthIncrease.IsEnabled = true;
                btnStaminaIncrease.IsEnabled = true;
                btnDexterityIncrease.IsEnabled = true;
                btnIntelligenceIncrease.IsEnabled = true;
            }
            if (_pointsRemaining == maxPoints)
            {
                btnStrengthDecrease.IsEnabled = false;
                btnStaminaDecrease.IsEnabled = false;
                btnDexterityDecrease.IsEnabled = false;
                btnIntelligenceDecrease.IsEnabled = false;
            }
            if (int.Parse(txtDexterity.Text) == STAT_DEFAULT_VALUES)
            {
                btnDexterityDecrease.IsEnabled = false;
            }
        }

        private void btnIntelligenceIncrease_Click(object sender, RoutedEventArgs e)
        {
            if (_pointsRemaining != 0)
            {
                _pointsRemaining--;
                txtIntelligence.Text = (int.Parse(txtIntelligence.Text) + txtIntelligenceIncrement).ToString();
                txtPointsRemaining.Text = _pointsRemaining.ToString();
                btnIntelligenceDecrease.IsEnabled = true;
            }
            if (_pointsRemaining == 0)
            {
                btnStrengthIncrease.IsEnabled = false;
                btnStaminaIncrease.IsEnabled = false;
                btnDexterityIncrease.IsEnabled = false;
                btnIntelligenceIncrease.IsEnabled = false;
            }
        }

        private void btnIntelligenceDecrease_Click(object sender, RoutedEventArgs e)
        {
            if (_pointsRemaining != maxPoints)
            {
                _pointsRemaining++;
                txtIntelligence.Text = (int.Parse(txtIntelligence.Text) - txtIntelligenceIncrement).ToString();
                txtPointsRemaining.Text = _pointsRemaining.ToString();
                btnIntelligenceDecrease.IsEnabled = true;
                btnStrengthIncrease.IsEnabled = true;
                btnStaminaIncrease.IsEnabled = true;
                btnDexterityIncrease.IsEnabled = true;
                btnIntelligenceIncrease.IsEnabled = true;
            }
            if (_pointsRemaining == maxPoints)
            {
                btnStrengthDecrease.IsEnabled = false;
                btnStaminaDecrease.IsEnabled = false;
                btnDexterityDecrease.IsEnabled = false;
                btnIntelligenceDecrease.IsEnabled = false;
            }
            if (int.Parse(txtIntelligence.Text) == STAT_DEFAULT_VALUES)
            {
                btnIntelligenceDecrease.IsEnabled = false;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_user.GameEditor == true)
            {

                if (cboClass.SelectedValue.ToString().Equals("Choose your Class"))
                {
                    MessageBox.Show("Please choose a class to continue.");
                    cboClass.Focus();
                    return;
                }
                if (!_characterSelected.PlayerClass.Equals(cboClass.SelectedValue.ToString()))
                {
                    _characterManager.UpdateCharacter(_characterSelected.PlayerCharacterID, _characterSelected.PlayerClass
                        , cboClass.SelectedValue.ToString());
                }
            }

            var strength = int.Parse(txtStrength.Text);
            var stamina = int.Parse(txtStamina.Text);
            var dexterity = int.Parse(txtDexterity.Text);
            var intelligence = int.Parse(txtIntelligence.Text);
            var pointsRemaining = int.Parse(txtPointsRemaining.Text);

            _statManager.UpdateStats(_characterSelected.StatID, stats.Strength, strength,
                stats.Stamina, stamina, stats.Dexterity, dexterity, stats.Intelligence, intelligence, pointsRemaining);
            populateCharacterInformation();
            btnReset.Visibility = Visibility.Visible;
            txtClass.Visibility = Visibility.Visible;
            MessageBox.Show("Character stats updated.");
        }

        private void dgItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.dgItems.SelectedItem != null)
            {
                var item = (CharacterItem)dgItems.SelectedItem;
                txtItemInfo.Text = item.Description;
            }
        }

        private void dgEquipment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.dgEquipment.SelectedItem != null)
            {
                var equipment = (CharacterEquipment)dgEquipment.SelectedItem;
                txtEquipmentInfo.Text = equipment.Description;
            }
        }

        private void dgWeapons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.dgWeapons.SelectedItem != null)
            {
                var weapon = (CharacterWeapon)dgWeapons.SelectedItem;
                txtWeaponInfo.Text = weapon.Description;
            }
        }

        // Hide the descriptions in the list while keeping the information for later
        private void dgWeapons_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Description")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
            if (e.PropertyName == "WeaponID")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }

        private void dgEquipment_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Description")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
            if (e.PropertyName == "EquipmentID")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }

        private void dgItems_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Description")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
            if (e.PropertyName == "ItemID")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }

        private void btnDeleteWeapon_Click(object sender, RoutedEventArgs e)
        {
            // Check if anything is selected
            if (this.dgWeapons.SelectedItems.Count == 0)
            {
                MessageBox.Show("You haven't selected anything!");
                return;
            }
            var weapon = (CharacterWeapon)dgWeapons.SelectedItem;
            if (weapon.Quantity > 1)
            {
                itemType = "weapon";
                var frmUpdateQuantity = new InventoryItemUpdate(_characterSelected.PlayerCharacterID, weapon.Name,
                    weapon.WeaponID, weapon.Quantity, weapon.Quantity, itemType, UpdateMode.Delete);
                frmUpdateQuantity.ShowDialog();
                this.dgWeapons.SelectedItem = null;
                refreshInventoryList();
            }
            else
            {
                var result = MessageBox.Show("Are you sure you want to delete this weapon?", "Delete Weapon",
                    MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.Yes)
                {
                    _characterInventoryManager.DeleteWeapon(_characterSelected.PlayerCharacterID, weapon.WeaponID);
                    MessageBox.Show(weapon.Name + " deleted");
                    this.dgWeapons.SelectedItem = null;
                    refreshInventoryList();
                }
                else
                {
                    return;
                }
            }
        }

        private void btnDeleteEquipment_Click(object sender, RoutedEventArgs e)
        {
            // Check if anything is selected
            if (this.dgEquipment.SelectedItems.Count == 0)
            {
                MessageBox.Show("You haven't selected anything!");
                return;
            }
            var equipment = (CharacterEquipment)dgEquipment.SelectedItem;
            if (equipment.Quantity > 1)
            {
                itemType = "equipment";
                var frmUpdateQuantity = new InventoryItemUpdate(_characterSelected.PlayerCharacterID, equipment.Name,
                     equipment.EquipmentID, equipment.Quantity, equipment.Quantity, itemType, UpdateMode.Delete);
                frmUpdateQuantity.ShowDialog();
                this.dgEquipment.SelectedItem = null;
                refreshInventoryList();
            }
            else
            {
                var result = MessageBox.Show("Are you sure you want to delete this equipment?", "Delete Equipment",
                    MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.Yes)
                {
                    _characterInventoryManager.DeleteEquipment(_characterSelected.PlayerCharacterID, equipment.EquipmentID);
                    MessageBox.Show(equipment.Name + " deleted");
                    this.dgEquipment.SelectedItem = null;
                    refreshInventoryList();
                }
                else
                {
                    return;
                }
            }
        }

        private void btnDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            // Check if anything is selected
            if (this.dgItems.SelectedItems.Count == 0)
            {
                MessageBox.Show("You haven't selected anything!");
                return;
            }
            var item = (CharacterItem)dgItems.SelectedItem;
            if (item.Quantity > 1)
            {
                itemType = "item";
                var frmUpdateQuantity = new InventoryItemUpdate(_characterSelected.PlayerCharacterID, item.Name,
                    item.ItemID, item.Quantity, item.Quantity, itemType, UpdateMode.Delete);
                frmUpdateQuantity.ShowDialog();
                this.dgItems.SelectedItem = null;
                refreshInventoryList();
            }
            else
            {
                var result = MessageBox.Show("Are you sure you want to delete this item?", "Delete Item",
                    MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.Yes)
                {
                    _characterInventoryManager.DeleteItem(_characterSelected.PlayerCharacterID, item.ItemID);
                    MessageBox.Show(item.Name + " deleted");
                    this.dgItems.SelectedItem = null;
                    refreshInventoryList();
                }
                else
                {
                    return;
                }
            }
        }

        private void tabWeaponInventory_Loaded(object sender, RoutedEventArgs e)
        {
            if (_characterSelected == null)
            {
                btnDeleteWeaponFromList.Visibility = Visibility.Visible;
                btnUpdateWeapon.Visibility = Visibility.Visible;
                btnAddWeapon.Content = "Add new weapon";
                btnAddWeapon.SetValue(Grid.ColumnSpanProperty, 1);
            }
            else
            {

                btnDeleteWeaponFromList.Visibility = Visibility.Hidden;
                btnUpdateWeapon.Visibility = Visibility.Hidden;
                btnAddWeapon.Content = "Add weapon to inventory";
                btnAddWeapon.SetValue(Grid.ColumnSpanProperty, 2);
            }
        }

        private void loadInventory()
        {
            try
            {
                dgWeaponsList.ItemsSource = _inventoryManager.RetrieveWeaponList();
                dgEquipmentList.ItemsSource = _inventoryManager.RetrieveEquipmentList();
                dgItemsList.ItemsSource = _inventoryManager.RetrieveItemList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void tabEquipmentInventory_Loaded(object sender, RoutedEventArgs e)
        {
            if (_characterSelected == null)
            {
                btnDeleteEquipmentFromList.Visibility = Visibility.Visible;
                btnUpdateEquipment.Visibility = Visibility.Visible;
                btnAddEquipment.Content = "Add new equipment";
                btnAddEquipment.SetValue(Grid.ColumnSpanProperty, 1);
            }
            else
            {
                btnDeleteEquipmentFromList.Visibility = Visibility.Hidden;
                btnUpdateEquipment.Visibility = Visibility.Hidden;
                btnAddEquipment.Content = "Add equipment to inventory";
                btnAddEquipment.SetValue(Grid.ColumnSpanProperty, 2);
            }
        }

        private void tabItemInventory_Loaded(object sender, RoutedEventArgs e)
        {
            if (_characterSelected == null)
            {
                btnDeleteItemFromList.Visibility = Visibility.Visible;
                btnUpdateItem.Visibility = Visibility.Visible;
                btnAddItem.Content = "Add new item";
                btnAddItem.SetValue(Grid.ColumnSpanProperty, 1);
            }
            else
            {
                btnDeleteItemFromList.Visibility = Visibility.Hidden;
                btnUpdateItem.Visibility = Visibility.Hidden;
                btnAddItem.Content = "Add item to inventory";
                btnAddItem.SetValue(Grid.ColumnSpanProperty, 2);
            }
        }

        private void dgWeaponsList_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Description")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }

        private void dgEquipmentList_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Description")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }

        private void dgItemsList_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Description")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }

        private void btnAddWeapon_Click(object sender, RoutedEventArgs e)
        {
            Weapon selectedWeapon = (Weapon)dgWeaponsList.SelectedItem;
            if (_characterSelected == null)
            {
                var frmItemUpdate = new ItemUpdate("Weapon", ItemInformationMode.Add);
                frmItemUpdate.ShowDialog();
                loadInventory();
            }
            else
            {
                bool created = false;
                foreach (CharacterWeapon weapons in _characterInventory.Weapons)
                {
                    if (weapons.WeaponID == selectedWeapon.WeaponID)
                    {
                        var frmIncreaseQuantity = new InventoryItemUpdate(_characterSelected.PlayerCharacterID, selectedWeapon.Name,
                            selectedWeapon.WeaponID, weapons.Quantity, weapons.Quantity, "weapon", UpdateMode.Add);
                        frmIncreaseQuantity.ShowDialog();
                        created = true;
                        break;
                    }
                }
                if (created == false)
                {
                    var frmAddWeapon = new InventoryItemUpdate(_characterSelected.PlayerCharacterID,
                        selectedWeapon.Name, selectedWeapon.WeaponID, "weapon", UpdateMode.Add);
                    frmAddWeapon.ShowDialog();
                }
                refreshInventoryList();
            }
            
        }

        private void btnUpdateWeapon_Click(object sender, RoutedEventArgs e)
        {
            if (dgWeaponsList.SelectedItem == null)
            {
                MessageBox.Show("No weapon was selected");
            }
            else
            {
                Weapon selectedWeapon = (Weapon)dgWeaponsList.SelectedItem;
                var frmItemUpdate = new ItemUpdate(selectedWeapon.WeaponID, selectedWeapon.Name, selectedWeapon.Description, selectedWeapon.Attack, "Weapon", ItemInformationMode.Edit);
                frmItemUpdate.ShowDialog();
                loadInventory();

            }
        }

        private void btnDeleteWeaponFromList_Click(object sender, RoutedEventArgs e)
        {
            if (dgWeaponsList.SelectedItem == null)
            {
                MessageBox.Show("No weapon was selected");
            }
            else
            {
                Weapon selectedWeapon = (Weapon)dgWeaponsList.SelectedItem;
                var result = MessageBox.Show("Are you sure you wish to delete " + selectedWeapon.Name, "Delete Weapon", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.Yes)
                {
                    _inventoryManager.DeleteWeapon(selectedWeapon.WeaponID);
                    MessageBox.Show(selectedWeapon.Name + " Deleted");
                }
                loadInventory();
            }
        }

        private void btnAddEquipment_Click(object sender, RoutedEventArgs e)
        {

            Equipment selectedItem = (Equipment)dgEquipmentList.SelectedItem;
            if (_characterSelected == null)
            {
                var frmItemUpdate = new ItemUpdate("Equipment", ItemInformationMode.Add);
                frmItemUpdate.ShowDialog();
                loadInventory();
            }
            else
            {
                bool created = false;
                foreach (CharacterEquipment equipment in _characterInventory.Equipment)
                {
                    if (equipment.EquipmentID == selectedItem.EquipmentID)
                    {
                        var frmIncreaseQuantity = new InventoryItemUpdate(_characterSelected.PlayerCharacterID, selectedItem.Name,
                            selectedItem.EquipmentID, equipment.Quantity, equipment.Quantity, "equipment", UpdateMode.Add);
                        frmIncreaseQuantity.ShowDialog();
                        created = true;
                        break;
                    }
                }
                if (created == false)
                {
                    var frmAddItem = new InventoryItemUpdate(_characterSelected.PlayerCharacterID,
                        selectedItem.Name, selectedItem.EquipmentID, "equipment", UpdateMode.Add);
                    frmAddItem.ShowDialog();
                }
                refreshInventoryList();
            }

        }

        private void btnUpdateEquipment_Click(object sender, RoutedEventArgs e)
        {
            if (dgEquipmentList.SelectedItem == null)
            {
                MessageBox.Show("No equipment selected");
            }
            else
            {
                Equipment selectedEquipment = (Equipment)dgEquipmentList.SelectedItem;
                var frmItemUpdate = new ItemUpdate(selectedEquipment.EquipmentID, selectedEquipment.Name, selectedEquipment.Description, selectedEquipment.Defense, "Equipment", ItemInformationMode.Edit);
                frmItemUpdate.ShowDialog();
                loadInventory();

            }
        }

        private void btnDeleteEquipmentFromList_Click(object sender, RoutedEventArgs e)
        {
            if (dgEquipmentList.SelectedItem == null)
            {
                MessageBox.Show("No equipment selected");
            }
            else
            {
                Equipment selectedEquipment = (Equipment)dgEquipmentList.SelectedItem;
                var result = MessageBox.Show("Are you sure you wish to delete " + selectedEquipment.Name, "Delete Equipment", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.Yes)
                {
                    _inventoryManager.DeleteEquipment(selectedEquipment.EquipmentID);
                    MessageBox.Show(selectedEquipment.Name + " Deleted");
                }
                loadInventory();
            }
        }

        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {

            Item selectedItem = (Item)dgItemsList.SelectedItem;
            if (_characterSelected == null)
            {
                var frmItemUpdate = new ItemUpdate("Item", ItemInformationMode.Add);
                frmItemUpdate.ShowDialog();
                loadInventory();
            }
            else
            {
                bool created = false;
                foreach (CharacterItem items in _characterInventory.Items)
                {
                    if (items.ItemID == selectedItem.ItemID)
                    {
                        var frmIncreaseQuantity = new InventoryItemUpdate(_characterSelected.PlayerCharacterID, selectedItem.Name,
                            selectedItem.ItemID, items.Quantity, items.Quantity, "item", UpdateMode.Add);
                        frmIncreaseQuantity.ShowDialog();
                        created = true;
                        break;
                    }
                }
                if (created == false)
                {
                    var frmAddItem = new InventoryItemUpdate(_characterSelected.PlayerCharacterID,
                        selectedItem.Name, selectedItem.ItemID, "item", UpdateMode.Add);
                    frmAddItem.ShowDialog();
                }
                refreshInventoryList();
            }

        }

        private void btnUpdateItem_Click(object sender, RoutedEventArgs e)
        {
            if (dgItemsList.SelectedItem == null)
            {
                MessageBox.Show("No item was selected");
            }
            else
            {
                Item selectedItem = (Item)dgItemsList.SelectedItem;
                var frmItemUpdate = new ItemUpdate(selectedItem.ItemID, selectedItem.Name, selectedItem.Description, selectedItem.AttackBoost, selectedItem.DefenseBoost, ItemInformationMode.Edit);
                frmItemUpdate.ShowDialog();
                loadInventory();

            }
        }

        private void btnDeleteItemFromList_Click(object sender, RoutedEventArgs e)
        {
            if (dgItemsList.SelectedItem == null)
            {
                MessageBox.Show("No item was selected");
            }
            else
            {
                Item selectedItem = (Item)dgItemsList.SelectedItem;
                var result = MessageBox.Show("Are you sure you wish to delete " + selectedItem.Name, "Delete Item", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.Yes)
                {
                    _inventoryManager.DeleteItem(selectedItem.ItemID);
                    MessageBox.Show(selectedItem.Name + " Deleted");
                }
                loadInventory();
            }
        }

        private void dgItemsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = (Item)dgItemsList.SelectedItem;
            var frmItemDescription = new ItemDescription(item.Name, item.Description);
            frmItemDescription.ShowDialog();
            dgItemsList.SelectedItem = null;
        }

        private void dgEquipmentList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var equipment = (Equipment)dgEquipmentList.SelectedItem;
            var frmEquipmentDescription = new ItemDescription(equipment.Name, equipment.Description);
            frmEquipmentDescription.ShowDialog();
            dgItemsList.SelectedItem = null;
        }

        private void dgWeaponsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var weapon = (Weapon)dgWeaponsList.SelectedItem;
            var frmWeaponDescription = new ItemDescription(weapon.Name, weapon.Description);
            frmWeaponDescription.ShowDialog();
            dgItemsList.SelectedItem = null;
        }


    }
}
