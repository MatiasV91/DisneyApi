using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisneyApi.Services
{
    public interface IEmailService
    {
        Task SendWelcomeEmail(string email);
    }
}
