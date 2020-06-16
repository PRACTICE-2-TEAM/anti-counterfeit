using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Anticontrafact2.Models
{
    public class User
    {
        public User()
        {
            IsLogin = false;
        }
        private string email;
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                if(value == null)
                {
                    IsLogin = false;
                }
                else
                {
                    IsLogin = true;
                }
            }
        }
        public bool IsLogin { get; set; }
    }
}
