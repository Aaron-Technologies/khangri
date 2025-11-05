using Khangri.DataAccess.KhangriDAL;
using Khangri.DataAccess.Abstract;
using Khangri.Entities;
using Khangri.UI.Models;
using AutoMapper;
using System.Data;

namespace Khangri.UI.Helpers
{
    public interface ICounterHelper
    {
        List<CounterViewModel> GetAllCounter(int counterId, ref string msg);
    }

    public class CounterHelper : ICounterHelper
    {
        private readonly IBaCounter _baCounter;
        private readonly IMapper _mapper;

        public CounterHelper(IMapper mapper, IBaCounter baCounter)
        {
            _mapper = mapper;
            _baCounter = baCounter;
        }

        public List<CounterViewModel> GetAllCounter(int counterId, ref string msg)
        {
            DataSet data = null;

            var AllCounter = new List<CounterViewModel>();
            try
            {
                data = _baCounter.GetCounter(counterId, ref msg, "0", 0);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            if (data != null && data.Tables != null && data.Tables.Count > 0)
            {
                var entity = _mapper.Map<List<EntityCounter>>(data.Tables[1].Rows);
                AllCounter = _mapper.Map<List<CounterViewModel>>(entity);

			}

            return AllCounter;
        }

    }
}
