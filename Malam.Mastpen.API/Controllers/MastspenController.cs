using Malam.Mastpen.Core;
using Microsoft.AspNetCore.Mvc;


namespace Malam.Mastpen.API.Controllers
{
#pragma warning disable CS1591
    public class MastpenController : ControllerBase
    {
        public MastpenController()
        {
        }

        private IUserInfo m_userInfo;

        public IUserInfo UserInfo
        {
            get
            {
                return m_userInfo ?? (m_userInfo = new UserInfo());
            }
            set
            {
                m_userInfo = value;
            }
        }
    }
#pragma warning restore CS1591
}
