// File: NextDayAirPackage.cs
// The NextDayAirPackage class is a concrete derived class from AirPackage. It adds
// an express fee.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class NextDayAirPackage : AirPackage
{
    private decimal expressFee; // Next day air package's express fee

    // Precondition:  pLength > 0, pWidth > 0, pHeight > 0,
    //                pWeight > 0, expFee >= 0
    // Postcondition: The next day air package is created with the specified values for
    //                origin address, destination address, length, width,
    //                height, weight, and express fee
    public NextDayAirPackage(Address originAddress, Address destAddress,
        double pLength, double pWidth, double pHeight, double pWeight, decimal expFee)
        : base(originAddress, destAddress, pLength, pWidth, pHeight, pWeight)
    {
        ExpressFee = expFee;
    }

    public decimal ExpressFee
    {
        // Precondition:  None
        // Postcondition: The next day air package's express fee has been returned
        get
        {
            return expressFee;
        }

        // Precondition:  value >= 0
        // Postcondition: The next day air package's express fee has been set to the
        //                specified value
        private set // Helper set property
        {
            if (value >= 0)
                expressFee = value;
            else
                throw new ArgumentOutOfRangeException("ExpressFee", value,
                    "ExpressFee must be >= 0");
        }
    }

    // Precondition:  None
    // Postcondition: The next day air package's cost has been returned
    public override decimal CalcCost()
    {
        const decimal DIM_FACTOR = .40M;    // Dimension coefficient in cost equation
        const decimal WEIGHT_FACTOR = .30M; // Weight coefficient in cost equation
        const decimal HEAVY_FACTOR = .25M;  // Heavy coefficient in cost equation
        const decimal LARGE_FACTOR = .25M;  // Large coefficient in cost equation

        decimal cost; // Running total of cost of package

        cost = (DIM_FACTOR * (decimal)TotalDimension +
            WEIGHT_FACTOR * (decimal)Weight) + ExpressFee;

        if (IsHeavy())
            cost += HEAVY_FACTOR * (decimal)Weight;
        if (IsLarge())
            cost += LARGE_FACTOR * (decimal)TotalDimension;

        return cost;
    }

    // Precondition:  None
    // Postcondition: A String with the next day air package's data has been returned
    public override string ToString()
    {
        return String.Format("NextDay{0}{2}Express Fee: {1:C}",
            base.ToString(), ExpressFee, System.Environment.NewLine);
    }
}
