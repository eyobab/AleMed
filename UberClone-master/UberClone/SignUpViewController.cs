using System;
using UIKit;
using Firebase.Auth;
using System.Threading.Tasks;
namespace UberClone
{
    public partial class SignUpViewController : UIViewController
    {
        public SignUpViewController (IntPtr handle) : base (handle)
        {
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View.BackgroundColor = new UIColor(patternImage: new UIImage("background.png"));
            createButton.Layer.CornerRadius = 10;
            createButton.Layer.BorderWidth = 1;
            createButton.Layer.BorderColor = UIColor.White.CGColor; 
            createButton.TouchUpInside += async (sender, e) =>
            {
                string Token = await CreateWithEmailPassword(email.Text, password.Text);
                if (Token != "")
                {
                    this.PerformSegue("GoToMap", this);
                }
                else
                {
                    var errorAlertController = UIAlertController.Create("Error", "Can't create user account", UIAlertControllerStyle.Alert);
                    errorAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                    PresentViewController(errorAlertController, true, null);
                }
            };
        }
        public async Task<string> CreateWithEmailPassword(string email, string password)
        {
            try
            {
                var user = await Auth.DefaultInstance.CreateUserAsync(email, password);
                return await user.GetIdTokenAsync();
            }
            catch
            {
                return "";
            }
        }
    }
}