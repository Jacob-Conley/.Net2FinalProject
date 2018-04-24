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
    /// Interaction logic for CharaterCreatorWindow.xaml
    /// </summary>
    public partial class CharacterCreatorWindow : Window
    {
        private string _playerName;
        private string _race;
        private string _gender;
        private string _playerClass;
        private string _playerRace;
        private int _gameUserID;

        private const int MAX_POINTS = 5;
        private int _defaultValue;
        private int _pointsRemaining;
        private int strength;
        private int stamina;
        private int dexterity;
        private int intelligence;
        private int txtStrengthIncrement;
        private int txtStaminaIncrement;
        private int txtDexterityIncrement;
        private int txtIntelligenceIncrement;

        private string image;
        private int fileNumber;
        private UserManager _userManager;
        private CharacterManager _characterManager;
        private StatManager _statManager = new StatManager();
        private CharacterInventoryManager _inventoryManager = new CharacterInventoryManager();

        public CharacterCreatorWindow(UserManager userManager, int fileNumber, int gameUserID)
        {
            this._userManager = userManager;
            this.fileNumber = fileNumber;
            this._gameUserID = gameUserID;
            _characterManager = new CharacterManager();
            InitializeComponent();
        }


        private void Character_Creator_Loaded(object sender, RoutedEventArgs e)
        {
            cboClass.Items.Insert(0, "Choose a Class");
            cboClass.Items.Insert(1, "Warrior");
            cboClass.Items.Insert(2, "Rogue");
            cboClass.Items.Insert(3, "Mage");
            cboGender.Items.Insert(0, "Choose your Gender");
            cboGender.Items.Insert(1, "Male");
            cboGender.Items.Insert(2, "Female");
            cboRace.Items.Insert(0, "Select your Race");
            cboRace.Items.Insert(1, "Human");
            cboRace.Items.Insert(2, "Zombie");
            cboRace.Items.Insert(3, "Vampire");
            cboRace.Items.Insert(4, "Centaur");
            cboClass.SelectedIndex = 0;
            cboGender.SelectedIndex = 0;
            cboRace.SelectedIndex = 0;
        }

        private void btnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            _playerClass = cboClass.Text.ToLower();

            if (txtName.Text.Equals("") || txtName.Text.Equals(null))
            {
                MessageBox.Show("Please choose a name to continue.");
                txtName.Focus();
                return;
            }
            if (_playerClass.Equals("Choose your Class"))
            {
                MessageBox.Show("Please choose a class to continue.");
                cboClass.Focus();
                return;
            }

            _playerRace = cboRace.Text.ToLower();

            if (_playerRace.Equals("Select your Race"))
            {
                MessageBox.Show("Please select a race to continue.");
                cboRace.Focus();
                return;
            }

            _gender = cboGender.Text;
            if (_gender.Equals("Choose your Gender"))
            {
                MessageBox.Show("Please select a gender to continue.");
                cboGender.Focus();
                return;
            }
            _playerName = txtName.Text.ToString();


            tabsetCharacterCreation.SelectedItem = tabAttributes;
            _defaultValue = 10;
            _pointsRemaining = MAX_POINTS;
            setStatChanges();
            setInitialStatValues();
        }


        private void cboRace_DropDownClosed(object sender, EventArgs e)
        {
            _race = cboRace.Text;
            _gender = cboGender.Text;
            if (_gender.Equals(""))
            {
                imgSelectedRace.Source = new BitmapImage(new Uri(@"Resources\new_character.png", UriKind.Relative));
                return;
            }
            if (_race.Equals(""))
            {
                imgSelectedRace.Source = new BitmapImage(new Uri(@"Resources\new_character.png", UriKind.Relative));
                return;
            }
            else if (_race.Equals("Select your Race"))
            {
                imgSelectedRace.Source = new BitmapImage(new Uri(@"Resources\new_character.png", UriKind.Relative));
                return;
            }
            if (_gender.Equals("Female"))
            {
                _gender = "girl";
                image = _race.ToLower() + "_" + _gender;
                imgSelectedRace.Source = new BitmapImage(new Uri(@"Resources\" + image + ".png", UriKind.Relative));
            }
            else if (_gender.Equals("Choose your Gender"))
            {
                _gender = null;
                imgSelectedRace.Source = new BitmapImage(new Uri(@"Resources\new_character.png", UriKind.Relative));
                return;
            }
            else
            {
                _gender = "dude";
                image = _race.ToLower() + "_" + _gender;
                imgSelectedRace.Source = new BitmapImage(new Uri(@"Resources\" + image + ".png", UriKind.Relative));
            }

        }

        private void cboGender_DropDownClosed(object sender, EventArgs e)
        {
            _race = cboRace.Text;
            if (_race.Equals(""))
            {
                imgSelectedRace.Source = new BitmapImage(new Uri(@"Resources\new_character.png", UriKind.Relative));
                return;
            }
            else if (_race.Equals("Select your Race"))
            {
                imgSelectedRace.Source = new BitmapImage(new Uri(@"Resources\new_character.png", UriKind.Relative));
                return;
            }
            _gender = cboGender.Text;
            if (_gender.Equals("Female"))
            {
                _gender = "girl";
                image = _race.ToLower() + "_" + _gender;
                imgSelectedRace.Source = new BitmapImage(new Uri(@"Resources\" + image + ".png", UriKind.Relative));
            }
            else if (_gender.Equals("Male"))
            {
                _gender = "dude";
                image = _race.ToLower() + "_" + _gender;

                imgSelectedRace.Source = new BitmapImage(new Uri(@"Resources\" + image + ".png", UriKind.Relative));
            }
            else
            {
                _gender = null;
                imgSelectedRace.Source = new BitmapImage(new Uri(@"Resources\new_character.png", UriKind.Relative));
            }

        }





        private void setStatChanges()
        {
            // Set the default values then check individual bonuses
            txtStrengthIncrement = 1;
            txtStaminaIncrement = 1;
            txtDexterityIncrement = 1;
            txtIntelligenceIncrement = 1;

            if (_playerRace.Equals("human"))
            {
                if (_playerClass.Equals("warrior"))
                {
                    txtStrengthIncrement = 2;
                    txtStaminaIncrement = 2;
                }
                else if (_playerClass.Equals("rogue"))
                {
                    txtDexterityIncrement = 3;
                }
                else if (_playerClass.Equals("mage"))
                {
                    txtIntelligenceIncrement = 4;
                }
            }
            else if (_playerRace.Equals("zombie"))
            {
                if (_playerClass.Equals("warrior"))
                {
                    txtStrengthIncrement = 2;
                    txtStaminaIncrement = 4;
                }
                else if (_playerClass.Equals("rogue"))
                {
                    txtDexterityIncrement = 2;
                }
                else if (_playerClass.Equals("mage"))
                {
                    txtIntelligenceIncrement = 2;
                }
            }
            else if (_playerRace.Equals("vampire"))
            {
                if (_playerClass.Equals("warrior"))
                {
                    txtStrengthIncrement = 4;
                    txtStaminaIncrement = 2;
                }
                else if (_playerClass.Equals("rogue"))
                {
                    txtDexterityIncrement = 4;
                }
                else if (_playerClass.Equals("mage"))
                {
                    txtIntelligenceIncrement = 3;
                }
            }
            else if (_playerRace.Equals("centaur"))
            {
                if (_playerClass.Equals("warrior"))
                {
                    txtStrengthIncrement = 3;
                    txtStaminaIncrement = 3;
                }
                else if (_playerClass.Equals("rogue"))
                {
                    txtDexterityIncrement = 2;
                }
                else if (_playerClass.Equals("mage"))
                {
                    txtIntelligenceIncrement = 2;
                }
            }
        }

        private void btnStrengthIncrease_Click(object sender, RoutedEventArgs e)
        {
            if (_pointsRemaining != 0)
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

        private void btnStrengthDecrease_Click(object sender, RoutedEventArgs e)
        {
            if (_pointsRemaining != MAX_POINTS)
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
            if (_pointsRemaining == MAX_POINTS)
            {
                btnStrengthDecrease.IsEnabled = false;
                btnStaminaDecrease.IsEnabled = false;
                btnDexterityDecrease.IsEnabled = false;
                btnIntelligenceDecrease.IsEnabled = false;
            }
            if (int.Parse(txtStrength.Text) == _defaultValue)
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
            if (_pointsRemaining != MAX_POINTS)
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
            if (_pointsRemaining == MAX_POINTS)
            {
                btnStrengthDecrease.IsEnabled = false;
                btnStaminaDecrease.IsEnabled = false;
                btnDexterityDecrease.IsEnabled = false;
                btnIntelligenceDecrease.IsEnabled = false;
            }
            if (int.Parse(txtStamina.Text) == _defaultValue)
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
            if (_pointsRemaining != MAX_POINTS)
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
            if (_pointsRemaining == MAX_POINTS)
            {
                btnStrengthDecrease.IsEnabled = false;
                btnStaminaDecrease.IsEnabled = false;
                btnDexterityDecrease.IsEnabled = false;
                btnIntelligenceDecrease.IsEnabled = false;
            }
            if (int.Parse(txtDexterity.Text) == _defaultValue)
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
            if (_pointsRemaining != MAX_POINTS)
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
            if (_pointsRemaining == MAX_POINTS)
            {
                btnStrengthDecrease.IsEnabled = false;
                btnStaminaDecrease.IsEnabled = false;
                btnDexterityDecrease.IsEnabled = false;
                btnIntelligenceDecrease.IsEnabled = false;
            }
            if (int.Parse(txtIntelligence.Text) == _defaultValue)
            {
                btnIntelligenceDecrease.IsEnabled = false;
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            setInitialStatValues();
        }

        private void setInitialStatValues()
        {
            txtPointsRemaining.Text = MAX_POINTS.ToString();
            _pointsRemaining = MAX_POINTS;
            txtStrength.Text = _defaultValue.ToString();
            txtStamina.Text = _defaultValue.ToString();
            txtDexterity.Text = _defaultValue.ToString();
            txtIntelligence.Text = _defaultValue.ToString();
            strength = int.Parse(txtStrength.Text);
            stamina = int.Parse(txtStamina.Text);
            dexterity = int.Parse(txtDexterity.Text);
            intelligence = int.Parse(txtIntelligence.Text);
            btnStrengthDecrease.IsEnabled = false;
            btnStaminaDecrease.IsEnabled = false;
            btnDexterityDecrease.IsEnabled = false;
            btnIntelligenceDecrease.IsEnabled = false;
            btnStrengthIncrease.IsEnabled = true;
            btnStaminaIncrease.IsEnabled = true;
            btnDexterityIncrease.IsEnabled = true;
            btnIntelligenceIncrease.IsEnabled = true;
        }

        private void btnCreateCharacter_Click(object sender, RoutedEventArgs e)
        {
            if (_pointsRemaining != 0)
            {
                MessageBox.Show("Please use the rest of your stat points.");
                return;
            }

            // Set the values to the text in textboxes
            strength = int.Parse(txtStrength.Text);
            stamina = int.Parse(txtStamina.Text);
            dexterity = int.Parse(txtDexterity.Text);
            intelligence = int.Parse(txtIntelligence.Text);

            _statManager.CreateStats(strength, stamina, dexterity, intelligence);

            _characterManager.CreateCharacter(_gameUserID, fileNumber, _playerName, _playerRace,
                _playerClass, image);

            // cannot know which characterid was made, so loop through list to find
            // the active character in the specific slot this was made in.
            int playerCharacterID = 0;
            var characterList = _characterManager.RetrieveCharacterList(_gameUserID);
            foreach (var character in characterList)
            {
                if (character.PlayerSlot == fileNumber)
                {
                    playerCharacterID = character.PlayerCharacterID;
                }
            }
            _inventoryManager.CreateDefaultInventory(playerCharacterID);
            MessageBox.Show("Character created.");
            this.Close();
        }
    }
}
