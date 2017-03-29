// This class creates the Choose Address Form and allows the users to select an Address object to edit.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prog2
{
    public partial class ChooseAddressForm : Form
    {
        // Precondition: None
        // Postcondition: Creates a choose address form with a list of addresses past to it
        public ChooseAddressForm(List<Address> addressList)
        {
            InitializeComponent();

            // loads all of the address names into the combo box
            foreach (Address a in addressList)
                addressComboBox.Items.Add(a.Name);
            // sets a default item for the combo box
            addressComboBox.SelectedIndex = 0;
        }

        public int AddressIndex // Address Index property
        {
            // Precondition: None
            // Postcondition: returns the selected index from the address combo box
            get
            {
                return addressComboBox.SelectedIndex;
            }
            // Precondition: None
            // Postcondition: sets the selected index of the combo box to a specified value
            set
            {
                addressComboBox.SelectedIndex = value;
            }
        }

        // Precondition: the OK button was clicked
        // Postcondition: the OK dialog result is sent
        private void okBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        // Precondition: the cancel button was clicked
        // Postcondition: the cancel dialog result is sent
        private void cancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
