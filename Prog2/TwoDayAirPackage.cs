// File: TwoDayAirPackage.cs
// The TwoDayAirPackage class is a concrete derived class from AirPackage. It adds
// a delivery type.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TwoDayAirPackage : AirPackage
{
    public enum Delivery { Early, Saver }; // Delivery types

    // Precondition:  pLength > 0, pWidth > 0, pHeight > 0,
    //                pWeight > 0
    // Postcondition: The two day air package is created with the specified values for
    //                origin address, destination address, length, width,
    //                height, weight, and delivery type
    public TwoDayAirPackage(Address originAddress, Address destAddress,
        double pLength, double pWidth, double pHeight, double pWeight, Delivery delType)
        : base(originAddress, destAddress, pLength, pWidth, pHeight, pWeight)
    {
        DeliveryType = delType;
    }

    public Delivery DeliveryType
    {
        // Precondition:  None
        // Postcondition: The two day air package's delivery type has been returned
        get;

        // Precondition:  None
        // Postcondition: The two day air package's delivery type has been set to the
        //                specified value
        set;
    }

    // Precondition:  None
    // Postcondition: The two day air package's cost has been returned
    public override decimal CalcCost()
    {
        const decimal DIM_FACTOR = .25M;       // Dimension coefficient in cost equation
        const decimal WEIGHT_FACTOR = .25M;    // Weight coefficient in cost equation
        const decimal DISCOUNT_FACTOR = 0.90M; // Discount factor in cost equation

        decimal cost; // Running total of cost of package

        cost = (DIM_FACTOR * (decimal)TotalDimension +
            WEIGHT_FACTOR * (decimal)Weight);

        if (DeliveryType == Delivery.Saver)
            cost *= DISCOUNT_FACTOR;

        return cost;
    }

    // Precondition:  None
    // Postcondition: A String with the two day air package's data has been returned
    public override string ToString()
    {
        return String.Format("TwoDay{0}{2}Delivery Type: {1}",
            base.ToString(), DeliveryType, System.Environment.NewLine);
    }
}
