using Khangri.DataAccess.Abstract;
using Khangri.Entities;
using Khangri.UI.Models;
using Khangri.UI.Utils;
using AutoMapper;
using System.Data;

namespace Khangri.UI.Helpers
{
    public interface IResetPasswordHelper
    {
        EntityResetPasswordInitiativeResponse InitiatePasswordReset(ResetPasswordInitiativeRequestViewModel viewModel, ref string msg);
        void ResetPassword(ResetPasswordViewModel viewModel, ref string msg);
    }

    public class ResetPasswordHelper : IResetPasswordHelper
    {
        private readonly IBaUser _baUser;
        private readonly IMapper _mapper;
        private readonly IUtility _utility;
        public ResetPasswordHelper(IBaUser baUser, IMapper mapper, IUtility utility)
        {
            _baUser = baUser;
            _mapper = mapper;
            _utility = utility;
        }

        public EntityResetPasswordInitiativeResponse InitiatePasswordReset(ResetPasswordInitiativeRequestViewModel viewModel, ref string msg)
        {
            msg = String.Empty;
            var res = new EntityResetPasswordInitiativeResponse();
            var ds = new DataSet();
            var entityRequest = _mapper.Map<EntityResetPasswordInitiativeRequest>(viewModel);
            try
            {
                ds = _baUser.InitiateResetPassword(entityRequest, ref msg, "0", 0);
                if (String.IsNullOrWhiteSpace(msg))
                {
                    msg = _utility.GetErrorMessageFromDataSet(ds, 2);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            if (String.IsNullOrWhiteSpace(msg))
            {
                if (ds != null && ds.Tables != null && ds.Tables.Count > 1)
                {
                    res = _mapper.Map<List<EntityResetPasswordInitiativeResponse>>(ds.Tables[1].Rows).FirstOrDefault();
                }
            }


            return res;
        }

        public void ResetPassword(ResetPasswordViewModel viewModel, ref string msg)
        {
            msg = String.Empty;

            var ds = new DataSet();
            var entityRequest = new EntityUserPasswordResetRequest()
            {
                UserId = Convert.ToInt64(viewModel.UserId),
                NewPassword = viewModel.NewPassword,
                Token = viewModel.Token
            };
            try
            {
                ds = _baUser.ResetPassword(entityRequest, ref msg, "0", 0);
                if (String.IsNullOrWhiteSpace(msg))
                {
                    msg = _utility.GetErrorMessageFromDataSet(ds);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
        }
    }
}
