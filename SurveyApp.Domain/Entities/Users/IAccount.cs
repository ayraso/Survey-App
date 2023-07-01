using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Domain.Entities.Users
{
    public interface IAccount
    {
        public string AccountType { get; set; }
    }
}
