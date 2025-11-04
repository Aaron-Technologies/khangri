using Khangri.DataAccess.KhangriDAL;
using Khangri.DataAccess.Abstract;
using Khangri.Entities;
using Khangri.UI.Models;
using AutoMapper;
using System.Data;

namespace Khangri.UI.Helpers
{
    public interface ISetUpHelper
    {
        List<EntitySetup> GetAllSetup(ref string msg,int setUpId=1);
    }

    public class SetUpHelper : ISetUpHelper
    {
        private readonly IBaSetUp _baSetUp;
        private readonly IMapper _mapper;

        public SetUpHelper(IMapper mapper, IBaSetUp baSetUp)
        {
            _mapper = mapper;
            _baSetUp = baSetUp;
        }

        public List<EntitySetup> GetAllSetup(ref string msg,int setUpId)
        {
            DataSet data = null;

            var allSetup = new List<EntitySetup>();
            try
            {
                data = _baSetUp.GetSetUp(setUpId, ref msg, "0", 0);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            if (data != null && data.Tables != null && data.Tables.Count > 0)
            {
                allSetup = _mapper.Map<List<EntitySetup>>(data.Tables[1].Rows);
            }

            return allSetup;
        }

    }
}
