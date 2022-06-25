using System;
using System.Threading.Tasks;
using UIKit;
using Firebase.Auth; 
namespace UberClone
{
    public partial class LoginViewController : UIViewController
    {
        //AuthDataResultHandler handler = (user, error) =>
        //{
        //    if (error != null)
        //    {
        //        Console.WriteLine(error);
        //    }
        //    else
        //    {
        //        Console.WriteLine("success");
        //    }
        //};
        public LoginViewController(IntPtr handle) : base(handle)
        {

        }

        partial void UIButton2763_TouchUpInside(UIButton sender)
        {
            //try
            //{
            //  //  loginWithPassword(userName.Text, password.Text);
            //} catch
            //{
            //    ShowError(); 
            //}
        }
        //public void loginWithPassword(string email, string password)
        //{
        //    Auth.DefaultInstance.SignInWithPassword(email, password, handler); 
        //}
    }
}