using Khangri.DataAccess.KhangriDAL;
using Khangri.DataAccess.Abstract;
using Khangri.Entities;
using Khangri.UI.Models;
using Khangri.UI.Utils;
using AutoMapper;
using System.Data;

namespace Khangri.UI.Helpers
{
    public interface IUserHelper
    {
        List<EntityUser> GetAllUser(long userId, int userTypeid, int pageNo, int pageSize, ref string msg);
        List<EntityLoginUser> Login(string loginName, string password, ref string msg);
    }

    public class UserHelper : IUserHelper
    {
        private readonly IBaUser _baUser;
        private readonly IMapper _mapper;
        private readonly IUtility _utility;

        public UserHelper(IMapper mapper, IBaUser baUser, IUtility utility)
        {
            _mapper = mapper;
            _baUser = baUser;
            _utility = utility;
        }

        public List<EntityUser> GetAllUser(long userId, int userTypeid, int pageNo, int pageSize, ref string msg)
        {
            DataSet data = null;

            var AllUser = new List<EntityUser>();
            try
            {
                data = _baUser.GetUsers(userId, userTypeid, pageNo, pageSize, ref msg, "0", 0);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            if (data != null && data.Tables != null && data.Tables.Count > 0)
            {
                AllUser = _mapper.Map<List<EntityUser>>(data.Tables[1].Rows);
            }

            return AllUser;
        }
        public List<EntityLoginUser> Login(string loginName, string password, ref string msg)
        {
            DataSet data = null;
            var users = new List<EntityLoginUser>();
            try
            {
                data = _baUser.Login(loginName, password, ref msg, "0", 0);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return users;
            }
            if (String.IsNullOrWhiteSpace(msg))
            {
                msg = _utility.GetErrorMessageFromDataSet(data);
                if (!String.IsNullOrWhiteSpace(msg))
                {
                    return users;
                }

            }


            if (data != null && data.Tables != null && data.Tables.Count > 1)
            {
                try
                {
                    users = _mapper.Map<List<EntityLoginUser>>(data.Tables[1].Rows);
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }
            }

            return users;
        }

    }
}
