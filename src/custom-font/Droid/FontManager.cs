using System;
using System.Collections.Generic;
using Android.App;
using Android.Graphics;

namespace CustomFont.Droid
{
	public class FontManager
	{
		private IDictionary<string, Typeface> _typefaces = null;
		protected FontManager()
		{
			_typefaces = new Dictionary<string, Typeface>();
		}

		public Typeface GetTypeface(string fontName)
		{
			if (!_typefaces.ContainsKey(fontName))
			{
				RegisterTypeFace(
					fontName,
					string.Format("Fonts/{0}.ttf", fontName));
			}

			return _typefaces[fontName];
		}

		public FontManager RegisterTypeFace(string fontPath)
		{
			var fontName = System.IO.Path.GetFileNameWithoutExtension(fontPath);
			Console.WriteLine("fontName: " + fontName);
			return RegisterTypeFace(fontName, fontPath);
		}

		private FontManager RegisterTypeFace(string fontName, string fontPath)
		{
			Typeface newTypeface = null;

			try
			{
				newTypeface = Typeface.CreateFromAsset(
					Application.Context.Assets,
					fontPath);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Typeface service: " + ex);
				newTypeface = Typeface.Default;
			}

			_typefaces.Add(fontName, newTypeface);

			return this;
		}

		public void ChangeFont(Android.Widget.TextView control, string fontFamily)
		{
			control.TransformationMethod = null;
			var typeface = string.IsNullOrEmpty(fontFamily) ?
				Typeface.Default :
				GetTypeface(fontFamily);
			control.Typeface = typeface;
		}

		private static FontManager _current = null;
		public static FontManager Current
		{
			get
			{
				return _current ?? (_current = new FontManager());
			}
		}
	}
}
