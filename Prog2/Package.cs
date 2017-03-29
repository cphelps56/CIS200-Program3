// File: Package.cs
// The Package class is an abstract derived class from Parcel. It adds
// dimensions and weight.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class Package : Parcel
{
    private double length; // Length of package in inches
    private double width;  // Width of package in inches
    private double height; // Height of package in inches
    private double weight; // Weight of package in pounds

    // Precondition:  pLength > 0, pWidth > 0, pHeight > 0,
    //                pWeight > 0
    // Postcondition: The package is created with the specified values for
    //                origin address, destination address, length, width,
    //                height, and weight
    public Package(Address originAddress, Address destAddress,
        double pLength, double pWidth, double pHeight, double pWeight)
        : base(originAddress, destAddress)
    {
        Length = pLength;
        Width = pWidth;
        Height = pHeight;
        Weight = pWeight;
    }

    public double Length
    {
        // Precondition:  None
        // Postcondition: The package's length has been returned
        get
        {
            return length;
        }

        // Precondition:  value > 0
        // Postcondition: The package's length has been set to the
        //                specified value
        set
        {
            if (value > 0)
                length = value;
            else
                throw new ArgumentOutOfRangeException("Length", value,
                    "Length must be > 0");
        }
    }

    public double Width
    {
        // Precondition:  None
        // Postcondition: The package's width has been returned
        get
        {
            return width;
        }

        // Precondition:  value > 0
        // Postcondition: The package's width has been set to the
        //                specified value
        set
        {
            if (value > 0)
                width = value;
            else
                throw new ArgumentOutOfRangeException("Width", value,
                    "Width must be > 0");
        }
    }

    public double Height
    {
        // Precondition:  None
        // Postcondition: The package's height has been returned
        get
        {
            return height;
        }

        // Precondition:  value > 0
        // Postcondition: The package's height has been set to the
        //                specified value
        set
        {
            if (value > 0)
                height = value;
            else
                throw new ArgumentOutOfRangeException("Height", value,
                    "Height must be > 0");
        }
    }

    public double Weight
    {
        // Precondition:  None
        // Postcondition: The package's weight has been returned
        get
        {
            return weight;
        }

        // Precondition:  value > 0
        // Postcondition: The package's weight has been set to the
        //                specified value
        set
        {
            if (value > 0)
                weight = value;
            else
                throw new ArgumentOutOfRangeException("Weight", value,
                    "Weight must be > 0");
        }
    }
    
    // Helper Property
    protected double TotalDimension
    {
        // Precondition:  None
        // Postcondition: The package's (Length + Width + Height) is returned
        get
        {
            return (Length + Width + Height);
        }
    }

    // Precondition:  None
    // Postcondition: A String with the package's data has been returned
    public override string ToString()
    {
        return String.Format("Package{5}{0}{5}Length: {1:N1}{5}Width: {2:N1}{5}" +
        "Height: {3:N1}{5}Weight: {4:N1}", base.ToString(), Length, Width,
        Height, Weight, System.Environment.NewLine);
    }
}
