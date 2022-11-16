using AutoMapper;
using StockTracker.Core.Calculations;
using StockTracker.Core.Calculations.Response;
using StockTracker.Core.Domain;
using StockTracker.Core.Interfaces.Calculations;
using StockTracker.Core.Interfaces;
using StockTracker.Business.DTO;
using StockTracker.Business.Enumerations;
using StockTracker.Infrastructure.Repository.Interfaces;
using Averages = StockTracker.Domain.Entities.Averages;
using StockTracker.Business.Services.Interfaces;

namespace StockTracker.Business.Services
{
    public class AveragesService: IAverageService
    {
        readonly IAveragesRepo _repo;
        readonly IMapper _mapper;
        private readonly Dictionary<AverageTypes, ushort> _hashTable = new Dictionary<AverageTypes, ushort>();

        public AveragesService(IAveragesRepo activitiesRepo, IMapper mapper)
		{
			_repo = activitiesRepo;
			_mapper = mapper;

            LoadDictionary();
        }

		public List<MADto> CalculateEMA(List<MADto> eMAs, ushort numberOfPeriods)
		{
            List<MADto> dtos = new();

            if (eMAs.Count == 0) return dtos;

			List<ITradingStructure> eMAData = new();
			eMAData.AddRange(_mapper.Map<List<MADto>, List<MAData>>(eMAs));

			ExponetialMovingAverage exponetialMovingAverage = new(eMAData);

			exponetialMovingAverage.NumberOfPeriods = numberOfPeriods;
			exponetialMovingAverage.ColumnPreviousEma = "PrevMA";
			exponetialMovingAverage.ColumnToAvg = "CalculateValue";

            var resp =  exponetialMovingAverage.Calculate();

			foreach(IResponse response in resp)
			{
				dtos.Add(new MADto(response.ActivityDate, 0,response.GetDecimalValue("Value")));
			}

			return dtos;
        }

		public List<IResponse> CalculateMoveingAverage(List<MADto> MAs, ushort numberOfPeriods)
		{
            if (MAs.Count == 0) return new List<IResponse>();

            List<ITradingStructure> mAData = new();
            mAData.AddRange(_mapper.Map<List<MADto>, List<MAData>>(MAs));

            MovingAveraage movingAveraage = new MovingAveraage((IList<ITradingStructure>)mAData);

			movingAveraage.NumberOfPeriods = numberOfPeriods;
            movingAveraage.ColumnToAvg = "CalculateValue";

            return movingAveraage.Calculate();
		}

        public void CalculateAllAverages(List<Securities> tickers)
        {
            foreach (var symbol in tickers)
            {
                List<Averages> averageRange = new();

                foreach (var i in _hashTable)
                {
                    try
                    {
                        MADto? previous = RetrieveLastAverage(symbol.Id, i.Key);
                        DateTime startTime = (previous == null) ? DateTime.UnixEpoch : previous.ActivityDate;

                        var data = RetrieveDataForAverageCalculations(symbol.Id, i.Key, i.Value, previous);

                        if (data.Count <= 1) continue;  //Averages are up to date or no data available

                        if (i.Key.ToString().Contains("EMA"))
                        {
                            averageRange.AddRange(
                                ConvertToAverageEntity(
                                    CalculateEMA(data, i.Value),
                                    symbol,
                                    i.Key
                                )
                            );

                            continue;
                        }

                        List<IResponse> averageData = CalculateMoveingAverage(data, i.Value);

                        
                        List<Averages> movingAvg = ConvertToAverageEntity(averageData,
                                symbol,
                                i.Key
                            ).Where(dt => dt.ActivityDate > startTime).ToList();

                        averageRange.AddRange(
                            movingAvg
                        );


                    }
                    catch (Exception e)
                    {
                        var ex = e.Message;
                    }
                }

                if (averageRange.Count > 0)
                {
                    try
                    {
                        _repo.AddRange(averageRange);

                    }
                    catch (Exception ex)
                    {
                        var message = ex.Message;
                    }

                }
            }
        }

        private List<Averages> ConvertToAverageEntity(List<MADto> dtoResults, Securities symbol, AverageTypes averageTypes)
        {
            List<Averages> averageRange = new List<Averages>();

            foreach (var result in dtoResults)
            {
                if (result.PreviousMA == 0) //Newly calculated value
                {
                    averageRange.Add(new Averages()
                    {
                        ActivityDate = result.ActivityDate,
                        TickerId = symbol.Id,
                        Value = result.CalculateValue,
                        AverageType = averageTypes.ToString()
                    });
                }
            }

            return averageRange;
        }

        private List<Averages> ConvertToAverageEntity(List<IResponse> dtoResults, Securities symbol, AverageTypes averageTypes)
        {
            List<Averages> averageRange = new List<Averages>();

            foreach (AverageResponse result in dtoResults)
            {
                averageRange.Add(new Averages()
                {
                    ActivityDate = result.ActivityDate,
                    TickerId = symbol.Id,
                    Value = result.Value,
                    AverageType = averageTypes.ToString()
                });
            }

            return averageRange;
        }

        private void LoadDictionary()
        {
            _hashTable.Add(AverageTypes.VOL90, 90);
            _hashTable.Add(AverageTypes.EMA12, 12);
            _hashTable.Add(AverageTypes.EMA26, 26);
            _hashTable.Add(AverageTypes.MA9, 9);
            _hashTable.Add(AverageTypes.MA7, 7);
            _hashTable.Add(AverageTypes.MA14, 14);
            _hashTable.Add(AverageTypes.MA21, 21);
            _hashTable.Add(AverageTypes.MA50, 50);
            _hashTable.Add(AverageTypes.EMA9, 9);
            _hashTable.Add(AverageTypes.EMA2, 2);
            _hashTable.Add(AverageTypes.EMA3, 3);
            _hashTable.Add(AverageTypes.EMA4, 4);
            _hashTable.Add(AverageTypes.EMA5, 5);
            _hashTable.Add(AverageTypes.EMA6, 6);
            _hashTable.Add(AverageTypes.EMA7, 7);
            _hashTable.Add(AverageTypes.EMA8, 8);
        }

        public MADto? RetrieveLastAverage(int tickerId, AverageTypes averageType)
        {
            var data = (from ema in _repo.GetDbContext().Averages 
                        where ema.AverageType == averageType.ToString()
                             && ema.TickerId == tickerId
                        orderby ema.ActivityDate descending
                        select (new MADto(ema.ActivityDate, ema.Value ?? 0, 0)))
                       .Take(1)
                       ;


            return data.SingleOrDefault();
        }

        public List<MADto> RetrieveDataForAverageCalculations(int tickerId, AverageTypes averageType, ushort numberOfPeriods, MADto? lastUpdate)
        {
            var collection = new List<MADto>();

          
            DateTime startTime = (lastUpdate == null) ? DateTime.UnixEpoch : lastUpdate.ActivityDate;

            if (averageType.ToString().StartsWith("MA") || averageType.ToString().Contains("VOL"))
            {
                startTime = CalculateNewStartDate(startTime, numberOfPeriods);
            }
            else
            {
                if (lastUpdate != null) collection.Add(lastUpdate);
            }

            var data = (from quotes in _repo.GetDbContext().Activities
                        where (
                            quotes.TickerId == tickerId
                                && quotes.ActivityDate > startTime
                            )
                        orderby quotes.ActivityDate
                        select (new MADto(quotes.ActivityDate, 0, (averageType.ToString().Contains("VOL") ? quotes.Volume : quotes.Close))));
            ;

            collection.AddRange(data.ToList());

            return collection;
        }

        private DateTime CalculateNewStartDate(DateTime lastUpdated, int interval)
        {
            var dateData = (from dates in _repo.GetDbContext().Activities
                            where (
                                dates.ActivityDate < lastUpdated
                                && dates.ActivityDate > lastUpdated.Subtract(new TimeSpan(365,0,0,0))
                            )
                            orderby dates.ActivityDate descending
                            select dates.ActivityDate).Distinct().ToList();

            if (dateData.Count == 0) return DateTime.UnixEpoch;

            return dateData.ElementAt(interval);
        }
    }
}

