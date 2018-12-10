using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XAMLTest.Data;
using XAMLTest.Views;

namespace XAMLTest.Data
{
    public abstract class User
    {
        public static string UserName { get; set; }
        public static string Password { get; set; }

    }
}