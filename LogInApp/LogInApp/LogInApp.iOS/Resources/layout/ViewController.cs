using System;
using LogInApp;
using LogInApp.ViewModels;


using UIKit;

namespace LogInApp.iOS
{
    public partial class ViewController : UIViewController
    {
        int count = 1;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public MainViewModel ViewModel
        {
            get { return MainViewModel.Instance; }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.
            Button.AccessibilityIdentifier = "myButton";
            Button.TouchUpInside += delegate
            {
                var title = string.Format("{0} clicks!", count++);
                Button.SetTitle(title, UIControlState.Normal);
            };
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.		
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            Buttttttton.TouchUpInside += Buttttttton_TouchUpInside;
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            Buttttttton.TouchUpInside -= Buttttttton_TouchUpInside;
        }

        void Buttttttton_TouchUpInside(object sender, EventArgs e)
        {
            var Login = loginField.Text;
            var Password = passwordField.Text;

            var choose = ViewModel.CheckUser(Login, Password);
            if (choose == Guid.Empty)
            {
                return;
            }

            var storyboard = UIStoryboard.FromName("Main",null);  

            var vc = (MainMenuViewController)storyboard.InstantiateViewController("MainMenuViewController");

            NavigationController.PushViewController(vc, true);
        }

    }
}
