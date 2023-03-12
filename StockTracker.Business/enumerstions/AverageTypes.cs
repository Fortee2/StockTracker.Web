using System;
namespace StockTracker.Business.Enumerations
{
	//Averages need to be in numerical order.  The moving average simulator uses
	//the integer value of the enum to determine a shorter time period average
	//Before compared to a longer period average
	public enum AverageTypes
	{
		MA7,
		MA9,
		MA14,
		MA21,
		MA50,
		EMA2,
		EMA3,
		EMA4,
		EMA5,
		EMA6,
		EMA7,
		EMA8,
		EMA9,
		EMA10,
		EMA11,
		EMA12,
		EMA13,
		EMA14,
		EMA15,
		EMA16,
		EMA17,
		EMA18,
		EMA19,
		EMA20,
		EMA21,
		EMA22,
		EMA23,
		EMA24,
		EMA25,
		EMA26,
		EMA27,
		EMA28,
		EMA29,
		EMA30,
		VOL90
	}
}

