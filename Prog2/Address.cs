// File: Address.cs
// This classes stores a typical US address consisting of name,
// two address lines, city, state, and 5 digit zip code.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]

public class Address
{
    public const int MAX_ZIP = 99999; // Maximum ZipCode value

    private int zip;                  // Address' zip code

    // Precondition:  0 <= zipcode <= MAX_ZIP
    // Postcondition: The address is created with the specified values for
    //                name, address1, address2, city, state, and zipcode
    public Address(String name, String address1, String address2,
        String city, String state, int zipcode)
    {
        Name = name;
        Address1 = address1;
        Address2 = address2;
        City = city;
        State = state;
        Zip = zipcode;
    }

    public String Name
    {
        // Precondition:  None
        // Postcondition: The address' name has been returned
        get;

        // Precondition:  None
        // Postcondition: The address' name has been set to the
        //                specified value
        set;
    }

    public String Address1
    {
        // Precondition:  None
        // Postcondition: The address' first address line has been returned
        get;

        // Precondition:  None
        // Postcondition: The address' first address line has been set to
        //                the specified value
        set;
    }

    public String Address2
    {
        // Precondition:  None
        // Postcondition: The address' second address line has been returned
        get;

        // Precondition:  None
        // Postcondition: The address' second address line has been set to
        //                the specified value
        set;
    }

    public String City
    {
        // Precondition:  None
        // Postcondition: The address' city has been returned
        get;

        // Precondition:  None
        // Postcondition: The address' city has been set to the
        //                specified value
        set;
    }

    public String State
    {
        // Precondition:  None
        // Postcondition: The address' state has been returned
        get;

        // Precondition:  None
        // Postcondition: The address' state has been set to the
        //                specified value
        set;
    }

    public int Zip
    {
        // Precondition:  None
        // Postcondition: The address' zip code has been returned
        get
        {
            return zip;
        }

        // Precondition:  0 <= value <= MAX_ZIP
        // Postcondition: The address' zip code has been set to the
        //                specified value
        set
        {
            if ((value >= 0) && (value <= MAX_ZIP))
                zip = value;
            else
                throw new ArgumentOutOfRangeException("ZIP", value,
                    "ZIP must be U.S. 5 digit zip code");
        }
    }

    // Precondition:  None
    // Postcondition: A String with the address' data has been returned
    public override String ToString()
    {
        if (String.IsNullOrWhiteSpace(Address2)) // Is Address2 empty?
            return String.Format("{0}{5}{1}{5}{2}, {3} {4:D5}",
            Name, Address1, City, State, Zip, Environment.NewLine);
        else
            return String.Format("{0}{6}{1}{6}{2}{6}{3}, {4} {5:D5}",
                Name, Address1, Address2, City, State, Zip, Environment.NewLine);
    }
}