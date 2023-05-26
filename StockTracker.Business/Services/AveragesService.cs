using AutoMapper;
using StockTracker.Core.Calculations;
using StockTracker.Core.Calculations.Response;
using StockTracker.Core.Domain;
using StockTracker.Core.Interfaces.Calculations;
using StockTracker.Core.Interfaces;
using StockTracker.Business.DTO;
using StockTracker.Business.Enumerations;
using StockTracker.Infrastructure.Repository.Interfaces;
using Averages = StockTracker.Core.Entities.Averages;
using StockTracker.Business.Services.Interfaces;

namespace StockTracker.Business.Services
{
    public class AveragesService: IAverageService
    {
        readonly IAveragesRepo _repo;
        readonly IJobStatusRepo _statusRepo;
        readonly IMapper _mapper;
        private readonly Dictionary<AverageTypes, ushort> _hashTable = new Dictionary<AverageTypes, ushort>();

        public AveragesService(IAveragesRepo activitiesRepo, IJobStatusRepo jobStatusRepo, IMapper mapper)
		{
			_repo = activitiesRepo;
			_mapper = mapper;
            _statusRepo = jobStatusRepo;

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

        public async Task CalculateAllAveragesBySymbol(Securities ticker)
        {
             await ProcessAverages(ticker);
        }

        public async Task CalculateAllAverages(List<Securities> tickers)
        {
            foreach (var symbol in tickers)
            {
               await  ProcessAverages(symbol);
            }
        }

        private async Task ProcessAverages(Securities security)
        {
            List<Averages> averageRange = new();

            await _statusRepo.AddAsync(new Core.Entities.JobStatus()
            {
                JobName = "CalculateAllAverages",
                ActivityDescription = String.Format("Started Processing {0} ",
                security.Name),
                ActivityTime = DateTime.Now
            });

            foreach (var i in _hashTable)
            {
                await _statusRepo.AddAsync(new Core.Entities.JobStatus()
                {
                    JobName = "CalculateAllAverages",
                    ActivityDescription = String.Format("Calculating {0} ", i.Key),
                    ActivityTime = DateTime.Now
                });

                try
                {
                    MADto? previous = RetrieveLastAverage(security.Id, i.Key);
                    DateTime startTime = (previous == null) ? DateTime.UnixEpoch : previous.ActivityDate;

                    var data = RetrieveDataForAverageCalculations(security.Id, i.Key, previous);

                    if (data.Count <= 1) continue;  //Averages are up to date or no data available

                    if (i.Key.ToString().Contains("EMA"))
                    {
                        averageRange.AddRange(
                            ConvertToAverageEntity(
                                CalculateEMA(data, i.Value),
                                security,
                                i.Key
                            )
                        );

                        continue;
                    }

                    List<Averages> movingAvg = ConvertToAverageEntity(
                        CalculateMoveingAverage(data, i.Value),
                        security,
                        i.Key
                    ).Where(
                        dt => dt.ActivityDate > startTime
                    ).ToList();

                    averageRange.AddRange(
                        movingAvg
                    );


                }
                catch (Exception e)
                {
                    var ex = e.Message;
                }
            }

            if (averageRange != null && averageRange.Count > 0)
            {
                try
                {
                    await _repo.AddRangeAsync(averageRange);
                }
                catch (Exception ex)
                {
                    await _statusRepo.AddAsync(new Core.Entities.JobStatus()
                    {
                        JobName = "CalculateAllAverages",
                        ActivityDescription = String.Format("Error Saving {0} ",
                         ex.Message),
                        ActivityTime = DateTime.Now
                    });
                }

            }

            await _statusRepo.AddAsync(new Core.Entities.JobStatus()
            {
                JobName = "CalculateAllAverages",
                ActivityDescription = String.Format("Finished Processing {0} ",
                 security.Name),
                ActivityTime = DateTime.Now
            });
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
            _hashTable.Add(AverageTypes.MA7, 7);
            _hashTable.Add(AverageTypes.MA14, 14);
            _hashTable.Add(AverageTypes.MA21, 21);
            _hashTable.Add(AverageTypes.MA50, 50);
            _hashTable.Add(AverageTypes.EMA7, 7);
            _hashTable.Add(AverageTypes.EMA14,14);
            _hashTable.Add(AverageTypes.EMA12, 12);
            _hashTable.Add(AverageTypes.EMA26, 26);
            _hashTable.Add(AverageTypes.EMA21,21);
            _hashTable.Add(AverageTypes.EMA30,30);
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

        public List<MADto> RetrieveDataForAverageCalculations(int tickerId, AverageTypes averageType, MADto? lastUpdate)
        {
            var collection = new List<MADto>();
            ushort numberOfPeriods = _hashTable.Where(w => w.Key == averageType).Select(v => v.Value).First();
                      
            DateTime startTime = (lastUpdate == null) ? DateTime.UnixEpoch : lastUpdate.ActivityDate;

            if (averageType.ToString().StartsWith("MA") || averageType.ToString().Contains("VOL"))
            {
                startTime = CalculateNewStartDate(tickerId, startTime, numberOfPeriods);
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

        private DateTime CalculateNewStartDate(int TickerId, DateTime LastUpdated, int Interval)
        {
            var lastDataPoint = (from dates in _repo.GetDbContext().Activities
                                 where dates.TickerId == TickerId
                                    && dates.ActivityDate > LastUpdated
                                 select dates.ActivityDate).FirstOrDefault();

            if (lastDataPoint.Year == 1) return LastUpdated;

            //We have to retrieve more data than necessary because dates and periods are not the same thing
            var dateData = (from dates in _repo.GetDbContext().Activities
                            where (
                                dates.TickerId == TickerId 
                                && dates.ActivityDate < LastUpdated
                                && dates.ActivityDate > LastUpdated.Subtract(new TimeSpan(Interval * 2,0,0,0))  
                            )
                            orderby dates.ActivityDate
                            select dates.ActivityDate).Distinct().ToList();

            if (dateData.Count == 0) return DateTime.UnixEpoch;

            //TODO:Ordering the list descending doesn't change the order of the data returned
            //Think this maybe an EF and mysql issue.  I am just going to calculate where to
            //Grab a value from the end
            int calcuatePosition = dateData.Count - Interval - 1;

            return dateData.ElementAt(calcuatePosition);
        }

        public Task CalculateIndustryAverages(List<Securities> tickers)
        {
            throw new NotImplementedException();
        }
    }
}

