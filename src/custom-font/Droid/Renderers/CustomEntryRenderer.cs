using System;
using System.ComponentModel;
using Android.Graphics;
using CustomFont.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer))]
namespace CustomFont.Droid.Renderers
{
	public class CustomEntryRenderer: EntryRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
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
			
			if (e.PropertyName == Entry.FontFamilyProperty.PropertyName)
			{
				FontManager.Current.ChangeFont(Control, Element.FontFamily);
			}
		}
	}
}
