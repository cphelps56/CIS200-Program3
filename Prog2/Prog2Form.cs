// Program 3
// CIS 200-10
// Summer 2015
// Due: 6/22/2015
// By: Colin Phelps

// File: Prog2Form.cs
// This class creates the main GUI for Program 2. It provides a
// File menu with About, Exit, Open Addresses, and Save Addresses, an Insert menu with Address and
// Letter items, an Edit menu with an Address item, and a Report menu with List Addresses and List Parcels
// items.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;


namespace Prog2
{
    public partial class Prog2Form : Form
    {
        private List<Address> addressList; // The list of addresses
        private List<Parcel> parcelList;   // The list of parcels

        // Precondition:  None
        // Postcondition: The form's GUI is prepared for display. A few test addresses are
        //                added to the list of addresses
        public Prog2Form()
        {
            InitializeComponent();

            addressList = new List<Address>();
            parcelList = new List<Parcel>();

        }

        // Precondition:  File, About menu item activated
        // Postcondition: Information about author displayed in dialog box
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(String.Format("Program 2{0}By: Andrew L. Wright{0}" +
                "CIS 200{0}Summer 2015", Environment.NewLine), "About Program 2");
        }

        // Precondition:  File, Exit menu item activated
        // Postcondition: The application is exited
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Precondition:  Insert, Address menu item activated
        // Postcondition: The Address dialog box is displayed. If data entered
        //                are OK, an Address is created and added to the list
        //                of addresses
        private void addressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddressForm addressForm = new AddressForm(); // The address dialog box form
            DialogResult result = addressForm.ShowDialog(); // Show form as dialog and store result

            if (result == DialogResult.OK) // Only add if OK
            {
                try
                {
                    Address newAddress = new Address(addressForm.AddressName, addressForm.Address1,
                        addressForm.Address2, addressForm.City, addressForm.State,
                        int.Parse(addressForm.ZipText)); // Use form's properties to create address
                    addressList.Add(newAddress);
                }
                catch (FormatException) // This should never happen if form validation works!
                {
                    MessageBox.Show("Problem with Address Validation!", "Validation Error");
                }
            }

            addressForm.Dispose(); // Best practice for dialog boxes
        }

        // Precondition:  Report, List Addresses menu item activated
        // Postcondition: The list of addresses is displayed in the addressResultsTxt
        //                text box
        private void listAddressesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder result = new StringBuilder(); // Holds text as report being built
            // StringBuilder more efficient than String

            result.Append("Addresses:");
            result.Append(Environment.NewLine); // Remember, \n doesn't always work in GUIs
            result.Append(Environment.NewLine);

            foreach (Address a in addressList)
            {
                result.Append(a.ToString());
                result.Append(Environment.NewLine);
                result.Append(Environment.NewLine);
            }

            reportTxt.Text = result.ToString();

            // Put cursor at start of report
            reportTxt.Focus();
            reportTxt.SelectionStart = 0;
            reportTxt.SelectionLength = 0;
        }

        // Precondition:  Insert, Letter menu item activated
        // Postcondition: The Letter dialog box is displayed. If data entered
        //                are OK, a Letter is created and added to the list
        //                of parcels
        private void letterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LetterForm letterForm; // The letter dialog box form
            DialogResult result;   // The result of showing form as dialog

            if (addressList.Count < LetterForm.MIN_ADDRESSES) // Make sure we have enough addresses
            {
                MessageBox.Show("Need " + LetterForm.MIN_ADDRESSES + " addresses to create letter!",
                    "Addresses Error");
                return;
            }

            letterForm = new LetterForm(addressList); // Send list of addresses
            result = letterForm.ShowDialog();

            if (result == DialogResult.OK) // Only add if OK
            {
                try
                {
                    // For this to work, LetterForm's combo boxes need to be in same
                    // order as addressList
                    Letter newLetter = new Letter(addressList[letterForm.OriginAddressIndex],
                        addressList[letterForm.DestinationAddressIndex],
                        decimal.Parse(letterForm.FixedCostText)); // Letter to be inserted
                    parcelList.Add(newLetter);
                }
                catch (FormatException) // This should never happen if form validation works!
                {
                    MessageBox.Show("Problem with Letter Validation!", "Validation Error");
                }
            }

            letterForm.Dispose(); // Best practice for dialog boxes
        }

        // Precondition:  Report, List Parcels menu item activated
        // Postcondition: The list of parcels is displayed in the parcelResultsTxt
        //                text box
        private void listParcelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder result = new StringBuilder(); // Holds text as report being built
            // StringBuilder more efficient than String
            decimal totalCost = 0;                      // Running total of parcel shipping costs

            result.Append("Parcels:");
            result.Append(Environment.NewLine); // Remember, \n doesn't always work in GUIs
            result.Append(Environment.NewLine);

            foreach (Parcel p in parcelList)
            {
                result.Append(p.ToString());
                result.Append(Environment.NewLine);
                result.Append(Environment.NewLine);
                totalCost += p.CalcCost();
            }

            result.Append("------------------------------");
            result.Append(Environment.NewLine);
            result.Append(String.Format("Total Cost: {0:C}", totalCost));

            reportTxt.Text = result.ToString();

            // Put cursor at start of report
            reportTxt.Focus();
            reportTxt.SelectionStart = 0;
            reportTxt.SelectionLength = 0;
        }

        private BinaryFormatter formatter = new BinaryFormatter(); // object for serializing a list of Addresses in a binary format
        private FileStream output; // stream for writing to a file

        // Precondition: Address list must have items
        // Postcondition: The address list is serialized and written to the selected file
        private void saveAddressesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result; // variable to hold dialog result of the file chooser dialog box
            string fileName; // name of file to save data

            using (SaveFileDialog fileChooser = new SaveFileDialog())
            {
                fileChooser.CheckFileExists = false;

                result = fileChooser.ShowDialog(); // retrieve the result of the dialog box
                fileName = fileChooser.FileName; // get specified file name
            }

            if (result == DialogResult.OK)
            {
                // show error if user specified invalid file
                if (fileName == string.Empty)
                {
                    MessageBox.Show("Invalid File Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    // save file via FileStream if user specified valid file
                    try
                    {
                        // open file with write access
                        output = new FileStream(fileName, FileMode.Create, FileAccess.Write);

                        if (addressList.Count != 0) // tests if address list is empty
                        {
                            formatter.Serialize(output, addressList); // serializes address list
                        }
                        else // error message if address list is empty
                        {
                            MessageBox.Show("Address List is empty", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }// end try

                    catch (IOException) // handles exception if there is a problem opening the file
                    {
                        // notify user if file could not be opened
                        MessageBox.Show("Error opening file", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (SerializationException) // handles exception if there is a problem writing to the file
                    {
                        MessageBox.Show("Error Writing to File", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (FormatException) // handles exception if there is an error regarding format
                    {
                        MessageBox.Show("Invalid Format", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        // determines whether file exists
                        if (output != null)
                        {
                            //close file
                            try
                            {
                                output.Close();
                            }//end try
                            catch (IOException) // handles exception if there is an error closing the file
                            {
                                MessageBox.Show("Cannot close file", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }// end catch
                        }// end if
                    }//end finally
                }
            }
        }

        private BinaryFormatter reader = new BinaryFormatter(); // object for deserializing an address list in binary format
        private FileStream input; // stream for reading from a file

        // Precondition: Selected file must contain a list of addresses
        // Postcondition: Parcel list is cleared and address list is replaced with new address list from selected file
        private void openAddressesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result; // result of OpenFileDialog
            string fileName; // name of file containing data

            using (OpenFileDialog fileChooser = new OpenFileDialog())
            {
                result = fileChooser.ShowDialog();
                fileName = fileChooser.FileName; // get specified name
            }

            if (result == DialogResult.OK)
            {
                // show error if user specified invalid file
                if (fileName == string.Empty)
                {
                    MessageBox.Show("Invalid File Name", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    // create file stream to obtain read access to file
                    input = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                }

                try
                {
                    // address list is replaced with new address list from file
                    addressList = (List<Address>)reader.Deserialize(input);

                    parcelList.Clear(); // parcel list is cleared
                }
                catch (SerializationException) // handles exception when there is no serialized data in the file
                {
                    MessageBox.Show("There is no list of addresses in the selected file", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (InvalidCastException) // handles exception when there is not a list of addresses in the file
                {
                    MessageBox.Show("There is no list of addresses in the selected file", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (input != null)
                    {
                        // closes filestream
                        try
                        {
                            input.Close();
                        }
                        // handles exception when there is an error closing the file
                        catch (IOException)
                        {
                            MessageBox.Show("Cannot close file", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        // Precondition: Address list cannot be empty
        // Postcondition: An address object is selected and can be edited by the user
        private void addressesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChooseAddressForm chooseAddressForm; // the choose address dialog box form
            DialogResult result1; // holds the result from the dialog box

            if (addressList.Count < 1)// ensures the address list is not empty
            {
                MessageBox.Show("There are no addresses to edit", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                chooseAddressForm = new ChooseAddressForm(addressList);// creates form and sends the address list
                result1 = chooseAddressForm.ShowDialog();

                if (result1 == DialogResult.OK)
                {
                    int index = chooseAddressForm.AddressIndex; // holds selection index
                    Address a = addressList[index]; // variable to hold selected address

                    AddressForm addressForm = new AddressForm(); // creates address form dialog box

                    // sets text boxes on form to the properties from the selected address 
                    addressForm.AddressName = a.Name;
                    addressForm.Address1 = a.Address1;
                    addressForm.Address2 = a.Address2;
                    addressForm.City = a.City;
                    addressForm.State = a.State;
                    addressForm.ZipText = a.Zip.ToString();

                    DialogResult result2 = addressForm.ShowDialog();

                    if (result2 == DialogResult.OK)
                    {
                        // sets the properties for the selected address to the edited values
                        a.Name = addressForm.AddressName;
                        a.Address1 = addressForm.Address1;
                        a.Address2 = addressForm.Address2;
                        a.City = addressForm.City;
                        a.State = addressForm.State;
                        a.Zip = int.Parse(addressForm.ZipText);
                    }
                    addressForm.Dispose();
                }
                chooseAddressForm.Dispose();
            }
        }
    }
}