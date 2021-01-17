using Hayalpc.Fatura.Common.Dtos;
using Hayalpc.Fatura.Common.Helpers.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hayalpc.Fatura.Common.Helpers
{
    public class SessionHelper : Hayalpc.Library.Common.Helpers.SessionHelper, ISessionHelper
    {
        public SessionHelper(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        private UserDto _user { get; set; }
        private UserDataDto _userData { get; set; }

        public new UserDto User
        {
            get
            {
                if (_user == null)
                    if (Get("User") != null)
                        _user = JsonConvert.DeserializeObject<UserDto>(Get("User"));
                return _user;
            }
            set { Set("User", JsonConvert.SerializeObject(value)); }
        }

        public new UserDataDto UserData
        {
            get
            {
                if (_userData == null)
                    if (IsAuthenticated)
                        if (Get("UserData") != null)
                            _userData = JsonConvert.DeserializeObject<UserDataDto>(Get("UserData"));
                return _userData;
            }
            set { Set("UserData", JsonConvert.SerializeObject(value)); }
        }
    }
}
