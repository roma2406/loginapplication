// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace LogInApp.iOS
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        UIKit.UIButton Button { get; set; }


        [Outlet]
        UIKit.UIButton Buttttttton { get; set; }


        [Outlet]
        UIKit.UITextField loginField { get; set; }


        [Outlet]
        UIKit.UITextField passwordField { get; set; }


        [Action ("Button_TouchUpInside:")]
        partial void Button_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (Button != null) {
                Button.Dispose ();
                Button = null;
            }

            if (Buttttttton != null) {
                Buttttttton.Dispose ();
                Buttttttton = null;
            }

            if (loginField != null) {
                loginField.Dispose ();
                loginField = null;
            }

            if (passwordField != null) {
                passwordField.Dispose ();
                passwordField = null;
            }
        }
    }
}