using Anticontrafact2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Anticontrafact2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AutificationLoginPage : ContentPage
    {
        public AutificationLoginPage()
        {
            InitializeComponent();

            BindingContext = new AutificftionLoginViewModel(this);
        }
    }
}