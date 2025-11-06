using Khangri.DataAccess.KhangriDAL;
using Khangri.DataAccess.Abstract;
using Khangri.Entities;
using Khangri.UI.Models;
using AutoMapper;
using System.Data;

namespace Khangri.UI.Helpers
{
    public interface IUserTypeHelper
    {
        List<EntityUserType> GetAllUserType(int userTypeId, ref string msg);
    }

    public class UserTypeHelper : IUserTypeHelper
    {
        private readonly IBaUserType _baUserType;
        private readonly IMapper _mapper;

        public UserTypeHelper(IMapper mapper, IBaUserType baUserType)
        {
            _mapper = mapper;
            _baUserType = baUserType;
        }

        public List<EntityUserType> GetAllUserType(int userTypeId, ref string msg)
        {
            DataSet data = null;

            var AllUserType = new List<EntityUserType>();
            try
            {
                data = _baUserType.GetUserType(userTypeId, ref msg, "0", 0);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            if (data != null && data.Tables != null && data.Tables.Count > 0)
            {
                AllUserType = _mapper.Map<List<EntityUserType>>(data.Tables[0].Rows);
            }

            return AllUserType;
        }

    }
}
