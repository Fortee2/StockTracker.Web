using System;
using StockTracker.Business.DTO;
using StockTracker.Business.Enumerations;
using StockTracker.Business.Services.Interfaces;
using StockTracker.Core.Calculations;
using StockTracker.Core.Calculations.Response;
using StockTracker.Core.Domain;
using StockTracker.Core.Interfaces;
using StockTracker.Core.Interfaces.Calculations;
using StockTracker.Core.Entities;
using StockTracker.Infrastructure.Repository.Interfaces;
using Averages = StockTracker.Core.Entities.Averages;

namespace StockTracker.Business.Services
{
	public class MACDService: IMACDService
    {
        private readonly IMovingAverageRepo _movingAverageRepo;
        private readonly IAveragesRepo _averagesRepo;


        public MACDService(IMovingAverageRepo repo, IAveragesRepo averagesRepo)
		{
            _movingAverageRepo = repo;
            _averagesRepo = averagesRepo;
		}

        public void CalculateMACD(List<Securities> tickers)
        {
            foreach(var tick in tickers)
            {
                DateTime lastCalculated = GetLastMACDDate(tick.Id)?? DateTime.UnixEpoch;
                DateTime endCalculation = DateTime.Now;

                if (endCalculation.Subtract(lastCalculated).TotalDays < 1) continue;
                
                List<ITradingStructure> tradingData = (List<ITradingStructure>)GetDataForCalculations(tick.Id, lastCalculated,endCalculation);

                MACD mACD = new MACD(tradingData);

                mACD.EMA12Column = "Previous12EMA";
                mACD.EMA26Column = "Previous26EMA";
                mACD.MACDColumn = "MACD";
                mACD.SignalColumn = "Signal";

                List<IResponse> data = mACD.Calculate();

                _averagesRepo.AddRange(ConvertToAverageEntity(data, tick));
            }

        }

        private List<Averages> ConvertToAverageEntity(List<IResponse> dtoResults, Securities symbol)
        {
            List<Averages> averageRange = new List<Averages>();

            foreach (MacdResponse result in dtoResults)
            {
                averageRange.Add(new Averages()
                {
                    ActivityDate = result.ActivityDate,
                    TickerId = symbol.Id,
                    Value = result.MACD,
                    AverageType = "MACD"
                });

                averageRange.Add(new Averages()
                {
                    ActivityDate = result.ActivityDate,
                    TickerId = symbol.Id,
                    Value = result.Signal,
                    AverageType = "Signal"
                });
            }

            return averageRange;
        }

        private List<ITradingStructure> GetDataForCalculations(int tickerId, DateTime start, DateTime end)
        {
            var data = _movingAverageRepo.GetPresetAverages(tickerId, start, end);

            List<ITradingStructure> tradingStructures = new();

            if (data != null && data.Count > 0)
            {
                foreach(var ma in data)
                {
                    tradingStructures.Add(
                        new MACDData(ma.ActivityDate,
                        0,
                        0,
                        0,
                        ma.EMA12,
                        ma.EMA26 ?? 0
                        )
                    );
                }
            }

            return tradingStructures;
        }

        private DateTime? GetLastMACDDate(int tickerId)
        {
            var data = (from ema in _movingAverageRepo.GetDbContext().Averages
                        where ema.AverageType == "MACD"
                             && ema.TickerId == tickerId
                        orderby ema.ActivityDate descending
                        select ema.ActivityDate)
            .Take(1)
           ;

            return data.SingleOrDefault(); ;
        }
    }
}

