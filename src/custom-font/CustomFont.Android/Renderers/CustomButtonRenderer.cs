using System;
using System.ComponentModel;
using Android.Graphics;
using CustomFont.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Button), typeof(CustomButtonRenderer))]
namespace CustomFont.Droid.Renderers
{
	public class CustomButtonRenderer: ButtonRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
		{
			base.OnElementChanged(e);

			if (Control == null || Element == null)
				return;
			
			FontManager.Current.ChangeFont(Control, Element.FontFamily);
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (Control == null || Element == null)
				return;
			
			if (e.PropertyName == Button.FontFamilyProperty.PropertyName)
			{
				FontManager.Current.ChangeFont(Control, Element.FontFamily);
			}
		}
	}
}
