using Foundation;
using System;
using UIKit;
using Firebase.Auth;
using System.Threading.Tasks; 
namespace UberClone
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            View.BackgroundColor = new UIColor(patternImage: new UIImage("background.png"));
            SignInButton.Layer.CornerRadius = 10;
            SignInButton.Layer.BorderWidth = 1;
            SignUpButton.Layer.CornerRadius = 10;
            SignUpButton.Layer.BorderWidth = 1;
            SignUpButton.Layer.BorderColor = UIColor.White.CGColor;
            SignInButton.Layer.BorderColor = UIColor.White.CGColor;
            SignInButton.TouchUpInside += async (sender, e) =>
            {
                string Token = await LoginWithEmailPassword(email.Text, password.Text);
                if (Token != "")
                {
                    this.PerformSegue("GoToMap", this);
                }
                else 
                {
                    var errorAlertController = UIAlertController.Create("Error", "Email or password is invalid", UIAlertControllerStyle.Alert);
                    errorAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                    PresentViewController(errorAlertController, true, null);
                }
            };
        }

        public async Task<string> LoginWithEmailPassword(string email, string password)
        {
            try
            {
                var user = await Auth.DefaultInstance.SignInAsync(email, password);
                return await user.GetIdTokenAsync(); 
            }
            catch
            {
                return "";
            }
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}