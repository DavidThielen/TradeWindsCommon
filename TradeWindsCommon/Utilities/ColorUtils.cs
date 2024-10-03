
// Copyright (c) 2024 Trade Winds Studios (David Thielen)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Drawing;

namespace TradeWindsCommon.Utilities
{
	/// <summary>
	/// Utilities for working with colors.
	/// </summary>
	public static class ColorUtils
    {
		/// <summary>
		/// The text color for a given background color.
		/// </summary>
		public enum TextColor
	    {
			/// <summary>
			/// Display white text.
			/// </summary>
			White,
			/// <summary>
			/// Display black text.
			/// </summary>
			Black
		}

		/// <summary>
		/// For a given background color, determine if the foreground text should be white or black.
		/// </summary>
		/// <param name="background">The background color.</param>
		/// <returns>The foreground text color.</returns>
		public static TextColor GetForegroundColor(Color background)
	    {
		    var luminance = CalculateLuminance(background);
		    return luminance > 0.5 ? TextColor.Black : TextColor.White;
	    }

		public static double CalculateLuminance(Color color)
	    {
		    var rLinear = Linearize(color.R);
		    var gLinear = Linearize(color.G);
		    var bLinear = Linearize(color.B);

		    return 0.2126 * rLinear + 0.7152 * gLinear + 0.0722 * bLinear;
	    }

		public static double Linearize(double channel)
	    {
		    channel /= 255.0;
		    return (channel <= 0.03928) ? (channel / 12.92) : Math.Pow((channel + 0.055) / 1.055, 2.4);
	    }
	}
}
